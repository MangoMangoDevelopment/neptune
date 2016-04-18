using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

/// <summary>
/// This class is the controller for the UI management
/// </summary>
public class UIManager : MonoBehaviour {
    private enum UIState
    {
        Create,
        Update,
        Read,
        Delete,
    }
    UIState currState;

    public GameObject sensorViewPort;               // This is the game object that contains the list of sensor
    public GameObject previewPort;                  // Canvas intended for showing the preview of sensor and preview of urdf
    public GameObject form;                         // This is the game object that holds the form elements (input fields)
    public GameObject deleteBtn;                    // Reference to the delete button on the UI
    public GameObject categoryContainerPrefab;      // Reference for the category container to hold sensor buttons
    public GameObject sensorBtnPrefab;              // Reference to the prefab for a sensor button
    public GameObject unkownSensorPrefab;           // Reference to the prefab of an empty/unknown sensor
    public GameObject invisibleText;                // Reference to the prefab containing 3D text of inivisble
    public Text title;

    private GameObject currSelectedSensor;              // Reference to the selected sensor button game object
    private GameObject currSelectedSensorGoModel;       // Reference to the selected sensor model game object
    private UrdfDb urdf;
    private Dictionary<string, InputField> inputs;
    private Dictionary<string, Dropdown> dropdowns;
    private Dictionary<string, Toggle> toggles;
    private Dictionary<int, GameObject> categoryHolderList;
    private List<GameObject> sensors;

    /// <summary>
    /// Overloading Unity function for initiatization for this script
    /// </summary>
    void Start()
    {
        this.urdf = new UrdfDb();
        this.currState = UIState.Create; // assume that the default is to add a sensor right away
        this.sensors = new List<GameObject>();
        categoryHolderList = new Dictionary<int, GameObject>();
        setupForm();
        SensorPanelSetup();
    }

    /// <summary>
    /// Grabs all available inputs fields and dropdowns on the form.
    /// </summary>
    void setupForm()
    {
        this.inputs = new Dictionary<string, InputField>();
        InputField[] inputsList = form.GetComponentsInChildren<InputField>();
        foreach (InputField input in inputsList)
        {
            this.inputs.Add(input.name, input);
        }

        this.dropdowns = new Dictionary<string, Dropdown>();
        Dropdown[] dropdownList = form.GetComponentsInChildren<Dropdown>();
        foreach (Dropdown dropdown in dropdownList)
        {
            this.dropdowns.Add(dropdown.name, dropdown);
        }

        this.toggles = new Dictionary<string, Toggle>();
        Toggle[] toggleList = form.GetComponentsInChildren<Toggle>();
        foreach (Toggle toggle in toggleList)
        {
            this.toggles.Add(toggle.name, toggle);
        }
    }


    /// <summary>
    /// Sets up the sensor panel by placing sensors in the appropriate category using the
    /// available prefabs. 
    /// </summary>
    void SensorPanelSetup()
    {
        List<SensorCategoriesModel> categories = urdf.GetSensorCategories();
        List<UrdfTypeModel> types = urdf.GetUrdfTypes();
        List<UrdfItemModel> sensorList = urdf.GetUrdfs();

        this.dropdowns["ddCategory"].options.Add(new Dropdown.OptionData("Unknown"));
        foreach (SensorCategoriesModel category in categories)
        {
            this.dropdowns["ddCategory"].options.Add(new Dropdown.OptionData(category.name));
        }

        this.dropdowns["ddTypes"].options.Add(new Dropdown.OptionData("Unknown"));
        foreach (UrdfTypeModel type in types)
        {
            this.dropdowns["ddTypes"].options.Add(new Dropdown.OptionData(type.name));
        }

        List<headingController> headingControllers = new List<headingController>();
        GameObject unknownHolder = GameObject.Instantiate(categoryContainerPrefab);
        headingController unknownController = unknownHolder.GetComponentInChildren<headingController>();
        unknownController.SetHeadingName("Unknown");
        headingControllers.Add(unknownController);
        unknownHolder.transform.SetParent(sensorViewPort.transform, false);
        categoryHolderList.Add(0, unknownHolder);

        foreach (SensorCategoriesModel category in categories)
        {
            GameObject categoryHolder = GameObject.Instantiate(categoryContainerPrefab);
            headingController controller = categoryHolder.GetComponentInChildren<headingController>();
            controller.SetHeadingName(category.name);
            headingControllers.Add(controller);
            categoryHolder.GetComponent<CategoryHeading>().category = category;
            categoryHolder.transform.SetParent(sensorViewPort.transform, false);
            categoryHolderList.Add(category.uid, categoryHolder);
        }


        foreach (UrdfItemModel item in sensorList)
        {
            GameObject sensorBtn = GameObject.Instantiate(sensorBtnPrefab);
            Button btn = sensorBtn.GetComponent<Button>();
            Text value = sensorBtn.GetComponentInChildren<Text>();
            Sensor sensor = sensorBtn.GetComponent<Sensor>();
            sensor.item = item;
            sensor.category = categoryHolderList[item.fk_category_id].GetComponent<CategoryHeading>().category;
            value.text = item.name;
            sensorBtn.name = item.name.ToLower();
            headingControllers[item.fk_category_id].AddSensor();
            sensorBtn.transform.SetParent(categoryHolderList[item.fk_category_id].transform, false);
            btn.onClick.AddListener(() => SensorOnClick(sensorBtn));
            this.sensors.Add(sensorBtn);
        }

        foreach (headingController controller in headingControllers)
        {
            controller.UpdateSensorCount();
        }
    }


