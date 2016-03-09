using UnityEngine;
using System.Collections;


/// <summary>
/// This is a model of urdfs stored in the database
/// </summary>
public class UrdfItemModel : MonoBehaviour
{
    public int uid;
    public new string name;
    public string modelNumber;
    public float internalCost;
    public float externalCost;
    public float weight;
    public float powerUsage;
    public int fk_type_id;
    public int fk_category_id;
    public int usable;
    public string urdfFilename;
    public string prefabFilename;
    public float time;
    // TODO: Add validation code
}
