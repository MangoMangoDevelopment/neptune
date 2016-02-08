using UnityEngine;
using System.Collections;

public class Manipulatable : MonoBehaviour {

    //Public Variables
    public float DragScaleFactor = 1f;
    public bool XPosManipulation = true;
    public bool YPosManipulation = true;
    public bool ZPosManipulation = true;
    public GameObject XPosHandle;
    public GameObject YPosHandle;
    public GameObject ZPosHandle;

    //Private Variables
    private bool isDragging = false;
    private AxisHandle.Axis draggedAxis;
    private float lastDragObjectPos;
    private Vector3 lastDragMousePos;

    void Start () {
        XPosHandle.SetActive(XPosManipulation);
        YPosHandle.SetActive(YPosManipulation);
        ZPosHandle.SetActive(ZPosManipulation);
    }
	

	void Update () {
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
                Vector3 startingPoint = lastDragMousePos;
                Vector3 currentPoint = Input.mousePosition;
                Debug.Log(startingPoint + "::" + currentPoint + " == " + (startingPoint - currentPoint));
                float offset = 0;
                switch (draggedAxis)
                {
                    case AxisHandle.Axis.XPos:
                        offset = (currentPoint - startingPoint).x * DragScaleFactor * Time.deltaTime;
                        pos.x = lastDragObjectPos + offset;
                        lastDragObjectPos = pos.x;
                        transform.position = pos;
                        break;
                    case AxisHandle.Axis.YPos:
                        offset = (currentPoint - startingPoint).y * DragScaleFactor * Time.deltaTime;
                        pos.y = lastDragObjectPos + offset;
                        lastDragObjectPos = pos.y;
                        transform.position = pos;
                        break;
                    case AxisHandle.Axis.ZPos:
                        //Get the larger offset (either X or Y axis) and use that difference to manipulate on the Z axis
                        float xOffset = (currentPoint - startingPoint).x;
                        float yOffset = (currentPoint - startingPoint).y;
                        offset = (Mathf.Abs(xOffset) > Mathf.Abs(yOffset) ? xOffset : yOffset) * DragScaleFactor * Time.deltaTime;
                        pos.z = lastDragObjectPos + offset;
                        lastDragObjectPos = pos.z;
                        transform.position = pos;
                        break;
                }
                lastDragMousePos = currentPoint;
            }
        }
	}
}
