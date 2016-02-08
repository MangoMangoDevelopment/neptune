using UnityEngine;
using System.Collections;

public class EditorManager : MonoBehaviour {

    public GameObject parts;
    
    public enum Mode
    {
        Translate,
        Rotate,
        Select
    }

    public Mode mode = Mode.Translate;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.T))
        {
            mode = Mode.Translate;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            mode = Mode.Rotate;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.S))
        {
            mode = Mode.Select;
        }
	}
    public Mode GetMode()
    {
        return mode;
    }
}
