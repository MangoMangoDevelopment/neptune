using UnityEngine;
using System.Collections.Generic;

public class DBManager {
    public static UrdfDb db = new UrdfDb();
    public DBManager() {}

    //TODO: This function shouldn't take in the gameobject to assign. It should take instead use some sort
    //      of representation to the URDF or to the output of Amber's converter that needn't necessarily be passed in.
	public void GetSensorList (UIManager uiManager, GameObject testObject, GameObject errorObject, GameObject invisibleObject)
    {
        List<UrdfItemModel> sensors = db.GetSensors();

        foreach(UrdfItemModel sensor in sensors)
        {
            if (sensor.visibility == 0)
            {
                //Invisible, but usable sensor
                uiManager.AddSensor(sensor.name, invisibleObject);
                continue;
            }
            if (string.IsNullOrEmpty(sensor.prefabFilename) || sensor.prefabFilename.Equals("unknown"))
            {
                continue;
            }

            GameObject go = testObject;
            if (!sensor.prefabFilename.Equals("unknown"))
                go = Resources.Load<GameObject>("Prefabs/Sensors/" + sensor.prefabFilename);
            if (go == null)
                go = errorObject;
            //go.AddComponent<Ros>

            /*
            //Load each sensor as it's being read from the DB
            //This ensures each sensor model is drawn on the GPU at least once. This gets cached so that there is no loading time next time each sensor is rendered
            GameObject inst = GameObject.Instantiate<GameObject>(go);
            Debug.Log("Loaded " + sensor.prefabFilename);
            GameObject.Destroy(inst);
            */

            uiManager.AddSensor(sensor.name, go, 10);

        }
        //uiManager.AddSensor("Microsoft Kinect v2", Resources.Load<GameObject>("Meshes/kinect"), 10);
        //uiManager.AddSensor("SICK LMS-1xx", Resources.Load<GameObject>("Meshes/sick-lms1xx"), 10);
        //uiManager.AddSensor("Kinect", Resources.Load<GameObject>("Models/Sensors/Kinect"), 8);
        //for (int i = 0; i < 50; i++)
        //{
        //    uiManager.AddSensor("Sensor " + i, testObject);
        //}
	}
}
