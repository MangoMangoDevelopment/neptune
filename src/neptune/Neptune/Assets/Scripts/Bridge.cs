using UnityEngine;
using System.Collections.Generic;

public class Bridge : MonoBehaviour {

    //Public variables
    public static string TAG = "Bridge";

    public GameObject LeftPillar;
    public GameObject RightPillar;
    public GameObject Crossbar;

    //Private variables
    private GameObject objectGO;
    private List<Collider> colliders;

    void Start()
    {
        colliders = new List<Collider>();
    }

    void Update()
    {
        foreach (Collider col in colliders)
        {
            if (col == null)
            {
                colliders.Remove(col);
                ShowBridge();
            }
        }
    }

    public void SetObjectGO(GameObject go)
    {
        objectGO = go;
    }

    public void SetDimensions(float gap, float height)
    {
        Vector3 leftPos = LeftPillar.transform.position;
        Vector3 rightPos = RightPillar.transform.position;
        Vector3 crossbarPos = Crossbar.transform.position;
        Vector3 leftScale = LeftPillar.transform.localScale;
        Vector3 rightScale = RightPillar.transform.localScale;
        Vector3 crossbarScale = Crossbar.transform.localScale;

        leftPos.x = -gap / 2 + (leftScale.x / 2);
        rightPos.x = gap / 2 - (rightScale.x / 2);
        leftPos.y = height / 2;
        rightPos.y = height / 2;

        crossbarPos.x = 0;
        crossbarPos.y = height - (crossbarScale.y / 2);
        
        leftScale.y = height;
        rightScale.y = height;
        crossbarScale.x = gap - leftScale.x;

        LeftPillar.transform.position = leftPos;
        RightPillar.transform.position = rightPos;
        Crossbar.transform.position = crossbarPos;
        LeftPillar.transform.localScale = leftScale;
        RightPillar.transform.localScale = rightScale;
        Crossbar.transform.localScale = crossbarScale;

        BoxCollider col = GetComponent<BoxCollider>();
        Vector3 colSize = col.size;
        Vector3 colCenter = col.center;
        colSize.y = height + 1;
        colCenter.y = height / 2 + 1;
        col.size = colSize;
        col.center = colCenter;
    }

    void OnTriggerEnter(Collider col)
    {
        if (objectGO != null)
        {
            if (col.tag.Equals(TAG))
            {
                colliders.Add(col);
                Manipulatable manipulatable = objectGO.GetComponent<Manipulatable>();
                if (manipulatable.isSelected)
                {
                    manipulatable.HideBridge();
                }
            }
        }
        else
        {
            Debug.Log("Trigger entered on bridge without it's ObjectGO set");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (objectGO != null)
        {
            colliders.Remove(col);
            ShowBridge();
        }
        else
        {
            Debug.Log("Trigger exited on bridge without it's ObjectGO set");
        }
    }

    private void ShowBridge()
    {
        if (colliders.Count == 0)
        {
            Manipulatable manipulatable = objectGO.GetComponent<Manipulatable>();
            manipulatable.ShowBridge();
        }
    }
}
