using UnityEngine;
using UnityEditor;
using UrdfUnity.IO;
using System;
using NLog;
using UrdfUnity.Urdf.Models;
using UrdfUnity.Urdf.Models.Links;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// This class handles imported Urdfs and associated meshes. Mesh will have
/// automated prefabs created and Urdfs will be transformed into a Unity GameObject
/// prefab.
/// </summary>
public class ObjectMeshManager : AssetPostprocessor
{
    static private string prefabFolderPath = "Assets/Resources/Prefabs";
    static private string prefabItemPathFormat = prefabFolderPath + "/{0}.prefab";
    static private string prefabPathFormat = prefabFolderPath + "/{0}/{1}.prefab";
    static private string assetFolderPath = "Assets/Materials";
    static private string assetPathFormat = assetFolderPath + "/{0}/{1}.mat";

    static private readonly NLog.Logger LOGGER = LogManager.GetCurrentClassLogger();
    
    /// <summary>
    /// Handles post process on all assets within Unity Editor. Finds all meshes and creates
    /// a Unity prefab allowing easy instation at runtime. For more information about this Unity
    /// Message Handle: http://docs.unity3d.com/ScriptReference/AssetPostprocessor.OnPostprocessAllAssets.html
    /// </summary>
    /// <param name="importedAssets">List of paths to assets that have been imported</param>
    /// <param name="deletedAssets">List of paths to assets that have been deleted</param>
    /// <param name="movedAssets">List of paths to assets that have been moved</param>
    /// <param name="movedFromAssetPaths">List of paths to assets that have been moved from paths</param>
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        if (importedAssets != null && importedAssets.Length > 0)
        {
            FileType type = FileType.UNKNOWN;
            foreach (string assetPath in importedAssets)
            {
                string filename = FileManagerImpl.GetFileName(assetPath, false);
                type = FileManagerImpl.GetFileType(assetPath);
                UnityEngine.Object asset = AssetDatabase.LoadMainAssetAtPath(assetPath);
                // Creating prefab here will cause this function to be invoked again causing an infinite loop of
                // assets being generated. Proper Check of the Object is of type GameObject, isn't part of a prefab
                // and that the asset is a proper model that can become a prefab.
                if (asset.GetType() == typeof(GameObject) &&
                    PrefabUtility.GetPrefabParent(asset) == null &&
                    PrefabUtility.GetPrefabType(asset) == PrefabType.ModelPrefab)
                {
                    // To properly create a prefab of the object we need to instantiate it into
                    // the game world and save that object as a prefab.
                    GameObject go = GameObject.Instantiate<GameObject>((GameObject)asset);
                    go.name = asset.name; // remove the (clone) within the name.
                    PrefabUtility.CreatePrefab(string.Format(prefabItemPathFormat, go.name), go);
                    GameObject.DestroyImmediate(go);
                }
                else if (type == FileType.URDF || type == FileType.XACRO)
                {
                    string prefabName = CreateUrdfRobot(assetPath);
                    if (!String.IsNullOrEmpty(prefabName))
                    {
                        UrdfItemModel item = new UrdfItemModel();
                        item.name = filename;
                        item.urdfFilename = assetPath;
                        item.prefabFilename = prefabName;
                        item.visibility = 1;
                        UrdfDb db = new UrdfDb();
                        db.AddSensor(item);
                    }
                }
            }
        }
    }


    /// <summary>
    /// Handles putting together a robot GameObject and saving it as a prefab for
    /// later use.
    /// </summary>
    /// <param name="path">The path of the URDF</param>
    /// <returns>A string represenation of the path and name of the prefab created.</returns>
    static string CreateUrdfRobot(string path)
    {
        FileManagerImpl fileManager = new FileManagerImpl();
        string filename = FileManagerImpl.GetFileName(path, false);
        Robot robo = fileManager.GetRobotFromFile(path);


        string roboName = "";
        if (robo != null)
        {
            roboName = filename + "/" + robo.Name;
            GameObject parent = new GameObject(robo.Name);
            parent.AddComponent<RosRobotModel>();
            parent.GetComponent<RosRobotModel>().robot = robo;

            Robot rob2 = parent.GetComponent<RosRobotModel>().robot;
            Debug.Log(rob2.Name);
            Dictionary<string, GameObject> linkAsGos = new Dictionary<string, GameObject>();

            // creating a list of gameobjects
            foreach (KeyValuePair<string, Link> link in robo.Links)
            {
                GameObject linkGo = new GameObject(link.Key);

                linkGo.AddComponent<RosLinkModel>();
                RosLinkModel linkModel = linkGo.GetComponent<RosLinkModel>();
                linkModel.link = link.Value;
                linkModel.name = link.Key;

                if (link.Value.Visual.Count > 0)
                {
                    GameObject linkVisualGo = null;
                    Vector3 linkScale = new Vector3();
                    Vector3 linkPosition = new Vector3();
                    Vector3 linkRotation = new Vector3();
                    float radiusScale = 0.0f;

                    foreach (Visual obj in link.Value.Visual)
                    {
                        if ((Geometry.Shapes.Mesh == obj.Geometry.Shape) && (obj.Geometry.Mesh != null))
                        {
                            linkModel.hasMesh = true;
                            string fileName = FileManagerImpl.GetFileName(obj.Geometry.Mesh.FileName, false);
                            string[] guids = AssetDatabase.FindAssets(string.Format("{0} t:GameObject", fileName));
                            foreach (string guid in guids)
                            {
                                UnityEngine.Object asset = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(guid));
                                if (asset.name == fileName)
                                {
                                    linkVisualGo = GameObject.Instantiate<GameObject>((GameObject)asset);
                                    break;
                                }
                            }
                            if (obj.Geometry.Mesh.Size != null)
                            {
                                linkScale = new Vector3((float)obj.Geometry.Mesh.Size.Length, (float)obj.Geometry.Mesh.Size.Height, (float)obj.Geometry.Mesh.Size.Width);
                            }
                            else
                            {
                                linkScale = new Vector3(0.1f, 0.1f, 0.1f);
                            }
                            linkPosition = new Vector3((float)obj.Origin.Xyz.X, (float)obj.Origin.Xyz.Y, (float)obj.Origin.Xyz.Z);
                            linkRotation = new Vector3(Mathf.Rad2Deg * (float)obj.Origin.Rpy.R, Mathf.Rad2Deg * (float)obj.Origin.Rpy.P, Mathf.Rad2Deg * (float)obj.Origin.Rpy.Y);
                        }
                        else
                        {
                            PrimitiveType type = PrimitiveType.Cube;

                            switch (obj.Geometry.Shape)
                            {
                                case Geometry.Shapes.Cylinder:
                                    type = PrimitiveType.Cylinder;
                                    radiusScale = (float)obj.Geometry.Cylinder.Radius * 2;
                                    linkScale = new Vector3(radiusScale, (float)obj.Geometry.Cylinder.Length / 2, radiusScale);
                                    break;
                                case Geometry.Shapes.Sphere:
                                    type = PrimitiveType.Sphere;
                                    radiusScale = (float)obj.Geometry.Sphere.Radius * 2;
                                    linkScale = new Vector3(radiusScale, radiusScale, radiusScale);
                                    break;
                                case Geometry.Shapes.Box:
                                    type = PrimitiveType.Cube;
                                    linkScale = new Vector3((float)obj.Geometry.Box.Size.Length, (float)obj.Geometry.Box.Size.Height, (float)obj.Geometry.Box.Size.Width);
                                    break;
                            }
                            linkVisualGo = GameObject.CreatePrimitive(type);
                            linkPosition = new Vector3((float)obj.Origin.Xyz.X, (float)obj.Origin.Xyz.Z, (float)obj.Origin.Xyz.Y);
                            linkRotation = new Vector3(Mathf.Rad2Deg * (float)obj.Origin.Rpy.R, Mathf.Rad2Deg * (float)obj.Origin.Rpy.Y, Mathf.Rad2Deg * (float)obj.Origin.Rpy.P);
                        }

                        if (linkVisualGo != null)
                        {
                            linkVisualGo.transform.localEulerAngles = linkRotation;
                            linkVisualGo.transform.localScale = linkScale;
                            linkVisualGo.transform.localPosition = linkPosition;

                            if (obj.Material != null && obj.Material.Color != null)
                            {
                                Material mat = new Material(Shader.Find("Standard"));
                                mat.color = new Color(obj.Material.Color.Rgb.R, obj.Material.Color.Rgb.G, obj.Material.Color.Rgb.B);
                                if(!Directory.Exists(assetFolderPath + "/" + parent.name))
                                {
                                    AssetDatabase.CreateFolder(assetFolderPath, parent.name);
                                }
                                AssetDatabase.CreateAsset(mat, string.Format(assetPathFormat, parent.name, link.Key));
                                linkVisualGo.GetComponent<Renderer>().sharedMaterial = mat;
                            }
                            linkVisualGo.transform.SetParent(linkGo.transform);
                        }
                    }
                    
                }
                linkGo.transform.SetParent(parent.transform);
                linkAsGos.Add(link.Key, linkGo);
            }

            foreach (KeyValuePair<string, UrdfUnity.Urdf.Models.Joint> joint in robo.Joints)
            {
                GameObject child = linkAsGos[joint.Value.Child.Name];
                child.transform.SetParent(linkAsGos[joint.Value.Parent.Name].transform);
                // Very strange, imported object will have the proper associated XYZ coordinates where as
                // a generated primitive types have the YZ coordinates are swapped.
                if (child.GetComponent<RosLinkModel>().hasMesh)
                {
                    child.transform.localPosition = new Vector3((float)joint.Value.Origin.Xyz.X, (float)joint.Value.Origin.Xyz.Y, (float)joint.Value.Origin.Xyz.Z);
                }
                else
                {
                    // URDF considers going up as the Z-axis where as unity consider going up is Y-axis
                    child.transform.localPosition = new Vector3((float)joint.Value.Origin.Xyz.X, (float)joint.Value.Origin.Xyz.Z, (float)joint.Value.Origin.Xyz.Y);
                }
                
                child.transform.localEulerAngles = new Vector3(Mathf.Rad2Deg * (float)joint.Value.Origin.Rpy.R, Mathf.Rad2Deg * (float)joint.Value.Origin.Rpy.Y, Mathf.Rad2Deg * (float)joint.Value.Origin.Rpy.P);
            }
            AssetDatabase.CreateFolder(prefabFolderPath, filename);
            PrefabUtility.CreatePrefab(string.Format(prefabPathFormat, filename, parent.name), parent);
            GameObject.DestroyImmediate(parent);
        }
        else
        {
            Debug.Log("Fail");
            LOGGER.Warn("No Robot generated from URDF.");
        }

        return roboName;
    }

}
