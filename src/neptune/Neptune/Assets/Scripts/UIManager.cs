using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor;

public class UIManager : MonoBehaviour
{

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
    public Button LanguageButtonPrefab;
    public Transform LanguageButtonsContent;
    public GameObject LanguagesPanel;
    public GameObject LanguagesPanelMask;
    public Text LanguagesButtonText;
    public InputField CustomCubeoidNameText;
    public Dropdown CustomCubeoidColorDropdown;
    public InputField CustomCubeoidWidthText;
    public InputField CustomCubeoidHeightText;
    public InputField CustomCubeoidDepthText;
    public float PanelSpeed;
    public Text ModeText;
    public Button DeleteSelectedObjectButton;
    public Color DefaultBackColor;
    
    public GameObject TestGO;
    public GameObject ErrorGO;
    public GameObject InvisibleGO;

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
    private Vector3 hiddenLanguagesPanelPos;
    private Vector3 shownLanguagesPanelPos;
    private bool languagesPanelShown = false;

    void Start()
    {
        dbManager = new DBManager();
        dbManager.GetSensorList(this, TestGO, ErrorGO, InvisibleGO);
        editorManager = GameObject.FindGameObjectWithTag(EditorManager.TAG).GetComponent<EditorManager>();
        hiddenResetAxesPanelPos = ResetAxesPanel.transform.position;
        shownResetAxesPanelPos = ResetAxesPanelMask.transform.position;
        hiddenCustomCubeoidPanelPos = CustomCubeoidPanel.transform.position;
        shownCustomCubeoidPanelPos = CustomCubeoidPanelMask.transform.position;
        hiddenLanguagesPanelPos = LanguagesPanel.transform.position;
        shownLanguagesPanelPos = LanguagesPanelMask.transform.position;

        List<SmartLocalization.SmartCultureInfo> cultures = SmartLocalization.LanguageManager.Instance.GetSupportedLanguages();
        foreach (SmartLocalization.SmartCultureInfo culture in cultures)
        {
            Button b = Instantiate(LanguageButtonPrefab);
            b.GetComponent<LanguageButton>().culture = culture;
            b.GetComponentInChildren<Text>().text = b.GetComponent<LanguageButton>().GetString();
            b.onClick.AddListener(() => { SetLanguage(b.GetComponent<LanguageButton>()); });
            b.gameObject.transform.SetParent(LanguageButtonsContent);
        }

        UpdateColorDropdownOptions();
    }

    private void UpdateColorDropdownOptions()
    {
        CustomCubeoidColorDropdown.ClearOptions();
        List<string> colorOptions = new List<string>();
        colorOptions.Add(SmartLocalization.LanguageManager.Instance.GetTextValue("Red"));
        colorOptions.Add(SmartLocalization.LanguageManager.Instance.GetTextValue("Green"));
        colorOptions.Add(SmartLocalization.LanguageManager.Instance.GetTextValue("Blue"));
        colorOptions.Add(SmartLocalization.LanguageManager.Instance.GetTextValue("Yellow"));
        colorOptions.Add(SmartLocalization.LanguageManager.Instance.GetTextValue("Cyan"));
        colorOptions.Add(SmartLocalization.LanguageManager.Instance.GetTextValue("Magenta"));
        colorOptions.Add(SmartLocalization.LanguageManager.Instance.GetTextValue("Black"));
        colorOptions.Add(SmartLocalization.LanguageManager.Instance.GetTextValue("White"));
        CustomCubeoidColorDropdown.AddOptions(colorOptions);
    }

    void Update()
    {
        ModeText.text = SmartLocalization.LanguageManager.Instance.GetTextValue("Mode." + editorManager.GetMode().ToString());
        if (resetAxesPanelShown)
            ResetAxesPanel.transform.position = Vector3.MoveTowards(ResetAxesPanel.transform.position, shownResetAxesPanelPos, PanelSpeed * Time.deltaTime);
        else
            ResetAxesPanel.transform.position = Vector3.MoveTowards(ResetAxesPanel.transform.position, hiddenResetAxesPanelPos, PanelSpeed * Time.deltaTime);

        if (customCubeoidPanelShown)
            CustomCubeoidPanel.transform.position = Vector3.MoveTowards(CustomCubeoidPanel.transform.position, shownCustomCubeoidPanelPos, PanelSpeed * Time.deltaTime);
        else
            CustomCubeoidPanel.transform.position = Vector3.MoveTowards(CustomCubeoidPanel.transform.position, hiddenCustomCubeoidPanelPos, PanelSpeed * Time.deltaTime);

        if (languagesPanelShown)
            LanguagesPanel.transform.position = Vector3.MoveTowards(LanguagesPanel.transform.position, shownLanguagesPanelPos, PanelSpeed * Time.deltaTime);
        else
            LanguagesPanel.transform.position = Vector3.MoveTowards(LanguagesPanel.transform.position, hiddenLanguagesPanelPos, PanelSpeed * Time.deltaTime);

        DeleteSelectedObjectButton.interactable = editorManager.GetSelectedObject() != null && editorManager.GetSelectedObject() != editorManager.GetRobotBaseObject();
    }

