using UnityEngine;
using System.Collections;

public class EditorManager : MonoBehaviour {

    //Public Static Variables
    public static string TAG = "EditorManager";

    //Public Structures
    public enum Mode
    {
        Translate,
        Rotate,
        CameraControl,
        Orbit
    }

    //Public Variables
    public GameObject PartsContainer;
    public GameObject XYZHandles;
    public GameObject XPosHandle;
    public GameObject YPosHandle;
    public GameObject ZPosHandle;
    public GameObject RPYHandles;
    public GameObject RRotHandle;
    public GameObject PRotHandle;
    public GameObject YRotHandle;

    public float CameraRotScaleFactor = 1f;
    public float CameraPosMoveSpeed= 1f;
    public float CameraScrollSpeed = 1f;
    public float CameraOrbitSpeed = 1f;
    public float HandleCameraDistance = 5f;
    public Mode mode = Mode.Translate;
    //Cameras
    public Camera MainCamera;
    public Camera HandleCamera;

    //Private Variables
    private GameObject selectedObject;
    private Mode lastMode = Mode.Translate;
    private Vector3 lastCameraMousePos;
    private Quaternion lastCameraRot;
    private UIManager uiManager;
    //Camera Animation
    private bool isAnimatingCameraPos = false;
    private bool isAnimatingCameraRot = false;
    private float cameraAnimationTargetOffset = 5f;
    private float cameraAnimationLerpTime = 1f;
    private float cameraPosYSensorRelative = 3.5f;

    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag(UIManager.TAG).GetComponent<UIManager>();
        XYZHandles.SetActive(false);
        RPYHandles.SetActive(false);
    }

	public void SetSelectedObject(GameObject go)
    {
        if (selectedObject != null)
            selectedObject.GetComponent<Manipulatable>().Deselect();
        selectedObject = go;
        if (selectedObject != null)
        {
            selectedObject.GetComponent<Manipulatable>().Select();
            uiManager.SelectPart(selectedObject);
        }
        else
        {
            uiManager.Deselect();
        }
    }

    void Update ()
    {
        //Toggle modes
        if (Input.GetKeyDown(KeyCode.T))
        {
            mode = Mode.Translate;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            mode = Mode.Rotate;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetSelectedObject(null);
        }

        UpdateSelection();
        UpdateHandles();
        UpdateCameraControl();

        //Regardless of where the current camera is, we want to update the Handle camera's position so that the handles are always the same size
        if (selectedObject)
        {
            HandleCamera.transform.rotation = MainCamera.transform.rotation;
            Vector3 heading = (selectedObject.transform.position - MainCamera.transform.position).normalized;
            Vector3 endPos = selectedObject.transform.position - (heading * HandleCameraDistance);
            HandleCamera.transform.position = endPos;
        }
    }

    private void UpdateSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                //Mouse is hovering over UI elements. Let's not let those events pass through to the game world.
                return;
            }
            Ray uiRay = HandleCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //Check if we hit UI elements (Axis handles) first.
            //If we hit an axis handle, do nothing since the Manipulatable code will handle axis handles
            LayerMask UIMask = (1 << LayerMask.NameToLayer("UI"));
            if (!Physics.Raycast(uiRay, out hit, 100, UIMask))
            {
                Ray mouseRay = MainCamera.ScreenPointToRay(Input.mousePosition);
                LayerMask ObjectMask = ~(Physics.IgnoreRaycastLayer | (1 << LayerMask.NameToLayer("UI")));
                if (Physics.Raycast(mouseRay, out hit, 100, ObjectMask))
                {
                    if (hit.transform.gameObject.tag == Manipulatable.TAG)
                    {
                        SetSelectedObject(hit.transform.gameObject);
                    }
                }
            }
        }
    }

    private void UpdateHandles()
    {
        if (selectedObject == null)
        {
            XYZHandles.SetActive(false);
            RPYHandles.SetActive(false);
            return;
        }
        XYZHandles.SetActive(true);
        RPYHandles.SetActive(true);
        Manipulatable part = selectedObject.GetComponent<Manipulatable>();
        switch (mode)
        {
            case EditorManager.Mode.Translate:
                XPosHandle.SetActive(part.XPosManipulation);
                YPosHandle.SetActive(part.YPosManipulation);
                ZPosHandle.SetActive(part.ZPosManipulation);
                RRotHandle.SetActive(false);
                PRotHandle.SetActive(false);
                YRotHandle.SetActive(false);
                break;
            case EditorManager.Mode.Rotate:
                XPosHandle.SetActive(false);
                YPosHandle.SetActive(false);
                ZPosHandle.SetActive(false);
                RRotHandle.SetActive(part.RRotManipulation);
                PRotHandle.SetActive(part.PRotManipulation);
                YRotHandle.SetActive(part.YRotManipulation);
                break;
        }

        XYZHandles.transform.position = selectedObject.transform.position;
        RPYHandles.transform.position = selectedObject.transform.position;

        //Update RPY Rotations
        /*
        RRotHandle.transform.LookAt(HandleCamera.transform.position);
        Vector3 rRotRot = RRotHandle.transform.rotation.eulerAngles;
        rRotRot.x = 0;
        rRotRot.y = 0;
        rRotRot.z = 0;
        RRotHandle.transform.rotation = Quaternion.Euler(rRotRot);
        
        PRotHandle.transform.LookAt(HandleCamera.transform.position);
        Vector3 pRotRot = PRotHandle.transform.rotation.eulerAngles;
        pRotRot.x = 0;
        pRotRot.y = 90;
        pRotRot.z = 0;
        PRotHandle.transform.rotation = Quaternion.Euler(pRotRot);
        */

        YRotHandle.transform.LookAt(HandleCamera.transform.position);
        Vector3 yRotRot = YRotHandle.transform.rotation.eulerAngles;
        yRotRot.x = 90;
        yRotRot.y -= 90;
        yRotRot.z = 0;
        YRotHandle.transform.rotation = Quaternion.Euler(yRotRot);
    }

    private void UpdateCameraControl()
    {
        if (isAnimatingCameraPos || isAnimatingCameraRot)
        {
            //Camera is being animated, let's not allow for interference.
            return;
        }
        //Hold modes
        if (Input.GetMouseButtonDown(1))
        {
            //Don't track other camera mode as mode to return to
            if (mode != Mode.Orbit)
                lastMode = mode;
            mode = Mode.CameraControl;
            lastCameraRot = MainCamera.transform.rotation;
            lastCameraMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            mode = lastMode;
        }
        else if (Input.GetMouseButtonDown(2))
        {
            if (mode != Mode.CameraControl && mode != Mode.Orbit)
                lastMode = mode;
            mode = Mode.Orbit;
            lastCameraRot = MainCamera.transform.rotation;
            lastCameraMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(2))
        {
            mode = lastMode;
        }
        else
        {
            if (mode == Mode.CameraControl) //Right-click (FPS Modifier)
            {
                Vector3 rotOffset = new Vector3(-(Input.mousePosition - lastCameraMousePos).y, (Input.mousePosition - lastCameraMousePos).x, 0);
                Vector3 cameraRot = rotOffset * CameraRotScaleFactor * Time.deltaTime;
                MainCamera.transform.rotation = Quaternion.Euler(lastCameraRot.eulerAngles + cameraRot);

                lastCameraMousePos = Input.mousePosition;
                lastCameraRot = MainCamera.transform.rotation;

                Vector3 cameraPos = MainCamera.transform.position;

                float posSpeed = CameraPosMoveSpeed;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    posSpeed *= 10;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    cameraPos += MainCamera.transform.forward * posSpeed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    cameraPos += -MainCamera.transform.forward * posSpeed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    cameraPos += -MainCamera.transform.right * posSpeed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    cameraPos += MainCamera.transform.right * posSpeed * Time.deltaTime;
                }

                MainCamera.transform.position = cameraPos;
            }
            else if (mode == Mode.Orbit)    //Middle-Click (Orbit modifier)
            {
                if (selectedObject != null)
                {
                    Vector3 posOffset = Input.mousePosition - lastCameraMousePos;
                    Vector3 cameraPos = posOffset * CameraOrbitSpeed * Time.deltaTime;

                    //Rotate around the up axis for the mouse x delta
                    MainCamera.transform.RotateAround(selectedObject.transform.position, Vector3.up, cameraPos.x);
                    //For the mouse y delta, we need to find a vector perpendicular to the camera's forward. That will give us the correct axis on which to rotate
                    Vector3 perpendicular = Vector3.Cross(MainCamera.transform.forward, Vector3.up);
                    MainCamera.transform.RotateAround(selectedObject.transform.position, perpendicular, cameraPos.y);
                    MainCamera.transform.LookAt(selectedObject.transform.position);

                    lastCameraMousePos = Input.mousePosition;
                }
            }
            else    //No camera modifiers held
            {
                float scrollVal = Input.GetAxis("Mouse ScrollWheel");
                MainCamera.transform.position += MainCamera.transform.forward * scrollVal * CameraScrollSpeed * Time.deltaTime;
            }
        }
    }

    public Vector3 RotatePointAboutPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = Quaternion.Euler(angles) * (point - pivot);
        point = dir + pivot;
        return point;
    }

    public IEnumerator MoveCameraPosCoroutine(Vector3 target)
    {
        float lerpTime = cameraAnimationLerpTime;
        float currentLerpTime = 0f;

        Vector3 startPos = MainCamera.transform.localPosition;

        //Pretend we are above the sensor, so that we get the heading we want
        Vector3 heading = (target - startPos).normalized;
        //Calculate how far away we want to be from the target on that heading
        Vector3 endPos = target - (heading * cameraAnimationTargetOffset);
        //Position the camera above the sensor so that we are always looking down on it
        if (endPos.y < target.y + cameraPosYSensorRelative)
        {
            //SIDE-EFFECT: Multiple double-clicks reposition the camera a bit - the new Quaternion calculations will be slightly different (since we manually changed the y position)
            //             This is something we can live with.
            endPos.y = target.y + cameraPosYSensorRelative;
        }

        isAnimatingCameraPos = true;

        while (lerpTime > 0)
        {
            lerpTime -= Time.deltaTime;
            currentLerpTime += Time.deltaTime;

            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            float t = currentLerpTime / lerpTime;
            //Lerp equation pulled from http://stackoverflow.com/questions/32208980/use-lerp-position-and-slerp-rotation-together-unity/32224625#32224625
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            MainCamera.transform.localPosition = Vector3.Lerp(startPos, endPos, t);

            yield return null;
        }
        
        isAnimatingCameraPos = false;
    }

    public IEnumerator MoveCameraRotCoroutine(Vector3 target)
    {
        float lerpTime = cameraAnimationLerpTime;
        float currentLerpTime = 0f;

        isAnimatingCameraRot = true;

        Vector3 relativePos =  target - MainCamera.transform.localPosition;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        Quaternion current = MainCamera.transform.localRotation;

        while (lerpTime > 0)
        {
            lerpTime -= Time.deltaTime;
            currentLerpTime += Time.deltaTime;

            if (currentLerpTime > lerpTime)
                currentLerpTime = lerpTime;

            relativePos = target - MainCamera.transform.position;
            rotation = Quaternion.LookRotation(relativePos);

            float t = currentLerpTime / lerpTime;
            //Lerp equation pulled from http://stackoverflow.com/questions/32208980/use-lerp-position-and-slerp-rotation-together-unity/32224625#32224625
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            MainCamera.transform.localRotation = Quaternion.Slerp(current, rotation, t);

            yield return null;
        }
        
        isAnimatingCameraRot = false;
    }

    public GameObject AddPart(GameObject go, string name)
    {
        GameObject sensor = Instantiate<GameObject>(go);
        sensor.name = name;
        sensor.transform.position = Vector3.zero;
        sensor.transform.SetParent(PartsContainer.transform);
        return sensor;
    }

    public void SelectPart(GameObject go)
    {
        Manipulatable sensor = go.GetComponent<Manipulatable>();
        if (sensor != null)
        {
            if (selectedObject != go)
                SetSelectedObject(go);
            else
                AnimateCameraToSelection();
        }
    }

    public void AnimateCameraToSelection(bool moveCamera = true)
    {
        if (moveCamera)
            StartCoroutine(MoveCameraPosCoroutine(selectedObject.transform.position));
        StartCoroutine(MoveCameraRotCoroutine(selectedObject.transform.position));
    }

    public Mode GetMode()
    {
        return mode;
    }
}
