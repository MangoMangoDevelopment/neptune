using UnityEngine;
using System.Collections;


/// <summary>
/// This is a model of urdfs stored in the database
/// </summary>
public class UrdfItemModel
{
    public int uid;
    public string name;
    public string modelNumber;
    public float internalCost;
    public float externalCost;
    public float weight;
    public float powerUsage;
    public int fk_type_id;
    public int fk_category_id;
    public int usable;
    public int visibility;
    public string urdfFilename;
    public string prefabFilename;
    public string notes;
    public float time;
    // TODO: Add validation code

    public void copy(UrdfItemModel model)
    {
        this.uid = model.uid;
        this.name = model.name;
        this.modelNumber = model.modelNumber;
        this.internalCost = model.internalCost;
        this.externalCost = model.externalCost;
        this.weight = model.weight;
        this.powerUsage = model.powerUsage;
        this.fk_type_id = model.fk_type_id;
        this.fk_category_id = model.fk_category_id;
        this.usable = model.usable;
        this.visibility = model.visibility;
        this.urdfFilename = model.urdfFilename;
        this.prefabFilename = model.prefabFilename;
        this.notes = model.notes;
        this.time = model.time;
    }
}
