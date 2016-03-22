using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Linq;

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
    private bool lastSelected = false;
    private bool isDragging = false;
    private AxisHandle.Axis draggedAxis;
    private float lastDragObjectPos;
    private Vector3 lastDragMousePos;
    private Vector2 offsetMultiplier;
    private AxisHandle.Axis mouseAxisModifier;
    private EditorManager editorManager;
    private GameObject outline;
    private GameObject bridge;
    private bool bridgeShown;

    void Start()
    {
        editorManager = GameObject.FindGameObjectWithTag("EditorManager").GetComponent<EditorManager>();
    }

    public void Select ()
    {
        lastSelected = isSelected;
        isSelected = true;
        ShowOutline(editorManager.OutlineMaterial);
    }

    public void Deselect()
    {
        lastSelected = isSelected;
        isSelected = false;
        ClearOutline();
    }

    public void ShowOutline(Material mat)
    {
        ClearOutline();
        outline = Instantiate(gameObject);
        foreach (Transform child in outline.transform)
        {
            if (child.name == "Outline")
            {
                Destroy(child.gameObject);
            }
        }
        outline.name = "Outline";
        Destroy(outline.GetComponent<Manipulatable>());
        outline.tag = "Outline";
        //This is so that the EditorManager ignores the outline (since it's technically in front of the object)
        outline.layer = LayerMask.NameToLayer("Ignore Raycast");
        outline.GetComponent<Renderer>().material = mat;
        outline.transform.localScale = transform.localScale + new Vector3(editorManager.OutlineThickness * transform.localScale.x, editorManager.OutlineThickness * transform.localScale.x, editorManager.OutlineThickness * transform.localScale.x);
        outline.transform.SetParent(transform);
        outline.transform.localPosition = Vector3.zero;
        outline.transform.localRotation = Quaternion.identity;
        Mesh mesh = outline.GetComponent<MeshFilter>().mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
    }

    public void ClearOutline()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "Outline")
            {
                Destroy(child.gameObject);
            }
        }
        Destroy(outline);
        outline = null;
    }

    public void ResetAxis(AxisHandle.Axis axis)
    {
        switch(axis)
        {
            case AxisHandle.Axis.XPos:
                {
                    Vector3 pos = transform.position;
                    pos.x = 0;
                    transform.position = pos;
                }
                break;
            case AxisHandle.Axis.YPos:
                {
                    Vector3 pos = transform.position;
                    pos.y = 0;
                    transform.position = pos;
                }
                break;
            case AxisHandle.Axis.ZPos:
                {
                    Vector3 pos = transform.position;
                    pos.z = 0;
                    transform.position = pos;
                }
                break;
            case AxisHandle.Axis.RRot:
                {
                    Vector3 rot = transform.rotation.eulerAngles;
                    rot.z = 0;
                    transform.rotation = Quaternion.Euler(rot);
                }
                break;
            case AxisHandle.Axis.PRot:
                {
                    Vector3 rot = transform.rotation.eulerAngles;
                    rot.x = 0;
                    transform.rotation = Quaternion.Euler(rot);
                }
                break;
            case AxisHandle.Axis.YRot:
                {
                    Vector3 rot = transform.rotation.eulerAngles;
                    rot.y = 0;
                    transform.rotation = Quaternion.Euler(rot);
                }
                break;
        }
    }

    void Update()
    {
        if (isSelected)
        {
            //Force skip this frame if we just clicked. Let the Editor manager handle its click first
            if (!lastSelected)
            {
                lastSelected = true;
            }
            else
            {
                UpdateDrag();
            }
        }
        else
        {
            lastSelected = isSelected;
        }

        UpdateOutline();
    }
    
    private void UpdateDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            offsetMultiplier = Vector2.one;
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                //Mouse is hovering over UI elements. Let's not let those events pass through to the game world.
                return;
            }
            Ray mouseRay = editorManager.HandleCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //Only check if we hit an axis handle
            LayerMask UIMask = (1 << LayerMask.NameToLayer("UI"));
            if (Physics.Raycast(mouseRay, out hit, 100, UIMask))
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
                                if (editorManager.HandleCamera.transform.position.z > transform.position.z)
                                    offsetMultiplier.x = -1;

                                if (editorManager.HandleCamera.transform.position.x > transform.position.x)
                                {
                                    offsetMultiplier.y = 1;
                                    if (editorManager.HandleCamera.transform.position.y > transform.position.y)
                                        offsetMultiplier.y = -1;
                                }
                                else
                                {
                                    if (editorManager.HandleCamera.transform.position.y < transform.position.y)
                                        offsetMultiplier.y = -1;
                                    else
                                        offsetMultiplier.y = 1;
                                }
                                mouseAxisModifier = handle.GetMouseAxisModifier();
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
                                if (editorManager.HandleCamera.transform.position.x < transform.position.x)
                                    offsetMultiplier.x = -1;
                                if (editorManager.HandleCamera.transform.position.z > transform.position.z)
                                {
                                    offsetMultiplier.y = 1;
                                    if (editorManager.HandleCamera.transform.position.y > transform.position.y)
                                        offsetMultiplier.y = -1;
                                }
                                else
                                {
                                    if (editorManager.HandleCamera.transform.position.y < transform.position.y)
                                        offsetMultiplier.y = -1;
                                    else
                                        offsetMultiplier.y = 1;
                                }
                                mouseAxisModifier = handle.GetMouseAxisModifier();
                            }
                            break;
                        case AxisHandle.Axis.RRot:
                            if (RRotManipulation)
                            {
                                draggedAxis = AxisHandle.Axis.RRot;
                                isDragging = true;
                                lastDragObjectPos = transform.rotation.eulerAngles.z;
                                lastDragMousePos = Input.mousePosition;
                                //Check where the raycast hit the torus. This changes the directions of mouse y and x
                                if (hit.point.x < transform.position.x)
                                    offsetMultiplier.y *= -1;
                                if (hit.point.y > transform.position.y)
                                    offsetMultiplier.x *= -1;
                                if (editorManager.HandleCamera.transform.position.z > transform.position.z)
                                    offsetMultiplier.x *= -1;
                            }
                            break;
                        case AxisHandle.Axis.PRot:
                            if (PRotManipulation)
                            {
                                draggedAxis = AxisHandle.Axis.PRot;
                                isDragging = true;
                                lastDragObjectPos = transform.rotation.eulerAngles.x;
                                lastDragMousePos = Input.mousePosition;
                                //Check where the raycast hit the torus. This changes the directions of mouse y and x
                                if (hit.point.z > transform.position.z)
                                    offsetMultiplier.y *= -1;
                                if (hit.point.y < transform.position.y)
                                    offsetMultiplier.x *= -1;
                                if (editorManager.HandleCamera.transform.position.x < transform.position.x)
                                    offsetMultiplier.x *= -1;
                            }
                            break;
                        case AxisHandle.Axis.YRot:
                            if (YRotManipulation)
                            {
                                draggedAxis = AxisHandle.Axis.YRot;
                                isDragging = true;
                                lastDragObjectPos = transform.rotation.eulerAngles.y;
                                lastDragMousePos = Input.mousePosition;
                                //Check if the camera is above or below the Y torus
                                if (editorManager.HandleCamera.transform.position.y < transform.position.y)
                                    offsetMultiplier.y *= -1;
                                //Check where the raycast hit the torus. This changes the directions of mouse y and x
                                //This is a little trickier than R and P rotations. The idea is as follows:
                                //Given the camera's forward vector, you can determine which side (left or right) the raycast hit the torus
                                //This comes from taking the perpendicular vector and checking if the raycast hit to the left or right of said vector
                                //This can be done by Crossing the forward vector and the up vector to get a plane. The Dot product then gets you the side of the plane you're on
                                Vector3 targetDirection = hit.point - editorManager.HandleCamera.transform.position;
                                Vector3 perpendicular = Vector3.Cross(editorManager.HandleCamera.transform.forward, targetDirection);
                                float side = Vector3.Dot(perpendicular, Vector3.up);

                                //We only care about inverting the Y multiplier if on the left side
                                if (side < 0f)
                                    offsetMultiplier.y *= -1;
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
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    offsetMultiplier *= editorManager.ShiftSpeedModifier;
                }
                else if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    offsetMultiplier *= 1/editorManager.ShiftSpeedModifier;
                }
                Vector3 pos = transform.position;
                float rot = 0;
                Vector3 startingPoint = lastDragMousePos;
                Vector3 currentPoint = Input.mousePosition;
                float xOffset = (currentPoint - startingPoint).x;
                float yOffset = (currentPoint - startingPoint).y;
                float offset = 0;
                switch (draggedAxis)
                {
                    case AxisHandle.Axis.XPos:
                        //Get the larger offset (either X or Y axis) and use that difference to manipulate on the X axis
                        offset = (mouseAxisModifier == AxisHandle.Axis.XPos ? xOffset * offsetMultiplier.x : yOffset * offsetMultiplier.y) * XYZDragScaleFactor * Time.deltaTime;
                        pos.x = lastDragObjectPos + offset;
                        lastDragObjectPos = pos.x;
                        transform.position = pos;
                        break;
                    case AxisHandle.Axis.YPos:
                        offset = (currentPoint - startingPoint).y * XYZDragScaleFactor * offsetMultiplier.y * Time.deltaTime;
                        pos.y = lastDragObjectPos + offset;
                        lastDragObjectPos = pos.y;
                        transform.position = pos;
                        break;
                    case AxisHandle.Axis.ZPos:
                        //Get the larger offset (either X or Y axis) and use that difference to manipulate on the Z axis
                        offset = (mouseAxisModifier == AxisHandle.Axis.XPos ? xOffset * offsetMultiplier.x : yOffset * offsetMultiplier.y) * XYZDragScaleFactor * Time.deltaTime;
                        pos.z = lastDragObjectPos + offset;
                        lastDragObjectPos = pos.z;
                        transform.position = pos;
                        break;
                    case AxisHandle.Axis.RRot:
                        //Get the larger offset (either X or Y axis) and use that difference to manipulate on the R axis
                        offset = (Mathf.Abs(xOffset) > Mathf.Abs(yOffset) ? xOffset * offsetMultiplier.x : yOffset * offsetMultiplier.y) * RPYDragScaleFactor * Time.deltaTime;
                        rot = lastDragObjectPos - offset;
                        transform.Rotate(0, 0, lastDragObjectPos - rot, Space.World);
                        lastDragObjectPos = rot;
                        break;
                    case AxisHandle.Axis.PRot:
                        //Get the larger offset (either X or Y axis) and use that difference to manipulate on the P axis
                        offset = (Mathf.Abs(xOffset) > Mathf.Abs(yOffset) ? xOffset * offsetMultiplier.x : yOffset * offsetMultiplier.y) * RPYDragScaleFactor * Time.deltaTime;
                        rot = lastDragObjectPos - offset;
                        transform.Rotate(lastDragObjectPos - rot, 0, 0, Space.World);
                        lastDragObjectPos = rot;
                        break;
                    case AxisHandle.Axis.YRot:
                        //Get the larger offset (either X or Y axis) and use that difference to manipulate on the Y axis
                        offset = (Mathf.Abs(xOffset) > Mathf.Abs(yOffset) ? xOffset * offsetMultiplier.x : yOffset * offsetMultiplier.y) * RPYDragScaleFactor * Time.deltaTime;
                        rot = lastDragObjectPos + offset;
                        transform.Rotate(0, lastDragObjectPos - rot, 0, Space.World);
                        lastDragObjectPos = rot;
                        break;
                }
                lastDragMousePos = currentPoint;
                UpdateBridge();
            }
        }
    }

    public void UpdateOutline()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            //Mouse is hovering over UI elements. Let's not let those events pass through to the game world.
            return;
        }
        if (!isSelected)
        {
            Ray uiRay = editorManager.HandleCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //Check if we hit UI elements (Axis handles) first.
            LayerMask UIMask = (1 << LayerMask.NameToLayer("UI"));
            //If we hit an Axis Handle, do nothing. It's own raycast code will deal with its own outline
            if (!Physics.Raycast(uiRay, out hit, 100, UIMask))
            {
                Ray mouseRay = editorManager.MainCamera.ScreenPointToRay(Input.mousePosition);
                LayerMask ObjectMask = ~editorManager.IgnoreLayers;
                if (Physics.Raycast(mouseRay, out hit, 100, ObjectMask))
                {
                    if (hit.transform.gameObject == gameObject)
                    {
                        if (outline == null)
                            ShowOutline(editorManager.HoverOutline);
                    }
                    else
                    {
                        ClearOutline();
                    }
                }
                else
                {
                    ClearOutline();
                }
            }
        }
    }

    private void UpdateBridge()
    {
        GameObject robotBase = editorManager.GetRobotBaseObject();
        if (transform.position.y - (transform.localScale.y / 2) > robotBase.transform.position.y + (robotBase.transform.localScale.y / 2) + editorManager.BridgeSpawnHeight)
        {
            if (bridge == null)
            {
                bridge = Instantiate(editorManager.BridgePrefab);
                bridgeShown = true;
            }
            Vector3 bridgePos = robotBase.transform.position;
            bridgePos.z = transform.position.z;
            bridge.transform.position = bridgePos;

            bridge.GetComponent<Bridge>().SetObjectGO(gameObject);
            bridge.GetComponent<Bridge>().SetDimensions(robotBase.transform.localScale.x, transform.position.y - (transform.localScale.y / 2));
        }
        else
        {
            Destroy(bridge);
            bridgeShown = false;
        }
    }

    public void ShowBridge()
    {
        if (!bridgeShown && bridge != null)
        {
            bridgeShown = true;
            foreach (Transform child in bridge.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    public void HideBridge()
    {
        if (bridgeShown && bridge != null)
        {
            bridgeShown = false;
            foreach(Transform child in bridge.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    public void Die()
    {
        if (bridge != null)
        {
            Destroy(bridge);
        }
        Destroy(gameObject);
    }
}
