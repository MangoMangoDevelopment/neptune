using UnityEngine;
using System.Collections;

public class Manipulatable : MonoBehaviour {

    //Static Variables
    public static string TAG = "Manipulatable";

    //Public Variables
    public bool isSelected = false;

    //XYZ Translation
    public float XYZDragScaleFactor = 1f;
    public bool XPosManipulation = true;
    public bool YPosManipulation = true;
    public bool ZPosManipulation = true;

    //RPY Rotation
    public float RPYDragScaleFactor = 10f;
    public bool RRotManipulation = true;
    public bool PRotManipulation = true;
    public bool YRotManipulation = true;

    //Private Variables
    private bool isDragging = false;
    private AxisHandle.Axis draggedAxis;
    private float lastDragObjectPos;
    private Vector3 lastDragMousePos;
    private EditorManager editorManager;

    void Start()
    {
        editorManager = GameObject.FindGameObjectWithTag("EditorManager").GetComponent<EditorManager>();
    }

    public void Select ()
    {
        isSelected = true;
    }

    public void Deselect()
    {
        isSelected = false;
    }

    void Update()
    {
        if (isSelected)
        {
            UpdateDrag();
        }
    }

    private void UpdateDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(mouseRay, out hit, 100))
            {
                if (hit.transform.gameObject.tag == AxisHandle.TAG)
                {
                    AxisHandle handle = hit.transform.gameObject.GetComponent<AxisHandle>();
                    switch (handle.GetAxis())
                    {
                        case AxisHandle.Axis.XPos:
                            if (XPosManipulation)
                            {
                                draggedAxis = AxisHandle.Axis.XPos;
                                isDragging = true;
                                lastDragObjectPos = transform.position.x;
                                lastDragMousePos = Input.mousePosition;
                            }
                            break;
                        case AxisHandle.Axis.YPos:
                            if (YPosManipulation)
                            {
                                draggedAxis = AxisHandle.Axis.YPos;
                                isDragging = true;
                                lastDragObjectPos = transform.position.y;
                                lastDragMousePos = Input.mousePosition;
                            }
                            break;
                        case AxisHandle.Axis.ZPos:
                            if (ZPosManipulation)
                            {
                                draggedAxis = AxisHandle.Axis.ZPos;
                                isDragging = true;
                                lastDragObjectPos = transform.position.z;
                                lastDragMousePos = Input.mousePosition;
                            }
                            break;
                        case AxisHandle.Axis.RRot:
                            if (RRotManipulation)
                            {
                                draggedAxis = AxisHandle.Axis.RRot;
                                isDragging = true;
                                lastDragObjectPos = transform.localRotation.eulerAngles.z;
                                lastDragMousePos = Input.mousePosition;
                            }
                            break;
                        case AxisHandle.Axis.PRot:
                            if (PRotManipulation)
                            {
                                draggedAxis = AxisHandle.Axis.PRot;
                                isDragging = true;
                                lastDragObjectPos = transform.localRotation.eulerAngles.x;
                                lastDragMousePos = Input.mousePosition;
                            }
                            break;
                        case AxisHandle.Axis.YRot:
                            if (YRotManipulation)
                            {
                                draggedAxis = AxisHandle.Axis.YRot;
                                isDragging = true;
                                lastDragObjectPos = transform.localRotation.eulerAngles.y;
                                lastDragMousePos = Input.mousePosition;
                            }
                            break;
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
        else
        {
            if (isDragging)
            {
                Vector3 pos = transform.position;
                Vector3 rot = transform.rotation.eulerAngles;
                Vector3 startingPoint = lastDragMousePos;
                Vector3 currentPoint = Input.mousePosition;
                float xOffset = 0;
                float yOffset = 0;
                float offset = 0;
                switch (draggedAxis)
                {
                    case AxisHandle.Axis.XPos:
                        offset = (currentPoint - startingPoint).x * XYZDragScaleFactor * Time.deltaTime;
                        pos.x = lastDragObjectPos + offset;
                        lastDragObjectPos = pos.x;
                        transform.position = pos;
                        break;
                    case AxisHandle.Axis.YPos:
                        offset = (currentPoint - startingPoint).y * XYZDragScaleFactor * Time.deltaTime;
                        pos.y = lastDragObjectPos + offset;
                        lastDragObjectPos = pos.y;
                        transform.position = pos;
                        break;
                    case AxisHandle.Axis.ZPos:
                        //Get the larger offset (either X or Y axis) and use that difference to manipulate on the Z axis
                        xOffset = (currentPoint - startingPoint).x;
                        yOffset = (currentPoint - startingPoint).y;
                        offset = (Mathf.Abs(xOffset) > Mathf.Abs(yOffset) ? xOffset : yOffset) * XYZDragScaleFactor * Time.deltaTime;
                        pos.z = lastDragObjectPos + offset;
                        lastDragObjectPos = pos.z;
                        transform.position = pos;
                        break;
                    case AxisHandle.Axis.RRot:
                        //Get the larger offset (either X or Y axis) and use that difference to manipulate on the R axis
                        xOffset = (currentPoint - startingPoint).x;
                        yOffset = (currentPoint - startingPoint).y;
                        offset = (Mathf.Abs(xOffset) > Mathf.Abs(yOffset) ? xOffset : yOffset) * RPYDragScaleFactor * Time.deltaTime;
                        rot.z = lastDragObjectPos + offset;
                        lastDragObjectPos = rot.z;
                        transform.localRotation = Quaternion.Euler(rot);
                        break;
                    case AxisHandle.Axis.PRot:
                        //Get the larger offset (either X or Y axis) and use that difference to manipulate on the R axis
                        xOffset = (currentPoint - startingPoint).x;
                        yOffset = (currentPoint - startingPoint).y;
                        offset = (Mathf.Abs(xOffset) > Mathf.Abs(yOffset) ? xOffset : yOffset) * RPYDragScaleFactor * Time.deltaTime;
                        rot.x = lastDragObjectPos + offset;
                        lastDragObjectPos = rot.x;
                        transform.localRotation = Quaternion.Euler(rot);
                        break;
                    case AxisHandle.Axis.YRot:
                        //Get the larger offset (either X or Y axis) and use that difference to manipulate on the R axis
                        xOffset = (currentPoint - startingPoint).x;
                        yOffset = (currentPoint - startingPoint).y;
                        offset = (Mathf.Abs(xOffset) > Mathf.Abs(yOffset) ? xOffset : yOffset) * RPYDragScaleFactor * Time.deltaTime;
                        rot.y = lastDragObjectPos + offset;
                        lastDragObjectPos = rot.y;
                        transform.localRotation = Quaternion.Euler(rot);
                        break;
                }
                lastDragMousePos = currentPoint;
            }
        }
    }
}
