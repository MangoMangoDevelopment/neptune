using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class AxisHandle : MonoBehaviour {

    //Static Variables
    public static string TAG = "AxisHandle";
    public GameObject head;
    public GameObject stem;
    public Axis axis;

    //Public Structures
    public enum Axis
    {
        XPos,
        YPos,
        ZPos,
        RRot,
        PRot,
        YRot
    };

    //Private Variables
    private float TopViewportLimit;
    private float BotViewportLimit;
    private float LeftViewportLimit;
    private float RightViewportLimit;
    private EditorManager editorManager;
    private bool highlighted;
    private Dictionary<Transform, Material> materials;

    void Start()
    {
        editorManager = GameObject.FindGameObjectWithTag(EditorManager.TAG).GetComponent<EditorManager>();
        TopViewportLimit = 0.9f;
        BotViewportLimit = 0.15f;
        LeftViewportLimit = 0.3f;
        RightViewportLimit = 0.7f;
        highlighted = false;
        materials = new Dictionary<Transform, Material>();

        if (axis == Axis.XPos || axis == Axis.YPos || axis == Axis.ZPos)
        {
            foreach (Transform child in transform)
            {
                materials.Add(child, child.gameObject.GetComponent<Renderer>().material);
            }
        }
        else
        {
            materials.Add(transform, gameObject.GetComponent<Renderer>().material);
        }
    }

    private void Highlight()
    {
        if (!highlighted)
        {
            highlighted = true;
            if (axis == Axis.XPos || axis == Axis.YPos || axis == Axis.ZPos)
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.GetComponent<Renderer>().material = editorManager.HandleOutline;
                }
            }
            else
            {
                gameObject.GetComponent<Renderer>().material = editorManager.HandleOutline;
            }
        }
    }

    private void ClearHighlight()
    {
        if (highlighted)
        {
            highlighted = false;
            if (axis == Axis.XPos || axis == Axis.YPos || axis == Axis.ZPos)
            {
                foreach (Transform child in transform)
                {
                    Material material;
                    materials.TryGetValue(child, out material);
                    child.gameObject.GetComponent<Renderer>().material = material;
                }
            }
            else
            {
                Material material;
                materials.TryGetValue(transform, out material);
                gameObject.GetComponent<Renderer>().material = material;
            }
        }
    }

    public void UpdateHighlight()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            //Mouse is hovering over UI elements. Let's not let those events pass through to the game world.
            return;
        }
        Ray uiRay = editorManager.HandleCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask UIMask = (1 << LayerMask.NameToLayer("UI"));
        //Only bother checking if we hit UI elements (Axis Handles)
        if (Physics.Raycast(uiRay, out hit, editorManager.HandleCamera.farClipPlane, UIMask))
        {
            if (hit.transform.gameObject == gameObject)
            {
                Highlight();
            }
            else
            {
                ClearHighlight();
            }
        }
        else
        {
            ClearHighlight();
        }
    }

    public Axis GetAxis()
    {
        return axis;
    }

    private void CheckYViewportLimits(out bool rotate, out float diff)
    {
        Vector3 stemViewportPoint = editorManager.HandleCamera.WorldToViewportPoint(stem.transform.position);
        Vector3 headViewportPoint = editorManager.HandleCamera.WorldToViewportPoint(head.transform.position);

        rotate = false;
        diff = 0;

        if (headViewportPoint.y > TopViewportLimit && headViewportPoint.y > stemViewportPoint.y)
        {
            rotate = true;
            diff = headViewportPoint.y - TopViewportLimit;
        }
        if (headViewportPoint.y < BotViewportLimit && headViewportPoint.y < stemViewportPoint.y)
        {
            rotate = true;
            diff = BotViewportLimit - headViewportPoint.y;
        }
    }

    private void CheckXZViewportLimits(out bool xRotate, out float xDiff, out bool yRotate, out float yDiff)
    {
        Vector3 stemViewportPoint = editorManager.HandleCamera.WorldToViewportPoint(stem.transform.position);
        Vector3 headViewportPoint = editorManager.HandleCamera.WorldToViewportPoint(head.transform.position);

        xRotate = false;
        yRotate = false;
        xDiff = 0;
        yDiff = 0;

        if (headViewportPoint.x > RightViewportLimit && headViewportPoint.x > stemViewportPoint.x)
        {
            xRotate = true;
            xDiff = headViewportPoint.x - RightViewportLimit;
        }
        else if (headViewportPoint.x < LeftViewportLimit && headViewportPoint.x < stemViewportPoint.x)
        {
            xRotate = true;
            xDiff = LeftViewportLimit - headViewportPoint.x;
        }

        if (headViewportPoint.y > TopViewportLimit && headViewportPoint.y > stemViewportPoint.y)
        {
            yRotate = true;
            yDiff = headViewportPoint.y - TopViewportLimit;
        }
        else if (headViewportPoint.y < BotViewportLimit && headViewportPoint.y < stemViewportPoint.y)
        {
            yRotate = true;
            yDiff = BotViewportLimit - headViewportPoint.y;
        }
    }

    public Axis GetMouseAxisModifier()
    {
        Vector3 stemViewportPoint = editorManager.HandleCamera.WorldToViewportPoint(stem.transform.position);
        Vector3 headViewportPoint = editorManager.HandleCamera.WorldToViewportPoint(head.transform.position);

        float xDiff = Mathf.Max(stemViewportPoint.x, headViewportPoint.x) - Mathf.Min(stemViewportPoint.x, headViewportPoint.x);
        float yDiff = Mathf.Max(stemViewportPoint.y, headViewportPoint.y) - Mathf.Min(stemViewportPoint.y, headViewportPoint.y);

        if (xDiff > yDiff)
        {
            return Axis.XPos;
        }
        return Axis.YPos;
    }

    void Update()
    {
        if (editorManager)
        {
            switch (axis)
            {
                case Axis.YPos:
                    bool rotate = false;
                    float diff = 0;

                    CheckYViewportLimits(out rotate, out diff);

                    if (rotate)
                    {
                        transform.Rotate(new Vector3(180, 0, 0));
                    }
                    break;
                case Axis.XPos:
                case Axis.ZPos:
                    bool xRotate = false;
                    bool yRotate = false;
                    float curXDiff = 0;
                    float curYDiff = 0;

                    CheckXZViewportLimits(out xRotate, out curXDiff, out yRotate, out curYDiff);

                    if (xRotate || yRotate)
                    {
                        float newXDiff = 0;
                        float newYDiff = 0;
                        transform.Rotate(new Vector3(180, 0, 0));
                        CheckXZViewportLimits(out xRotate, out newXDiff, out yRotate, out newYDiff);
                        if (xRotate || yRotate)
                        {
                            //Okay, we've checked both rotations and they both exceed our limits..
                            //Let's decide which one is the least intrusive and go with it
                            float deltaCur = Mathf.Max(curXDiff, curYDiff);
                            float deltaNew = Mathf.Max(newXDiff, newYDiff);
                            if (deltaCur < deltaNew)
                            {
                                //The original transform yields less of an intrusion into the limits..
                                //Let's go back to the original.
                                transform.Rotate(new Vector3(180, 0, 0));
                            }
                        }
                    }
                    break;
            }
            UpdateHighlight();
        }
    }
}
