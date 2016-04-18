using UnityEngine;
using UrdfUnity.Urdf.Models;


/// <summary>
/// This class holds a UrdfUnity Link model to be attached
/// to the corresponding GameObject. 
/// </summary>
public class RosLinkModel : MonoBehaviour {
    public Link link = null;
    public bool hasMesh = false;
}
