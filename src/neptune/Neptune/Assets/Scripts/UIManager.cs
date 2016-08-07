using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{

    //Public Static Variables
    public static string TAG = "UIManager";

    //Public Variables
    public GameObject SensorsContent;
    public GameObject PartsContent;
    public GameObject TextPrefab;
    public GameObject RobotBaseSelectPanel;
    public GameObject LoadingSensorPanel;
    public GameObject ResetAxesPanel;
    public GameObject ResetAxesPanelMask;
    public GameObject CustomCubeoidPanel;
    public GameObject CustomCubeoidPanelMask;
    public GameObject SendEmailPanel;
    public GameObject HelpPanel;
    public GameObject AboutPanel;
    public Button LanguageButtonPrefab;
    public Transform LanguageButtonsContent;
    public InputField CustomCubeoidNameText;
    public Dropdown CustomCubeoidColorDropdown;
    public InputField CustomCubeoidWidthText;
    public InputField CustomCubeoidHeightText;
    public InputField CustomCubeoidDepthText;
    public float PanelSpeed;
    public Button DeleteSelectedObjectButton;
    public InputField EmailFormFirstNameText;
    public InputField EmailFormLastNameText;
    public InputField EmailFormEmailText;
    public InputField EmailFormOrganizationNameText;
    public Dropdown EmailFormStateDropdown;
    public Dropdown EmailFormCountryDropdown;
    public Dropdown EmailFormIndustryDropdown;

    public Color ClearpathBlack;
    public Color ClearpathWhite;
    public Color ClearpathDarkGrey;
    public Color ClearpathGrey;
    public Color ClearpathYellow;

    public GameObject TestGO;
    public GameObject ErrorGO;
    public GameObject InvisibleGO;

    //Cameras
    public List<Camera> ScreenshotCameras;

    //Private Variables
    private DBManager dbManager;
    private EditorManager editorManager;
    private EmailHandler emailer;
    private Button selectedPart;
    private Vector3 hiddenResetAxesPanelPos;
    private Vector3 shownResetAxesPanelPos;
    private bool resetAxesPanelShown = false;
    private Vector3 hiddenCustomCubeoidPanelPos;
    private Vector3 shownCustomCubeoidPanelPos;
    private bool customCubeoidPanelShown = false;
    private ColorBlock invalidColour;
    private ColorBlock validColour;


    void Start()
    {
        emailer = new EmailHandler();
        dbManager = new DBManager();
        dbManager.GetSensorList(this, TestGO, ErrorGO, InvisibleGO);
        editorManager = GameObject.FindGameObjectWithTag(EditorManager.TAG).GetComponent<EditorManager>();

        List<SmartLocalization.SmartCultureInfo> cultures = SmartLocalization.LanguageManager.Instance.GetSupportedLanguages();
        bool firstLanguage = true;
        foreach (SmartLocalization.SmartCultureInfo culture in cultures)
        {
            Button b = Instantiate(LanguageButtonPrefab);
            b.GetComponent<LanguageButton>().culture = culture;
            if (firstLanguage)
            {
                firstLanguage = false;
                b.GetComponent<UIClearpathButton>().normalTextColor = ClearpathYellow;
            }
            else
            {
                b.GetComponent<UIClearpathButton>().normalTextColor = ClearpathGrey;
            }
            b.GetComponent<UIClearpathButton>().OnPointerExit(null);    // Trigger the color change
            b.GetComponent<UIClearpathButton>().highlightTextColor = ClearpathYellow;
            b.GetComponentInChildren<Text>().text = b.GetComponent<LanguageButton>().GetString().ToUpper();
            b.onClick.AddListener(() => { SetLanguage(b.GetComponent<LanguageButton>()); });
            b.gameObject.transform.SetParent(LanguageButtonsContent);
        }

        invalidColour = EmailFormEmailText.colors;
        invalidColour.normalColor = Color.red;
        validColour = EmailFormEmailText.colors;

        UpdateColorDropdownOptions();
        UpdateEmailFormDropdownOptions();
    }

    public void ShowLoadingPanel()
    {
        LoadingSensorPanel.SetActive(true);
    }

    public void HideLoadingPanel()
    {
        LoadingSensorPanel.SetActive(false);
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

    private void UpdateEmailFormDropdownOptions()
    {
        // This is so that the email form provides the exact same options as that of the following:
        // www.clearpathrobotics.com/jackal-user-manual/
        // Hopefully, we can find out if they get their lists from a Web API or something we can hit up
        //   so that we don't have to harcode all this in here...
        EmailFormStateDropdown.ClearOptions();
        List<string> stateOptions = new List<string>();
        stateOptions.Add("");
        stateOptions.Add(SmartLocalization.LanguageManager.Instance.GetTextValue("Form.NA"));
        stateOptions.Add("Alabama");
        stateOptions.Add("Alaska");
        stateOptions.Add("Alberta");
        stateOptions.Add("Arizona");
        stateOptions.Add("Arkansas");
        stateOptions.Add("British Columbia");
        stateOptions.Add("California");
        stateOptions.Add("Colorado");
        stateOptions.Add("Connecticut");
        stateOptions.Add("Delaware");
        stateOptions.Add("District of Columbia");
        stateOptions.Add("Florida");
        stateOptions.Add("Georgia");
        stateOptions.Add("Hawaii");
        stateOptions.Add("Idaho");
        stateOptions.Add("Illinois");
        stateOptions.Add("Indiana");
        stateOptions.Add("Iowa");
        stateOptions.Add("Kansas");
        stateOptions.Add("Kentucky");
        stateOptions.Add("Louisiana");
        stateOptions.Add("Maine");
        stateOptions.Add("Manitoba");
        stateOptions.Add("Maryland");
        stateOptions.Add("Massachusetts");
        stateOptions.Add("Michigan");
        stateOptions.Add("Minnesota");
        stateOptions.Add("Mississippi");
        stateOptions.Add("Missouri");
        stateOptions.Add("Montana");
        stateOptions.Add("Nebraska");
        stateOptions.Add("Nevada");
        stateOptions.Add("New Brunswick");
        stateOptions.Add("New Hampshire");
        stateOptions.Add("New Jersey");
        stateOptions.Add("New Mexico");
        stateOptions.Add("New York");
        stateOptions.Add("Newfoundland");
        stateOptions.Add("North Carolina");
        stateOptions.Add("North Dakota");
        stateOptions.Add("Northwest Territories");
        stateOptions.Add("Nova Scotia");
        stateOptions.Add("Nunavut");
        stateOptions.Add("Ohio");
        stateOptions.Add("Oklahoma");
        stateOptions.Add("Ontario");
        stateOptions.Add("Oregon");
        stateOptions.Add("Pennsylvania");
        stateOptions.Add("Prince Edward Island");
        stateOptions.Add("Quebec");
        stateOptions.Add("Rhode Island");
        stateOptions.Add("Saskatchewan");
        stateOptions.Add("South Carolina");
        stateOptions.Add("South Dakota");
        stateOptions.Add("Tennessee");
        stateOptions.Add("Texas");
        stateOptions.Add("Utah");
        stateOptions.Add("Vermont");
        stateOptions.Add("Virginia");
        stateOptions.Add("Washington");
        stateOptions.Add("West Virginia");
        stateOptions.Add("Wisconsin");
        stateOptions.Add("Wyoming");
        stateOptions.Add("Yukon");
        stateOptions.Add("Unknown");
        EmailFormStateDropdown.AddOptions(stateOptions);

        EmailFormCountryDropdown.ClearOptions();
        List<string> countryOptions = new List<string>();
        countryOptions.Add("");
        countryOptions.Add("United States");
        countryOptions.Add("Canada");
        countryOptions.Add("Afghanistan");
        countryOptions.Add("Albania");
        countryOptions.Add("Algeria");
        countryOptions.Add("American Samoa");
        countryOptions.Add("Andorra");
        countryOptions.Add("Angola");
        countryOptions.Add("Anguilla");
        countryOptions.Add("Antartica");
        countryOptions.Add("Antigua and Barbuda");
        countryOptions.Add("Argentina");
        countryOptions.Add("Armenia");
        countryOptions.Add("Aruba");
        countryOptions.Add("Australia");
        countryOptions.Add("Austria");
        countryOptions.Add("Azerbaijan");
        countryOptions.Add("Bahamas");
        countryOptions.Add("Bahrain");
        countryOptions.Add("Bangladesh");
        countryOptions.Add("Barbados");
        countryOptions.Add("Belarus");
        countryOptions.Add("Belgium");
        countryOptions.Add("Belize");
        countryOptions.Add("Benin");
        countryOptions.Add("Bermuda");
        countryOptions.Add("Bhutan");
        countryOptions.Add("Bolivia");
        countryOptions.Add("Bosnia and Herzegovina");
        countryOptions.Add("Botswana");
        countryOptions.Add("Brazil");
        countryOptions.Add("British Indian Ocean Territory");
        countryOptions.Add("British Virgin Islands");
        countryOptions.Add("Brunei");
        countryOptions.Add("Bulgaria");
        countryOptions.Add("Burkina Faso");
        countryOptions.Add("Burundi");
        countryOptions.Add("Cambodia");
        countryOptions.Add("Cameroon");
        countryOptions.Add("Cape Verde");
        countryOptions.Add("Cayman Islands");
        countryOptions.Add("Central African Republic");
        countryOptions.Add("Chad");
        countryOptions.Add("Chile");
        countryOptions.Add("China");
        countryOptions.Add("Christmas Island");
        countryOptions.Add("Cocos (Keeling) Islands");
        countryOptions.Add("Colombia");
        countryOptions.Add("Comoros");
        countryOptions.Add("Congo");
        countryOptions.Add("Cook Islands");
        countryOptions.Add("Costa Rica");
        countryOptions.Add("Croatia");
        countryOptions.Add("Cuba");
        countryOptions.Add("Curacao");
        countryOptions.Add("Cyprus");
        countryOptions.Add("Czech Republic");
        countryOptions.Add("Cote d'Ivoire");
        countryOptions.Add("Democratic Republic of the Congo");
        countryOptions.Add("Denmark");
        countryOptions.Add("Djibouti");
        countryOptions.Add("Dominica");
        countryOptions.Add("Dominican Republic");
        countryOptions.Add("Ecuador");
        countryOptions.Add("Egypt");
        countryOptions.Add("El Salvador");
        countryOptions.Add("Equatorial Guinea");
        countryOptions.Add("Eritrea");
        countryOptions.Add("Estonia");
        countryOptions.Add("Ethiopia");
        countryOptions.Add("Falkland Islands");
        countryOptions.Add("Faroe Islands");
        countryOptions.Add("Fiji");
        countryOptions.Add("Finland");
        countryOptions.Add("France");
        countryOptions.Add("French Guiana");
        countryOptions.Add("French Polynesia");
        countryOptions.Add("French Southern Territories");
        countryOptions.Add("Gabon");
        countryOptions.Add("Gambia");
        countryOptions.Add("Georgia");
        countryOptions.Add("Germany");
        countryOptions.Add("Ghana");
        countryOptions.Add("Gibraltar");
        countryOptions.Add("Greece");
        countryOptions.Add("Greenland");
        countryOptions.Add("Grenada");
        countryOptions.Add("Guadeloupe");
        countryOptions.Add("Guam");
        countryOptions.Add("Guatemala");
        countryOptions.Add("Guernsey");
        countryOptions.Add("Guinea");
        countryOptions.Add("Guinea-Bissau");
        countryOptions.Add("Guyana");
        countryOptions.Add("Haiti");
        countryOptions.Add("Honduras");
        countryOptions.Add("Hong Kong S.A.R., China");
        countryOptions.Add("Hungary");
        countryOptions.Add("Iceland");
        countryOptions.Add("India");
        countryOptions.Add("Indonesia");
        countryOptions.Add("Iran");
        countryOptions.Add("Iraq");
        countryOptions.Add("Ireland");
        countryOptions.Add("Isle of Man");
        countryOptions.Add("Israel");
        countryOptions.Add("Italy");
        countryOptions.Add("Jamaica");
        countryOptions.Add("Japan");
        countryOptions.Add("Jersey");
        countryOptions.Add("Jordan");
        countryOptions.Add("Kazakhstan");
        countryOptions.Add("Kenya");
        countryOptions.Add("Kiribati");
        countryOptions.Add("Kuwait");
        countryOptions.Add("Kyrgystan");
        countryOptions.Add("Laos");
        countryOptions.Add("Latvia");
        countryOptions.Add("Lebanon");
        countryOptions.Add("Lesotho");
        countryOptions.Add("Liberia");
        countryOptions.Add("Libya");
        countryOptions.Add("Liechtenstein");
        countryOptions.Add("Lithuania");
        countryOptions.Add("Luxembourg");
        countryOptions.Add("Macao S.A.R., China");
        countryOptions.Add("Macedonia");
        countryOptions.Add("Madagascar");
        countryOptions.Add("Malawi");
        countryOptions.Add("Malaysia");
        countryOptions.Add("Maldives");
        countryOptions.Add("Mali");
        countryOptions.Add("Malta");
        countryOptions.Add("Marshall Islands");
        countryOptions.Add("Martinique");
        countryOptions.Add("Mauritania");
        countryOptions.Add("Mauritius");
        countryOptions.Add("Mayotte");
        countryOptions.Add("Mexico");
        countryOptions.Add("Micronesia");
        countryOptions.Add("Moldova");
        countryOptions.Add("Monaco");
        countryOptions.Add("Mongolia");
        countryOptions.Add("Montenegro");
        countryOptions.Add("Montserrat");
        countryOptions.Add("Morocco");
        countryOptions.Add("Mozambique");
        countryOptions.Add("Myanmar");
        countryOptions.Add("Namibia");
        countryOptions.Add("Nauru");
        countryOptions.Add("Nepal");
        countryOptions.Add("Netherlands");
        countryOptions.Add("New Caledonia");
        countryOptions.Add("New Zealand");
        countryOptions.Add("Nicaragua");
        countryOptions.Add("Niger");
        countryOptions.Add("Nigeria");
        countryOptions.Add("Niue");
        countryOptions.Add("Norfolk Island");
        countryOptions.Add("North Korea");
        countryOptions.Add("Northern Mariana Islands");
        countryOptions.Add("Norway");
        countryOptions.Add("Oman");
        countryOptions.Add("Pakistan");
        countryOptions.Add("Palau");
        countryOptions.Add("Palestinian Territory");
        countryOptions.Add("Panama");
        countryOptions.Add("Papua New Guinea");
        countryOptions.Add("Paraguay");
        countryOptions.Add("Peru");
        countryOptions.Add("Philippines");
        countryOptions.Add("Pitcairn");
        countryOptions.Add("Poland");
        countryOptions.Add("Portugal");
        countryOptions.Add("Puerto Rico");
        countryOptions.Add("Qatar");
        countryOptions.Add("Romania");
        countryOptions.Add("Russia");
        countryOptions.Add("Rwanda");
        countryOptions.Add("Reunion");
        countryOptions.Add("Saint Barthelemy");
        countryOptions.Add("Saint Helena");
        countryOptions.Add("Saint Kitts and Nevis");
        countryOptions.Add("Saint Lucia");
        countryOptions.Add("Saint Pierre and Miquelon");
        countryOptions.Add("Saint Vincent and the Grenadines");
        countryOptions.Add("Samoa");
        countryOptions.Add("San Marino");
        countryOptions.Add("Sao Tome and Principe");
        countryOptions.Add("Saudi Arabia");
        countryOptions.Add("Senegal");
        countryOptions.Add("Serbia");
        countryOptions.Add("Seychelles");
        countryOptions.Add("Sierra Leone");
        countryOptions.Add("Singapore");
        countryOptions.Add("Slovakia");
        countryOptions.Add("Slovenia");
        countryOptions.Add("Solomon Islands");
        countryOptions.Add("Somalia");
        countryOptions.Add("South Africa");
        countryOptions.Add("South Korea");
        countryOptions.Add("South Sudan");
        countryOptions.Add("Spain");
        countryOptions.Add("Sri Lanka");
        countryOptions.Add("Sudan");
        countryOptions.Add("Suriname");
        countryOptions.Add("Svalbard and Jan Mayen");
        countryOptions.Add("Swaziland");
        countryOptions.Add("Sweden");
        countryOptions.Add("Switzerland");
        countryOptions.Add("Syria");
        countryOptions.Add("Taiwan");
        countryOptions.Add("Tajikistan");
        countryOptions.Add("Tanzania");
        countryOptions.Add("Thailand");
        countryOptions.Add("Timor-Leste");
        countryOptions.Add("Togo");
        countryOptions.Add("Tokelau");
        countryOptions.Add("Tonga");
        countryOptions.Add("Trinidad and Tobago");
        countryOptions.Add("Tunisia");
        countryOptions.Add("Turkey");
        countryOptions.Add("Turkmenistan");
        countryOptions.Add("Turks and Caicos Islands");
        countryOptions.Add("Tuvalu");
        countryOptions.Add("U.S. Virgin Islands");
        countryOptions.Add("Uganda");
        countryOptions.Add("Ukraine");
        countryOptions.Add("United Arab Emirates");
        countryOptions.Add("United Kingdom");
        countryOptions.Add("United States Minor Outlying Islands");
        countryOptions.Add("Uruguay");
        countryOptions.Add("Uzbekistan");
        countryOptions.Add("Vanuatu");
        countryOptions.Add("Vatican");
        countryOptions.Add("Venezuela");
        countryOptions.Add("Viet Nam");
        countryOptions.Add("Wallis and Futuna");
        countryOptions.Add("Western Sahara");
        countryOptions.Add("Yemen");
        countryOptions.Add("Zambia");
        countryOptions.Add("Zimbabwe");
        EmailFormCountryDropdown.AddOptions(countryOptions);

        EmailFormIndustryDropdown.ClearOptions();
        List<string> industryOptions = new List<string>();
        industryOptions.Add("");
        industryOptions.Add("Aerospace & Defense");
        industryOptions.Add("Agriculture");
        industryOptions.Add("Automotive & Vehicle");
        industryOptions.Add("Construction");
        industryOptions.Add("Consumables");
        industryOptions.Add("Consumer Goods");
        industryOptions.Add("Contract Manufacturer");
        industryOptions.Add("Education");
        industryOptions.Add("Electronics");
        industryOptions.Add("Energy & Infrastructure");
        industryOptions.Add("Government");
        industryOptions.Add("Mining");
        industryOptions.Add("Other");
        industryOptions.Add("Other - Manufacturing");
        industryOptions.Add("Professional Services");
        industryOptions.Add("Research");
        industryOptions.Add("Software & Technology");
        industryOptions.Add("Transportation");
        industryOptions.Add("Warehousing & Distribution");
        EmailFormIndustryDropdown.AddOptions(industryOptions);
    }

    void Update()
    {
        //Need to update these positions here in case the screen resizes
        hiddenResetAxesPanelPos = ResetAxesPanelMask.transform.position + new Vector3(ResetAxesPanelMask.GetComponent<RectTransform>().rect.width, 0, 0);
        shownResetAxesPanelPos = ResetAxesPanelMask.transform.position;
        hiddenCustomCubeoidPanelPos = CustomCubeoidPanelMask.transform.position - new Vector3(CustomCubeoidPanelMask.GetComponent<RectTransform>().rect.width, 0, 0);
        shownCustomCubeoidPanelPos = CustomCubeoidPanelMask.transform.position;

        if (resetAxesPanelShown)
            ResetAxesPanel.transform.position = Vector3.MoveTowards(ResetAxesPanel.transform.position, shownResetAxesPanelPos, PanelSpeed * Time.deltaTime);
        else
            ResetAxesPanel.transform.position = Vector3.MoveTowards(ResetAxesPanel.transform.position, hiddenResetAxesPanelPos, PanelSpeed * Time.deltaTime);

        if (customCubeoidPanelShown)
            CustomCubeoidPanel.transform.position = Vector3.MoveTowards(CustomCubeoidPanel.transform.position, shownCustomCubeoidPanelPos, PanelSpeed * Time.deltaTime);
        else
            CustomCubeoidPanel.transform.position = Vector3.MoveTowards(CustomCubeoidPanel.transform.position, hiddenCustomCubeoidPanelPos, PanelSpeed * Time.deltaTime);

        DeleteSelectedObjectButton.interactable = editorManager.GetSelectedObject() != null && editorManager.GetSelectedObject() != editorManager.GetRobotBaseObject();
    }

    public void AddSensor(string text, string prefab, float scale = 1)
    {
        GameObject sensorText = Instantiate(TextPrefab);
        sensorText.name = text;
        sensorText.GetComponentInChildren<Text>().text = text;
        sensorText.transform.SetParent(SensorsContent.transform);
        sensorText.GetComponent<PartText>().SetPrefabName(prefab);
        sensorText.GetComponent<PartText>().SetScale(scale);
        sensorText.GetComponent<PartText>().SetState(PartText.State.AddNewSensor);
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
        part.GetComponent<UIClearpathButton>().normalTextColor = ClearpathBlack;
        part.GetComponent<UIClearpathButton>().OnPointerExit(null);     //Trigger the colour change
        ColorBlock c2 = selectedPart.colors;
        c2.normalColor = ClearpathYellow;
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
            selectedPart.GetComponent<UIClearpathButton>().normalTextColor = ClearpathWhite;
            selectedPart.GetComponent<UIClearpathButton>().OnPointerExit(null);     //Trigger the colour change
            ColorBlock c = selectedPart.colors;
            c.normalColor = ClearpathDarkGrey;
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

    public void SetLanguage(LanguageButton langButton)
    {
        SmartLocalization.LanguageManager.Instance.ChangeLanguage(langButton.culture);
        UpdateColorDropdownOptions();

        foreach (Transform t in LanguageButtonsContent)
        {
            t.GetComponent<UIClearpathButton>().normalTextColor = ClearpathGrey;
            t.GetComponent<UIClearpathButton>().OnPointerExit(null);    //Trigger the color change
        }
        langButton.GetComponent<UIClearpathButton>().normalTextColor = ClearpathYellow;
        langButton.GetComponent<UIClearpathButton>().OnPointerExit(null);    //Trigger the color change
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

    public void ShowEmailForm()
    {
        SendEmailPanel.SetActive(true);
    }

    public void SendEmail()
    {
        if (VerifyEmailForm())
        {
            HideEmailForm();
            editorManager.SetSelectedObject(null);
            foreach (Camera cam in ScreenshotCameras)
            {
                cam.gameObject.SetActive(true);
            }
            StartCoroutine(emailer.SendEmail(EmailFormEmailText.text));
        }
    }

    public void HideEmailForm()
    {
        SendEmailPanel.SetActive(false);
    }

    private bool VerifyEmailForm()
    {
        bool ret = true;
        if (EmailFormFirstNameText.text.Equals("")) { ret = false; EmailFormFirstNameText.colors = invalidColour; } else { EmailFormFirstNameText.colors = validColour; }
        if (EmailFormLastNameText.text.Equals("")) { ret = false; EmailFormLastNameText.colors = invalidColour; } else { EmailFormLastNameText.colors = validColour; }
        if (EmailFormEmailText.text.Equals("")) { ret = false; EmailFormEmailText.colors = invalidColour; } else { EmailFormEmailText.colors = validColour; }
        if (EmailFormOrganizationNameText.text.Equals("")) { ret = false; EmailFormOrganizationNameText.colors = invalidColour; } else { EmailFormOrganizationNameText.colors = validColour; }
        if (EmailFormStateDropdown.captionText.text.Equals("")) { ret = false; EmailFormStateDropdown.colors = invalidColour; } else { EmailFormStateDropdown.colors = validColour; }
        if (EmailFormCountryDropdown.captionText.text.Equals("")) { ret = false; EmailFormCountryDropdown.colors = invalidColour; } else { EmailFormCountryDropdown.colors = validColour; }
        if (EmailFormIndustryDropdown.captionText.text.Equals("")) { ret = false; EmailFormIndustryDropdown.colors = invalidColour; } else { EmailFormIndustryDropdown.colors = validColour; }
        return ret;
    }

    public void ShowHelpPanel()
    {
        HelpPanel.SetActive(true);
    }

    public void HideHelpPanel()
    {
        HelpPanel.SetActive(false);
    }

    public void ShowAboutPanel()
    {
        AboutPanel.SetActive(true);
    }

    public void HideAboutPanel()
    {
        AboutPanel.SetActive(false);
    }

}
