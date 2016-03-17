using UnityEngine;
using System.Collections;

/// <summary>
/// This is a model of sensor categories within the database
/// </summary>
public class SensorCategoriesModel : MonoBehaviour
{
    public int uid;
    public new string name;
    // TODO: Add validation code here for setting data

    public void copy(SensorCategoriesModel model)
    {
        this.uid = model.uid;
        this.name = model.name;
    }
}
