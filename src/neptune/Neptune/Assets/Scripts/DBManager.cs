using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DBManager {

    public DBManager() {}

    //TODO: This function shouldn't take in the gameobject to assign. It should take instead use some sort
    //      of representation to the URDF or to the output of Amber's converter that needn't necessarily be passed in.
	public void GetSensorList (UIManager uiManager, GameObject testObject)
    {
        for (int i = 0; i < 50; i++)
        {
            uiManager.AddSensor("Sensor " + i, testObject);
        }
	}
}
