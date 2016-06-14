using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PartText : MonoBehaviour {

    //Public Structures
    public enum State
    {
        AddNewSensor,
        SelectExistingSensor
    };

    //Private Variables
    private GameObject GO;
    private State state = State.AddNewSensor;
    private EditorManager editorManager;
    private UIManager uiManager;
    public float scale;

    void Start()
    {
        editorManager = GameObject.FindGameObjectWithTag(EditorManager.TAG).GetComponent<EditorManager>();
        uiManager = GameObject.FindGameObjectWithTag(UIManager.TAG).GetComponent<UIManager>();
        GetComponent<Button>().onClick.AddListener(delegate { OnClick(); });
    }

    public void SetGO(GameObject go)
    {
        GO = go;
    }

    public GameObject GetGO()
    {
        return GO;
    }

    public void SetScale(float scale)
    {
        this.scale = scale;
    }

    public void DestroyPart()
    {
        GO.GetComponent<Manipulatable>().Die();
        Destroy(gameObject);
    }

    public void SetState(State state)
    {
        this.state = state;
    }

    IEnumerator CallAddPart()
    {
        //We do this because Showing the loading panel takes 1 frame
        uiManager.ShowLoadingPanel();
        yield return 0;
        uiManager.AddPart(name, editorManager.AddPart(GO, name), scale);
        uiManager.HideLoadingPanel();
    }

    public void OnClick()
    {
        switch (state)
        {
            case State.AddNewSensor:
                {
                    //Get the instance of the prefab back from the Editor Manager so that we can reference it when selecting
                    StartCoroutine("CallAddPart");
                }
                break;
            case State.SelectExistingSensor:
                {
                    if (GO != null)
                    {
                        editorManager.SelectPart(GO);
                        uiManager.SelectPart(gameObject.GetComponent<Button>());
                    }
                }
                break;
        }
    }
}
