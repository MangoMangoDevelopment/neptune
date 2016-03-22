using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    //Public Static Variables
    public static string TAG = "UIManager";

    //Public Variables
    public GameObject SensorsContent;
    public GameObject PartsContent;
    public GameObject TextPrefab;
    public GameObject RobotBaseSelectPanel;
    public GameObject ResetAxesPanel;
    public GameObject ResetAxesPanelMask;
    public GameObject CustomCubeoidPanel;
    public GameObject CustomCubeoidPanelMask;
    public InputField CustomCubeoidNameText;
    public Dropdown CustomCubeoidColorDropdown;
    public InputField CustomCubeoidWidthText;
    public InputField CustomCubeoidHeightText;
    public InputField CustomCubeoidDepthText;
    public Button RobotBaseButton;
    public float PanelSpeed;
    public Text ModeText;

    //TODO: Remove this. This should be superseeded by a combination of Brian and Amber's work. See DBManager.GetSensorList() for more info.
    public GameObject TestGO;

    //Private Variables
    private DBManager dbManager;
    private EditorManager editorManager;
    private Button selectedPart;
    private Vector3 hiddenResetAxesPanelPos;
    private Vector3 shownResetAxesPanelPos;
    private bool resetAxesPanelShown = false;
    private Vector3 hiddenCustomCubeoidPanelPos;
    private Vector3 shownCustomCubeoidPanelPos;
    private bool customCubeoidPanelShown = false;

    void Start()
    {
        dbManager = new DBManager();
        dbManager.GetSensorList(this, TestGO);
        editorManager = GameObject.FindGameObjectWithTag(EditorManager.TAG).GetComponent<EditorManager>();
        hiddenResetAxesPanelPos = ResetAxesPanel.transform.position;
        shownResetAxesPanelPos = ResetAxesPanelMask.transform.position;
        hiddenCustomCubeoidPanelPos = CustomCubeoidPanel.transform.position;
        shownCustomCubeoidPanelPos = CustomCubeoidPanelMask.transform.position;
    }

    void Update()
    {
        ModeText.text = "Mode: " + editorManager.GetMode().ToString();
        if (resetAxesPanelShown)
            ResetAxesPanel.transform.position = Vector3.MoveTowards(ResetAxesPanel.transform.position, shownResetAxesPanelPos, PanelSpeed * Time.deltaTime);
        else
            ResetAxesPanel.transform.position = Vector3.MoveTowards(ResetAxesPanel.transform.position, hiddenResetAxesPanelPos, PanelSpeed * Time.deltaTime);

        if (customCubeoidPanelShown)
            CustomCubeoidPanel.transform.position = Vector3.MoveTowards(CustomCubeoidPanel.transform.position, shownCustomCubeoidPanelPos, PanelSpeed * Time.deltaTime);
        else
            CustomCubeoidPanel.transform.position = Vector3.MoveTowards(CustomCubeoidPanel.transform.position, hiddenCustomCubeoidPanelPos, PanelSpeed * Time.deltaTime);
    }

    public void AddSensor(string text, GameObject go, float scale = 1)
    {
        GameObject sensorText = Instantiate<GameObject>(TextPrefab);
        sensorText.name = text;
        sensorText.GetComponentInChildren<Text>().text = text;
        sensorText.transform.SetParent(SensorsContent.transform);
        go.transform.localScale *= scale;
        sensorText.GetComponent<PartText>().SetGO(go);
        sensorText.GetComponent<PartText>().SetState(PartText.State.AddNewSensor);
    }

    public void AddPart(string text, GameObject go)
    {
        GameObject partText = Instantiate<GameObject>(TextPrefab);
        partText.name = text;
        partText.GetComponentInChildren<Text>().text = text;
        partText.transform.SetParent(PartsContent.transform);
        partText.GetComponent<PartText>().SetGO(go);
        partText.GetComponent<PartText>().SetState(PartText.State.SelectExistingSensor);
    }

    public void SelectPart(Button part)
    {
        Deselect();
        selectedPart = part;
        ColorBlock c2 = selectedPart.colors;
        c2.normalColor = Color.gray;
        selectedPart.colors = c2;
    }
    
    public void SelectPart(GameObject go)
    {
        foreach (Transform part in PartsContent.transform)
        {
            if (part.GetComponent<PartText>().GetGO() == go)
            {
                SelectPart(part.GetComponent<Button>());
                break;
            }
        }
    }

    public void Deselect()
    {
        if (selectedPart != null)
        {
            ColorBlock c = selectedPart.colors;
            c.normalColor = Color.white;
            selectedPart.colors = c;
        }
        selectedPart = null;
    }

    public void ShowResetAxesPanel()
    {
        resetAxesPanelShown = true;
    }

    public void HideResetAxesPanel()
    {
        resetAxesPanelShown = false;
    }

    public void ShowCustomCubeoidPanel()
    {
        customCubeoidPanelShown = true;
    }

    public void HideCustomCubeoidPanel()
    {
        customCubeoidPanelShown = false;
    }

    public void ResetXPosAxis()
    {
        editorManager.GetSelectedObject().GetComponent<Manipulatable>().ResetAxis(AxisHandle.Axis.XPos);
    }

    public void ResetYPosAxis()
    {
        editorManager.GetSelectedObject().GetComponent<Manipulatable>().ResetAxis(AxisHandle.Axis.YPos);
    }

    public void ResetZPosAxis()
    {
        editorManager.GetSelectedObject().GetComponent<Manipulatable>().ResetAxis(AxisHandle.Axis.ZPos);
    }

    public void ResetRRotAxis()
    {
        editorManager.GetSelectedObject().GetComponent<Manipulatable>().ResetAxis(AxisHandle.Axis.RRot);
    }

    public void ResetPRotAxis()
    {
        editorManager.GetSelectedObject().GetComponent<Manipulatable>().ResetAxis(AxisHandle.Axis.PRot);
    }

    public void ResetYRotAxis()
    {
        editorManager.GetSelectedObject().GetComponent<Manipulatable>().ResetAxis(AxisHandle.Axis.YRot);
    }

    private Color GetCubeoidDropdownColor()
    {
        if (CustomCubeoidColorDropdown.captionText.text.Equals("Red"))
        {
            return Color.red;
        }
        if (CustomCubeoidColorDropdown.captionText.text.Equals("Green"))
        {
            return Color.green;
        }
        if (CustomCubeoidColorDropdown.captionText.text.Equals("Blue"))
        {
            return Color.blue;
        }
        if (CustomCubeoidColorDropdown.captionText.text.Equals("Yellow"))
        {
            return Color.yellow;
        }
        if (CustomCubeoidColorDropdown.captionText.text.Equals("Cyan"))
        {
            return Color.cyan;
        }
        if (CustomCubeoidColorDropdown.captionText.text.Equals("Magenta"))
        {
            return Color.magenta;
        }
        if (CustomCubeoidColorDropdown.captionText.text.Equals("Black"))
        {
            return Color.black;
        }
        if (CustomCubeoidColorDropdown.captionText.text.Equals("White"))
        {
            return Color.white;
        }
        return Color.gray;
    }

    public void CreateCustomCubeoid()
    {
        bool valid = true;
        if (CustomCubeoidNameText.text.Equals(""))
        {
            ColorBlock c = CustomCubeoidNameText.colors;
            c.normalColor = Color.red;
            CustomCubeoidNameText.colors = c;
            valid = false;
        }
        else
        {
            ColorBlock c = CustomCubeoidNameText.colors;
            c.normalColor = Color.white;
            CustomCubeoidNameText.colors = c;
        }
        if (CustomCubeoidWidthText.text.Equals(""))
        {
            ColorBlock c = CustomCubeoidWidthText.colors;
            c.normalColor = Color.red;
            CustomCubeoidWidthText.colors = c;
            valid = false;
        }
        else
        {
            ColorBlock c = CustomCubeoidWidthText.colors;
            c.normalColor = Color.white;
            CustomCubeoidWidthText.colors = c;
        }
        if (CustomCubeoidHeightText.text.Equals(""))
        {
            ColorBlock c = CustomCubeoidHeightText.colors;
            c.normalColor = Color.red;
            CustomCubeoidHeightText.colors = c;
            valid = false;
        }
        else
        {
            ColorBlock c = CustomCubeoidHeightText.colors;
            c.normalColor = Color.white;
            CustomCubeoidHeightText.colors = c;
        }
        if (CustomCubeoidDepthText.text.Equals(""))
        {
            ColorBlock c = CustomCubeoidDepthText.colors;
            c.normalColor = Color.red;
            CustomCubeoidDepthText.colors = c;
            valid = false;
        }
        else
        {
            ColorBlock c = CustomCubeoidDepthText.colors;
            c.normalColor = Color.white;
            CustomCubeoidDepthText.colors = c;
        }

        if (valid)
        {
            customCubeoidPanelShown = false;
            GameObject cubeoid = editorManager.CreateCustomCubeoid(CustomCubeoidNameText.text, GetCubeoidDropdownColor(), float.Parse(CustomCubeoidWidthText.text), float.Parse(CustomCubeoidHeightText.text), float.Parse(CustomCubeoidDepthText.text));
            AddPart(CustomCubeoidNameText.text, cubeoid);
            CustomCubeoidNameText.text = "";
            CustomCubeoidWidthText.text = "";
            CustomCubeoidHeightText.text = "";
            CustomCubeoidDepthText.text = "";
        }
    }

    public void SelectJackalBase()
    {
        EditorManager.RobotBase robotBase = EditorManager.RobotBase.Jackal;
        editorManager.SelectRobotBase(robotBase);
        RobotBaseSelectPanel.transform.parent.gameObject.SetActive(false);
        RobotBaseButton.GetComponentInChildren<Text>().text = robotBase.ToString();
    }

    public void SelectHuskyBase()
    {
        EditorManager.RobotBase robotBase = EditorManager.RobotBase.Husky;
        editorManager.SelectRobotBase(robotBase);
        RobotBaseSelectPanel.transform.parent.gameObject.SetActive(false);
        RobotBaseButton.GetComponentInChildren<Text>().text = robotBase.ToString();
    }

    public void SelectGrizzlyBase()
    {
        EditorManager.RobotBase robotBase = EditorManager.RobotBase.Grizzly;
        editorManager.SelectRobotBase(robotBase);
        RobotBaseSelectPanel.transform.parent.gameObject.SetActive(false);
        RobotBaseButton.GetComponentInChildren<Text>().text = robotBase.ToString();
    }

    public void SelectRobotBaseObject()
    {
        editorManager.SelectPart(editorManager.GetRobotBaseObject());
    }
}
