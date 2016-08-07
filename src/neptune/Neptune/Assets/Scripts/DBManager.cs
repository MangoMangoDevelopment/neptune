using UnityEngine;

public class DBManager {
    public static UrdfDb db = new UrdfDb();
    public DBManager() {}
    
	public void GetSensorList (UIManager uiManager, GameObject testObject, GameObject errorObject, GameObject invisibleObject)
    {
        UrdfItemModel[] sensors = db.GetSensors();

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
            
            if (!sensor.prefabFilename.Equals("unknown"))
            {
                uiManager.AddSensor(sensor.name, sensor.prefabFilename, 10);
            }
            else
            {
                uiManager.AddSensor(sensor.name, testObject, 10);
            }
            //go.AddComponent<Ros>

            /*
            //Load each sensor as it's being read from the DB
            //This ensures each sensor model is drawn on the GPU at least once. This gets cached so that there is no loading time next time each sensor is rendered
            GameObject inst = GameObject.Instantiate<GameObject>(go);
            Debug.Log("Loaded " + sensor.prefabFilename);
            GameObject.Destroy(inst);
            */
        }
	}
}
