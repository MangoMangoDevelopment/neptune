using UnityEngine;
using System.Collections;

/// <summary>
/// This is a model of urdf types within the database
/// </summary>
public class UrdfTypeModel
{
    public int uid;
    public string name;
    // TODO: Add validation code

    public void copy(UrdfTypeModel model)
    {
        this.uid = model.uid;
        this.name = model.name;
    }
}