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
        Select,
        CameraControl,
        CameraPan
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
    public Mode mode = Mode.Translate;

    //Private Variables
    private GameObject selectedObject;
    private Mode lastMode = Mode.Translate;
    private Vector3 lastCameraMousePos;
    private Quaternion lastCameraRot;
    private Vector3 lastCameraPos;
    //Camera Animation
    private bool isAnimatingCameraPos = false;
    private bool isAnimatingCameraRot = false;
    private float cameraAnimationTargetOffset = 5f;
    private float cameraAnimationLerpTime = 1f;

    void Start()
    {
        XYZHandles.SetActive(false);
        RPYHandles.SetActive(false);
    }

	public void SetSelectedObject(GameObject go)
    {
        if (selectedObject != null)
            selectedObject.GetComponent<Manipulatable>().Deselect();
        selectedObject = go;
        selectedObject.GetComponent<Manipulatable>().Select();
    }

    void Update ()
    {
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
            mode = Mode.Select;
        }

        UpdateSelection();
        UpdateHandles();
        UpdateCameraControl();
	}

    private void UpdateSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(mouseRay, out hit, 100))
            {
                if (hit.transform.gameObject.tag == Manipulatable.TAG)
                {
                    SetSelectedObject(hit.transform.gameObject);
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
            case EditorManager.Mode.Select:
                XPosHandle.SetActive(false);
                YPosHandle.SetActive(false);
                ZPosHandle.SetActive(false);
                RRotHandle.SetActive(false);
                PRotHandle.SetActive(false);
                YRotHandle.SetActive(false);
                break;
        }

        XYZHandles.transform.position = selectedObject.transform.position;
        RPYHandles.transform.position = selectedObject.transform.position;
    }

    private void UpdateCameraControl()
    {
        if (isAnimatingCameraPos || isAnimatingCameraRot)
        {
            //Camera is being animated, let's not allow for interference.
            return;
        }
        if (Input.GetMouseButtonDown(1))
        {
            //Don't track other camera mode as mode to return to
            if (mode != Mode.CameraPan)
                lastMode = mode;
            mode = Mode.CameraControl;
            lastCameraPos = Camera.main.transform.position;
            lastCameraRot = Camera.main.transform.rotation;
            lastCameraMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            mode = lastMode;
        }
        else if (Input.GetMouseButtonDown(2))
        {
            //Don't track other camera mode as mode to return to
            if (mode != Mode.CameraControl)
                lastMode = mode;
            mode = Mode.CameraPan;
            lastCameraPos = Camera.main.transform.position;
            lastCameraMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(2))
        {
            mode = lastMode;
        }
        else
        {
            if (mode == Mode.CameraControl)
            {
                Vector3 rotOffset = new Vector3(-(Input.mousePosition - lastCameraMousePos).y, (Input.mousePosition - lastCameraMousePos).x, 0);
                Vector3 cameraRot = rotOffset * CameraRotScaleFactor * Time.deltaTime;
                Camera.main.transform.rotation = Quaternion.Euler(lastCameraRot.eulerAngles + cameraRot);

                lastCameraMousePos = Input.mousePosition;
                lastCameraRot = Camera.main.transform.rotation;

                Vector3 cameraPos = Camera.main.transform.position;

                float posSpeed = CameraPosMoveSpeed;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    posSpeed *= 10;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    cameraPos += Camera.main.transform.forward * posSpeed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    cameraPos += -Camera.main.transform.forward * posSpeed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    cameraPos += -Camera.main.transform.right * posSpeed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    cameraPos += Camera.main.transform.right * posSpeed * Time.deltaTime;
                }

                Camera.main.transform.position = cameraPos;
                lastCameraPos = cameraPos;
            }
            else if (mode == Mode.CameraPan)
            {
                Vector3 posOffset = Input.mousePosition - lastCameraMousePos;
                Vector3 cameraPos = posOffset * CameraPosMoveSpeed * Time.deltaTime;
                Camera.main.transform.position += cameraPos;

                lastCameraMousePos = Input.mousePosition;
                lastCameraPos = Camera.main.transform.position;
            }
        }
    }

    public IEnumerator MoveCameraPosCoroutine(Vector3 target)
    {
        float lerpTime = cameraAnimationLerpTime;
        float currentLerpTime = 0f;

        Vector3 startPos = Camera.main.transform.localPosition;

        //Pretend we are above the sensor, so that we get the heading we want
        Vector3 heading = (target - startPos).normalized;
        //Calculate how far away we want to be from the target on that heading
        Vector3 endPos = target - (heading * cameraAnimationTargetOffset);
        float angle = Vector3.Angle(endPos - target, Vector3.up);
        //endPos = Quaternion.AngleAxis(50f - angle, Vector3.forward) * endPos;
        endPos = Quaternion.Euler(50f - angle, 0, 0) * endPos;
        Debug.Log(Vector3.Angle(endPos - target, Vector3.up));
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
            Camera.main.transform.localPosition = Vector3.Lerp(startPos, endPos, t);

            yield return null;
        }
        
        isAnimatingCameraPos = false;
    }

    public IEnumerator MoveCameraRotCoroutine(Vector3 target)
    {
        float lerpTime = cameraAnimationLerpTime;
        float currentLerpTime = 0f;

        isAnimatingCameraRot = true;

        Vector3 relativePos =  target - Camera.main.transform.localPosition;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        Quaternion current = Camera.main.transform.localRotation;

        while (lerpTime > 0)
        {
            lerpTime -= Time.deltaTime;
            currentLerpTime += Time.deltaTime;

            if (currentLerpTime > lerpTime)
                currentLerpTime = lerpTime;

            relativePos = target - Camera.main.transform.position;
            rotation = Quaternion.LookRotation(relativePos);

            float t = currentLerpTime / lerpTime;
            //Lerp equation pulled from http://stackoverflow.com/questions/32208980/use-lerp-position-and-slerp-rotation-together-unity/32224625#32224625
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            Camera.main.transform.localRotation = Quaternion.Slerp(current, rotation, t);

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

    public void AnimateCameraToSelection()
    {
        StartCoroutine(MoveCameraPosCoroutine(selectedObject.transform.position));
        StartCoroutine(MoveCameraRotCoroutine(selectedObject.transform.position));
    }

    public Mode GetMode()
    {
        return mode;
    }
}