    /// <summary>
    /// Sets the UI to recongize that we're trying to add a new sensor and clears the
    /// form to allow new data to be entered.
    /// </summary>
    public void AddSensor_click()
    {
        SetUiState(UIState.Create);
        this.currSelectedSensor = null; // deselect the button, don't destory it
        GameObject.Destroy(this.currSelectedSensorGoModel);
        clearForm();
    }


    /// <summary>
    /// This will clear the form for entering new sensor data.
    /// </summary>
    private void clearForm()
    {
        foreach (KeyValuePair<string, InputField> input in this.inputs)
        {
            Debug.Log(input.Value.text);
            input.Value.text = "";
        }

        foreach (KeyValuePair<string, Dropdown> dropdown in this.dropdowns)
        {
            dropdown.Value.value = 0;
        }

        foreach (KeyValuePair<string, Toggle> toggle in this.toggles)
        {
            toggle.Value.isOn = true;
        }
    }

    /// <summary>
    /// Given a model of the item, it will populate the form with the appropriate values.
    /// </summary>
    /// <param name="item">The model of the sensor it populate in the form</param>
    private void setForm(UrdfItemModel item)
    {
        this.inputs["txtName"].text = item.name;
        this.inputs["txtModel"].text = item.modelNumber;
        this.inputs["txtInternalCost"].text = item.internalCost.ToString();
        this.inputs["txtExternalCost"].text = item.externalCost.ToString();
        this.inputs["txtNotes"].text = item.notes;
        this.inputs["txtPower"].text = item.powerUsage.ToString();
        this.inputs["txtWeight"].text = item.weight.ToString();
        this.inputs["txtTime"].text = item.time.ToString();
        this.inputs["txtPrefabPath"].text = item.prefabFilename;

        this.dropdowns["ddCategory"].value = item.fk_category_id;
        this.dropdowns["ddTypes"].value = item.fk_type_id;

        this.toggles["toggleVisibility"].isOn = (item.visibility == 0 ? false : true);
    }

    /// <summary>
    /// This function will set the appropriate state before calling the 
    /// saveform which will trigger the delete function
    /// </summary>
    public void DeleteForm_click()
    {
        SetUiState(UIState.Delete);
        SaveForm_click();
    }

