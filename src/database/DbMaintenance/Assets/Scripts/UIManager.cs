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

    public GameObject sensorViewPort;      // This is the game object that contains the list of sensor
    public GameObject form;             // This is the game object that holds the form elements (input fields)
    public GameObject deleteBtn;
    public GameObject categoryContainerPrefab;
    public GameObject sensorBtnPrefab;

    private UrdfModel urdf;
    private InputField[] inputs;
    private Text[] sensors;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start ()
    {
        string connString = "URI=file:" + Application.dataPath + "/db/neptune.db";
        urdf = new UrdfModel(connString);
        currState = UIState.Create; // assume that the default is to add a sensor right away

        List<UrdfItem> sensorList = urdf.GetSensors();
        GameObject sensorBtn;
        GameObject categoryHolder = GameObject.Instantiate(categoryContainerPrefab);

        foreach (UrdfItem item in sensorList)
        {
            sensorBtn = GameObject.Instantiate(sensorBtnPrefab);
            Text value = sensorBtn.GetComponentInChildren<Text>();
            value.text = item.name;
            sensorBtn.transform.SetParent(categoryHolder.transform, false);
        }

        categoryHolder.transform.SetParent(sensorViewPort.transform, false);

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
        if (this.inputs == null)
        {
            this.inputs = form.GetComponentsInChildren<InputField>();
        }
        foreach (InputField input in this.inputs)
        {
            Debug.Log(input.text);
            input.text = "";
        }
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
        if (this.inputs == null)
        {
            this.inputs = form.GetComponentsInChildren<InputField>();
        }

        UrdfItem item = new UrdfItem();

        foreach(InputField input in inputs)
        {
            if(input.name == "txtName")
            {
                item.name = input.text;
            }
            else if (input.name == "txtModel")
            {
                item.modelNumber = input.text; 
            }
            else if (input.name == "txtInternalCost")
            {
                if(!float.TryParse(input.text, out item.internalCost))
                {
                    Debug.Log("[internalCost] Invalid float detected. Did you change the context type?");
                }
            }
            else if (input.name == "txtExternalCost")
            {
                if (!float.TryParse(input.text, out item.externalCost))
                {
                    Debug.Log("[externalCost] Invalid float detected. Did you change the context type?");
                }
            }
            else if (input.name == "txtPower")
            {
                if (!float.TryParse(input.text, out item.powerUsage))
                {
                    Debug.Log("[power] Invalid float detected. Did you change the context type?");
                }
            }
            else if (input.name == "txtWeight")
            {
                if (!float.TryParse(input.text, out item.weight))
                {
                    Debug.Log("[weight] Invalid float detected. Did you change the context type?");
                }
            }
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
        if (sensors == null)
        {
            this.sensors = sensorViewPort.GetComponentsInChildren<Text>();
        }
        foreach (Text name in this.sensors)
        {
            if (name.text != "Heading")
                name.transform.parent.gameObject.SetActive(name.text.ToLower().Contains(value.ToLower()));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    private void changeState(UIState state)
    {
        currState = state;
        switch(state)
        {

        }
    }

    /*
     Reference: http://docs.unity3d.com/ScriptReference/UI.InputField.html for input field functions > include validating characters
    */
}
