using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public delegate void CallbackFunction();

public class DialogManager : MonoBehaviour {

    public enum ButtonType
    {
        Okay,
        YesNo,
        Cancel
    }

    public static DialogManager instance;
    public GameObject DialogBox;
    public Text MessageText;
    public Text TitleText;
    public Button Button1;
    public Button Button2;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void ShowDialog(string msg, string title, ButtonType buttonType, CallbackFunction callback1 = null, CallbackFunction callback2 = null)
    {
        MessageText.text = msg;
        TitleText.text = title;
        
        switch (buttonType)
        {
            case ButtonType.Okay:
                Button1.GetComponentInChildren<Text>().text = SmartLocalization.LanguageManager.Instance.GetTextValue("Okay");
                Button1.onClick.AddListener(delegate { ExecuteDelegate(callback1); });
                Button2.gameObject.SetActive(false);
                break;
            case ButtonType.YesNo:
                Button1.GetComponentInChildren<Text>().text = SmartLocalization.LanguageManager.Instance.GetTextValue("Yes");
                Button1.onClick.AddListener(delegate { ExecuteDelegate(callback1); });
                Button2.GetComponentInChildren<Text>().text = SmartLocalization.LanguageManager.Instance.GetTextValue("No");
                Button2.onClick.AddListener(delegate { ExecuteDelegate(callback2); });
                break;
            case ButtonType.Cancel:
                Button1.GetComponentInChildren<Text>().text = SmartLocalization.LanguageManager.Instance.GetTextValue("Cancel");
                Button1.onClick.AddListener(delegate { ExecuteDelegate(callback1); });
                Button2.gameObject.SetActive(false);
                break;
        }
        DialogBox.SetActive(true);
    }

    public void HideDialog()
    {
        DialogBox.SetActive(false);
    }

    public void ExecuteDelegate(CallbackFunction function)
    {
        if (function != null)
            function();
        HideDialog();
        Button1.onClick.RemoveAllListeners();
        Button2.onClick.RemoveAllListeners();
    }
}
