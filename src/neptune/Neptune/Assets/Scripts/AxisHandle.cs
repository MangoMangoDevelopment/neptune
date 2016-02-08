using UnityEngine;
using System.Collections;

public class AxisHandle : MonoBehaviour {

    //Static Variables
    public static string TAG = "AxisHandle";

    //Public Structures
    public enum Axis
    {
        XPos,
        YPos,
        ZPos,
        RRot,
        PRot,
        YRot
    };

    //Private Variables
    [SerializeField]
    private Axis axis;

    public Axis GetAxis()
    {
        return axis;
    }
}
