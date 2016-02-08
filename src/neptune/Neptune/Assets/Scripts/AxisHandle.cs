using UnityEngine;
using System.Collections;

public class AxisHandle : MonoBehaviour {

    public static string TAG = "AxisHandle";

    public enum Axis
    {
        XPos,
        YPos,
        ZPos,
        RRot,
        PRot,
        YRot
    };

    [SerializeField]
    private Axis axis;

    public Axis GetAxis()
    {
        return axis;
    }
}
