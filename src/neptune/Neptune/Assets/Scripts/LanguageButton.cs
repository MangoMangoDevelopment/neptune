using UnityEngine;
using System.Collections;

public class LanguageButton : MonoBehaviour {
    public SmartLocalization.SmartCultureInfo culture;
    public string GetString()
    {
        return culture.nativeName;
    }
}
