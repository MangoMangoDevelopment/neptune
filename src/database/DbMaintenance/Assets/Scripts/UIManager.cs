/*

Reference:
    http://forum.unity3d.com/threads/accordion-type-layout.271818/
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using System.Collections.Generic;

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

    public GameObject sensorViewPort;       // This is the game object that contains the list of sensor
    public GameObject previewPort;          // Canvas intended for showing the preview of sensor and preview of urdf
    public GameObject form;                 // This is the game object that holds the form elements (input fields)
    public GameObject deleteBtn;
    public GameObject categoryContainerPrefab;
    public GameObject sensorBtnPrefab;
    public GameObject emptyGameObject;

    private GameObject currentObject;

    private GameObject currentSelectedSensor;

    private UrdfModel urdf;
    private Dictionary<string, InputField> inputs;
    private List<GameObject> sensors;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        this.urdf = new UrdfModel();
        this.currState = UIState.Create; // assume that the default is to add a sensor right away
        this.sensors = new List<GameObject>();
        setupInputFieldList();
        SensorPanelSetup();

        /*
        GameObject emptyPrefabWithMeshRenderer = Instantiate(emptyGameObject);
        emptyPrefabWithMeshRenderer.name = "bumblebee";
        string meshPath = "../../../meshes/HDL32E_Outline_Model.obj";
        GameObject spawnedPrefab;
        ObjImporter importer = new ObjImporter();
        Mesh importedMesh = importer.ImportFile(meshPath);
        spawnedPrefab = Instantiate(emptyPrefabWithMeshRenderer, transform.position, transform.rotation) as GameObject;
        spawnedPrefab.GetComponent<MeshFilter>().mesh = importedMesh;
        spawnedPrefab.transform.SetParent(previewPort.transform);
        */

        //GameObject prefab = new GameObject("Milkbottle");
        //var meshFilter = prefab.AddComponent<MeshFilter>();
        //meshFilter.mesh = loadmesh();
        //prefab.AddComponent<MeshRenderer>();
        //prefab.AddComponent<BoxCollider>();
        //// etc
        //return prefab;

        // GameObject model = GameObject.Instantiate(Resources.Load("bumblebee2", typeof(GameObject))) as GameObject;
        // model.transform.SetParent(previewPort.transform);
    }

    void setupInputFieldList()
    {
        this.inputs = new Dictionary<string, InputField>();
        InputField[] inputsList = form.GetComponentsInChildren<InputField>();
        foreach (InputField input in inputsList)
        {
            this.inputs.Add(input.name, input);
        }
    }

    void SensorPanelSetup()
    {
        List<SensorCategoriesModel> categories = urdf.GetSensorCategories();
        List<UrdfItemModel> sensorList = urdf.GetSensors();
        Dictionary<int, GameObject> categoryHolderList = new Dictionary<int, GameObject>();

        foreach (SensorCategoriesModel category in categories)
        {
            GameObject categoryHolder = GameObject.Instantiate(categoryContainerPrefab);
            SensorCategoriesModel urdfCat = categoryHolder.GetComponent<SensorCategoriesModel>();
            Text value = categoryHolder.GetComponentInChildren<Text>();
            //categoryHolder. //on.AddListener(() => CategoryOnClick(categoryHolder));
            urdfCat.copy(category);
            value.text = category.name;
            categoryHolder.name = category.name;
            categoryHolder.transform.SetParent(sensorViewPort.transform, false);
            categoryHolderList.Add(category.uid, categoryHolder);
        }


        foreach (UrdfItemModel item in sensorList)
        {
            GameObject sensorBtn = GameObject.Instantiate(sensorBtnPrefab);
            Button btn = sensorBtn.GetComponent<Button>();
            Text value = sensorBtn.GetComponentInChildren<Text>();
            UrdfItemModel urdfItem = sensorBtn.GetComponent<UrdfItemModel>();
            urdfItem.copy(item);
            value.text = item.name;
            sensorBtn.name = item.name.ToLower();
            sensorBtn.transform.SetParent(categoryHolderList[item.fk_category_id].transform, false);
            btn.onClick.AddListener(() => SensorOnClick(sensorBtn));
            this.sensors.Add(sensorBtn);
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update () {
	
	}

    /// <summary>
    /// Sets the UI to recongize that we're trying to add a new sensor and clears the
    /// form to allow new data to be entered.
    /// </summary>
    public void AddSensor_click()
    {
        currState = UIState.Create;
        clearForm();
    }


    /// <summary>
    /// This will clear the form for entering sensor data.
    /// </summary>
    private void clearForm()
    {
        foreach (KeyValuePair<string, InputField> input in this.inputs)
        {
            Debug.Log(input.Value.text);
            input.Value.text = "";
        }
    }

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
    }

    /// <summary>
    /// This function will set the appropriate state before calling the 
    /// saveform which will trigger the delete function
    /// </summary>
    public void DeleteForm_click()
    {
        currState = UIState.Delete;
        SaveForm_click();
    }

    /// <summary>
    /// This save function, grabs all the inputs and preforms the appropriate
    /// action according to the state it ccurrently is in.
    /// </summary>
    public void SaveForm_click()
    {
        UrdfItemModel item = new UrdfItemModel();
        item.name = this.inputs["txtName"].text;
        item.modelNumber = this.inputs["txtModel"].text;

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
                deleteBtn.SetActive(true);
                break;
            case UIState.Delete:
                urdf.DeleteSensor(item);
                deleteBtn.SetActive(false);
                break;
            case UIState.Update:
                urdf.UpdateSensor(item);
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
        foreach (GameObject item in this.sensors)
        {
            item.transform.gameObject.SetActive(item.name.Contains(value.ToLower()));
        }
    }


    void SensorOnClick(GameObject sensor)
    {
        currentSelectedSensor = sensor;
        UrdfItemModel item = sensor.GetComponent<UrdfItemModel>();
        Debug.Log(sensor.name);
        setForm(item);
        if (currentObject != null)
        {
            Destroy(currentObject);
        }

        currentObject = Instantiate(Resources.Load(item.prefabFilename, typeof(GameObject))) as GameObject;
    }

    public void PreviewButton_click()
    {
        previewPort.SetActive(!previewPort.activeInHierarchy);
    }

    // look at UiCollapsible.cs
    void CategoryOnClick(GameObject heading)
    {
        Debug.Log(heading.name);
    }

    /*
     Reference: http://docs.unity3d.com/ScriptReference/UI.InputField.html for input field functions > include validating characters
    */
}
