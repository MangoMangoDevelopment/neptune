using UnityEngine;
using UnityEngine.UI;

public class headingController : MonoBehaviour {
    private int sensorCount = 0;
    private int runningSensorCount = 0;

    private Text headingText;

    void Awake()
    {
        headingText = gameObject.GetComponentInChildren<Text>();
    }

    public void SetHeadingName(string name)
    {
        transform.parent.name = name;
        headingText.text = string.Format("{0} ({1})", name, sensorCount);
    }

    public void AddSensor()
    {
        runningSensorCount++;
    }

    public void RemoveSensor()
    {
        runningSensorCount--;
    }

    public bool hasSensors()
    {
        return sensorCount > 0;
    }

    public void UpdateSensorCount()
    {
        UpdateHeadingText(sensorCount, runningSensorCount);
        sensorCount = runningSensorCount;
    }

    private void UpdateHeadingText(int oldCount, int newCount)
    {
        string oldValue = string.Format("({0})", oldCount);
        string newValue = string.Format("({0})", newCount);
        headingText.text = headingText.text.Replace(oldValue, newValue);
    }
}
