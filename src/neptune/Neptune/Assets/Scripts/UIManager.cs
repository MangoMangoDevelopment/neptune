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
    public Text ModeText;

    //TODO: Remove this. This should be superseeded by a combination of Brian and Amber's work. See DBManager.GetSensorList() for more info.
    public GameObject TestGO;

    //Private Variables
    private DBManager dbManager;
    private EditorManager editorManager;

    void Start()
    {
        dbManager = new DBManager();
        dbManager.GetSensorList(this, TestGO);
        editorManager = GameObject.FindGameObjectWithTag(EditorManager.TAG).GetComponent<EditorManager>();
    }

    void Update()
    {
        ModeText.text = "Mode: " + editorManager.GetMode().ToString();
    }

    public void AddSensor(string text, GameObject go)
    {
        GameObject sensorText = Instantiate<GameObject>(TextPrefab);
        sensorText.name = text;
        sensorText.GetComponentInChildren<Text>().text = text;
        sensorText.transform.SetParent(SensorsContent.transform);
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
}
