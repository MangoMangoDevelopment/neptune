using UnityEngine;

/// <summary>
/// Handles information about a sensor associated to a unity game object.
/// </summary>
public class Sensor : MonoBehaviour {
    public GameObject sensorParent;

    public UrdfItemModel item = new UrdfItemModel();

    public UrdfTypeModel type = new UrdfTypeModel();

    public SensorCategoriesModel category = new SensorCategoriesModel();


}
