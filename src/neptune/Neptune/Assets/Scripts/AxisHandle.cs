using UnityEngine;
using System.Collections;

public class AxisHandle : MonoBehaviour {

    //Static Variables
    public static string TAG = "AxisHandle";
    public GameObject head;
    public GameObject stem;
    public Axis axis;

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
    private float TopViewportLimit;
    private float BotViewportLimit;
    private float LeftViewportLimit;
    private float RightViewportLimit;
    private EditorManager editorManager;

    void Start()
    {
        editorManager = GameObject.FindGameObjectWithTag(EditorManager.TAG).GetComponent<EditorManager>();
        TopViewportLimit = 0.9f;
        BotViewportLimit = 0.15f;
        LeftViewportLimit = 0.3f;
        RightViewportLimit = 0.7f;
    }

    public Axis GetAxis()
    {
        return axis;
    }

    private void CheckYViewportLimits(out bool rotate, out float diff)
    {
        Vector3 stemViewportPoint = editorManager.HandleCamera.WorldToViewportPoint(stem.transform.position);
        Vector3 headViewportPoint = editorManager.HandleCamera.WorldToViewportPoint(head.transform.position);

        rotate = false;
        diff = 0;

        if (headViewportPoint.y > TopViewportLimit && headViewportPoint.y > stemViewportPoint.y)
        {
            rotate = true;
            diff = headViewportPoint.y - TopViewportLimit;
        }
        if (headViewportPoint.y < BotViewportLimit && headViewportPoint.y < stemViewportPoint.y)
        {
            rotate = true;
            diff = BotViewportLimit - headViewportPoint.y;
        }
    }

    private void CheckXZViewportLimits(out bool xRotate, out float xDiff, out bool yRotate, out float yDiff)
    {
        Vector3 stemViewportPoint = editorManager.HandleCamera.WorldToViewportPoint(stem.transform.position);
        Vector3 headViewportPoint = editorManager.HandleCamera.WorldToViewportPoint(head.transform.position);

        xRotate = false;
        yRotate = false;
        xDiff = 0;
        yDiff = 0;

        if (headViewportPoint.x > RightViewportLimit && headViewportPoint.x > stemViewportPoint.x)
        {
            xRotate = true;
            xDiff = headViewportPoint.x - RightViewportLimit;
        }
        else if (headViewportPoint.x < LeftViewportLimit && headViewportPoint.x < stemViewportPoint.x)
        {
            xRotate = true;
            xDiff = LeftViewportLimit - headViewportPoint.x;
        }

        if (headViewportPoint.y > TopViewportLimit && headViewportPoint.y > stemViewportPoint.y)
        {
            yRotate = true;
            yDiff = headViewportPoint.y - TopViewportLimit;
        }
        else if (headViewportPoint.y < BotViewportLimit && headViewportPoint.y < stemViewportPoint.y)
        {
            yRotate = true;
            yDiff = BotViewportLimit - headViewportPoint.y;
        }
    }

    void Update()
    {
        if (editorManager)
        {

            switch (axis)
            {
                case Axis.YPos:
                    bool rotate = false;
                    float diff = 0;

                    CheckYViewportLimits(out rotate, out diff);

                    if (rotate)
                    {
                        transform.Rotate(new Vector3(180, 0, 0));
                    }
                    break;
                case Axis.XPos:
                case Axis.ZPos:
                    bool xRotate = false;
                    bool yRotate = false;
                    float curXDiff = 0;
                    float curYDiff = 0;

                    CheckXZViewportLimits(out xRotate, out curXDiff, out yRotate, out curYDiff);

                    if (xRotate || yRotate)
                    {
                        float newXDiff = 0;
                        float newYDiff = 0;
                        transform.Rotate(new Vector3(180, 0, 0));
                        CheckXZViewportLimits(out xRotate, out newXDiff, out yRotate, out newYDiff);
                        if (xRotate || yRotate)
                        {
                            //Okay, we've checked both rotations and they both exceed our limits..
                            //Let's decide which one is the least intrusive and go with it
                            float deltaCur = Mathf.Max(curXDiff, curYDiff);
                            float deltaNew = Mathf.Max(newXDiff, newYDiff);
                            if (deltaCur < deltaNew)
                            {
                                //The original transform yields less of an intrusion into the limits..
                                //Let's go back to the original.
                                transform.Rotate(new Vector3(180, 0, 0));
                            }
                        }
                    }
                    break;
            }
        }
    }
}