    public void AddSensor(string text, GameObject go, float scale = 1)
    {
        GameObject sensorText = Instantiate(TextPrefab);
        sensorText.name = text;
        sensorText.GetComponentInChildren<Text>().text = text;
        sensorText.transform.SetParent(SensorsContent.transform);
        sensorText.GetComponent<PartText>().SetGO(go);
        sensorText.GetComponent<PartText>().SetScale(scale);
        sensorText.GetComponent<PartText>().SetState(PartText.State.AddNewSensor);
    }

    public void AddPart(string text, GameObject go, float scale = 1)
    {
        GameObject partText = Instantiate(TextPrefab);
        partText.name = text;
        partText.GetComponentInChildren<Text>().text = text;
        go.transform.localScale *= scale;
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
            c.normalColor = DefaultBackColor;
            selectedPart.colors = c;
        }
        selectedPart = null;
    }

    public void TryDeleteSelectedObject()
    {
        if (selectedPart != null)
        {
            string message = SmartLocalization.LanguageManager.Instance.GetTextValue("DeletingSensor.AreYouSure");
            DialogManager.instance.ShowDialog(message + " \"" + selectedPart.name + "\"?", SmartLocalization.LanguageManager.Instance.GetTextValue("DeletingSensor"), DialogManager.ButtonType.YesNo, DeleteSelectedObject);
        }
    }

    private void DeleteSelectedObject()
    {
        if (selectedPart != null)
        {
            selectedPart.GetComponent<PartText>().DestroyPart();
        }
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

    public void ShowLanguagesPanel()
    {
        languagesPanelShown = true;
    }

    public void HideLanguagesPanel()
    {
        languagesPanelShown = false;
    }

    public void SetLanguage(LanguageButton langButton)
    {
        SmartLocalization.LanguageManager.Instance.ChangeLanguage(langButton.culture);
        UpdateColorDropdownOptions();
        LanguagesButtonText.text = langButton.GetString();
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
        if (CustomCubeoidColorDropdown.captionText.text.Equals(SmartLocalization.LanguageManager.Instance.GetTextValue("Red")))
        {
            return Color.red;
        }
        if (CustomCubeoidColorDropdown.captionText.text.Equals(SmartLocalization.LanguageManager.Instance.GetTextValue("Green")))
        {
            return Color.green;
        }
        if (CustomCubeoidColorDropdown.captionText.text.Equals(SmartLocalization.LanguageManager.Instance.GetTextValue("Blue")))
        {
            return Color.blue;
        }
        if (CustomCubeoidColorDropdown.captionText.text.Equals(SmartLocalization.LanguageManager.Instance.GetTextValue("Yellow")))
        {
            return Color.yellow;
        }
        if (CustomCubeoidColorDropdown.captionText.text.Equals(SmartLocalization.LanguageManager.Instance.GetTextValue("Cyan")))
        {
            return Color.cyan;
        }
        if (CustomCubeoidColorDropdown.captionText.text.Equals(SmartLocalization.LanguageManager.Instance.GetTextValue("Magenta")))
        {
            return Color.magenta;
        }
        if (CustomCubeoidColorDropdown.captionText.text.Equals(SmartLocalization.LanguageManager.Instance.GetTextValue("Black")))
        {
            return Color.black;
        }
        if (CustomCubeoidColorDropdown.captionText.text.Equals(SmartLocalization.LanguageManager.Instance.GetTextValue("White")))
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
    }

    public void SelectHuskyBase()
    {
        EditorManager.RobotBase robotBase = EditorManager.RobotBase.Husky;
        editorManager.SelectRobotBase(robotBase);
        RobotBaseSelectPanel.transform.parent.gameObject.SetActive(false);
    }

    public void SelectGrizzlyBase()
    {
        EditorManager.RobotBase robotBase = EditorManager.RobotBase.Grizzly;
        editorManager.SelectRobotBase(robotBase);
        RobotBaseSelectPanel.transform.parent.gameObject.SetActive(false);
    }
}