    /// <summary>
    /// This save function, grabs all the inputs and preforms the appropriate
    /// action according to the state it ccurrently is in.
    /// </summary>
    public void SaveForm_click()
    {
        UrdfItemModel item = null;
        Sensor sensor = null;
        if (this.currSelectedSensor != null)
        {
            sensor = this.currSelectedSensor.GetComponent<Sensor>();
        }

        if (sensor != null)
        {
            item = sensor.item;
        }
        else
        {
            item = new UrdfItemModel();
        }

        item.name = this.inputs["txtName"].text;
        item.modelNumber = this.inputs["txtModel"].text;
        item.notes = this.inputs["txtNotes"].text;
        item.prefabFilename = this.inputs["txtPrefabPath"].text;
        item.fk_category_id = this.dropdowns["ddCategory"].value;
        item.fk_type_id = this.dropdowns["ddTypes"].value;
        item.visibility = (this.toggles["toggleVisibility"].isOn ? 1 : 0);

        if (!float.TryParse(this.inputs["txtInternalCost"].text, out item.internalCost))
        {
            Debug.Log("[internalCost] Invalid float detected. Did you change the context type?");
        }
        if (!float.TryParse(this.inputs["txtExternalCost"].text, out item.externalCost))
        {
            Debug.Log("[externalCost] Invalid float detected. Did you change the context type?");
        }
        if (!float.TryParse(this.inputs["txtPower"].text, out item.powerUsage))
        {
            Debug.Log("[power] Invalid float detected. Did you change the context type?");
        }
        if (!float.TryParse(this.inputs["txtWeight"].text, out item.weight))
        {
            Debug.Log("[weight] Invalid float detected. Did you change the context type?");
        }
        if (!float.TryParse(this.inputs["txtTime"].text, out item.time))
        {
            Debug.Log("[time] Invalid float detected. Did you change the context type?");
        }


        switch (currState)
        {
            case UIState.Create:
                urdf.AddSensor(item);
                break;
            case UIState.Delete:
                urdf.DeleteSensor(item);
                GameObject.Destroy(this.currSelectedSensor);
                GameObject.Destroy(this.currSelectedSensorGoModel);
                SetUiState(UIState.Create);
                clearForm();
                break;
            case UIState.Update:
                urdf.UpdateSensor(item);
                this.currSelectedSensor.GetComponent<Sensor>().item = item;
                UpdateVisuals(item);
                break;
        }
    }


    /// <summary>
    /// This handles UI rules when switching states. Currently, handles heading
    /// title and the display of the delete button.
    /// </summary>
    /// <param name="state"></param>
    private void SetUiState(UIState state)
    {
        this.currState = state;
        switch (state)
        {
            case UIState.Create:
                deleteBtn.SetActive(false);
                title.text = "Add New Part";
                break;
            case UIState.Delete:
                deleteBtn.SetActive(false);
                break;
            case UIState.Update:
                deleteBtn.SetActive(true);
                title.text = "Updating Part - ";
                break;
        }
    }

    /// <summary>
    /// This function goes through the list of compnonents and determines if the
    /// search text is within the sensor name. If it is than it will turn it on
    /// else it will set it to not show.
    /// </summary>
    /// <param name="value"></param>
    public void SearchFunction (string value)
    {
        bool inName = false;
        foreach (GameObject item in this.sensors)
        {
            inName = item.name.Contains(value.ToLower());
            headingController controller = item.transform.parent.GetComponentInChildren<headingController>();
            // advoid double count sensors
            if (inName && !item.transform.gameObject.activeInHierarchy)
            {
                controller.AddSensor();
            }
            else if (!inName && item.transform.gameObject.activeInHierarchy)
            {
                controller.RemoveSensor();
            }
            item.transform.gameObject.SetActive(inName);
        }
        foreach(KeyValuePair<int, GameObject> category in categoryHolderList)
        {
            headingController controller = category.Value.GetComponentInChildren<headingController>();
            controller.UpdateSensorCount();
            category.Value.SetActive(controller.hasSensors());
        }
    }

    /// <summary>
    /// Handles the click event of a sensor button. Creates a prefab of the sensor in the preview
    /// window. It will destory the preview sensor object if there is one to destory.
    /// </summary>
    /// <param name="sensor">A reference to the sensor button being clicked.</param>
    void SensorOnClick(GameObject sensor)
    {
        this.currSelectedSensor = sensor;
        UrdfItemModel item = sensor.GetComponent<Sensor>().item;
        setForm(item);
        SetUiState(UIState.Update);

        title.text += item.name;

        UpdateVisuals(item);
    }

    private void UpdateVisuals(UrdfItemModel item)
    {
        if (this.currSelectedSensorGoModel != null)
        {
            Destroy(this.currSelectedSensorGoModel);
        }

        if (item.visibility == 0)
        {
            this.currSelectedSensorGoModel = Instantiate(invisibleText);
        }
        else
        {
            try
            {
                this.currSelectedSensorGoModel = Instantiate(Resources.Load("Prefabs/" + item.prefabFilename, typeof(GameObject))) as GameObject;
            }
            catch (Exception)
            {
                this.currSelectedSensorGoModel = Instantiate(unkownSensorPrefab);
            }
            finally
            {
                this.currSelectedSensorGoModel.AddComponent<modelPreview>();
            }
        }
    }

    /// <summary>
    /// Handles the preview button click event. This will toggle the preview view port.
    /// </summary>
    public void PreviewButton_click()
    {
        previewPort.SetActive(!previewPort.activeInHierarchy);
    }

}
