using UnityEngine;
using System.Collections.Generic;

public class DBManager {
    UrdfDb db = new UrdfDb();
    public DBManager() {}

    //TODO: This function shouldn't take in the gameobject to assign. It should take instead use some sort
    //      of representation to the URDF or to the output of Amber's converter that needn't necessarily be passed in.
	public void GetSensorList (UIManager uiManager, GameObject testObject)
    {
        List<UrdfItemModel> sensors = db.GetSensors();

        foreach(UrdfItemModel sensor in sensors)
        {
            if (string.IsNullOrEmpty(sensor.prefabFilename) || sensor.prefabFilename == "Unknown")
            {
                continue;
            }
            GameObject go = Resources.Load<GameObject>("Prefabs/" + sensor.prefabFilename);
            if (go.GetComponent<Manipulatable>() == null)
            {
                go.AddComponent<Manipulatable>();
            }
            uiManager.AddSensor(sensor.name, go, 8);

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
