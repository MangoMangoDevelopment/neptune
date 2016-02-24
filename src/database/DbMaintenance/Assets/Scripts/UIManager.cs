/*

Reference:
    http://forum.unity3d.com/threads/accordion-type-layout.271818/
*/
using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class UIState
{
    static int position = 0;
    GameObject panel;
}

/// <summary>
/// 
/// </summary>
public class UIManager : MonoBehaviour {
    private GameObject currState;
    public GameObject initialState;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start () {
        currState = initialState;
	}

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update () {
	
	}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nextPanel"></param>
    public void SwapPanel(GameObject nextPanel)
    {
        currState.SetActive(false);
        currState = nextPanel;
        nextPanel.SetActive(true);
    }

    /// <summary>
    /// Creating a toggling effect on a UI element
    /// </summary>
    /// <param name="panel"></param>
    public void TogglePanel(GameObject panel)
    {
        if(panel.activeInHierarchy)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
        }
    }

    /// <summary>
    /// Adding a new sensor process
    /// </summary>
    public void AddSensor_click()
    {

    }
}
