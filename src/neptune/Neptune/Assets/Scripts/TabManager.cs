using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabManager : MonoBehaviour {

    public List<Tabber> Selectables;

	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            foreach (Tabber field in Selectables)
            {
                if (field.gameObject == EventSystem.current.currentSelectedGameObject)
                {
                    if (Input.GetKey(KeyCode.LeftShift) || (Input.GetKey(KeyCode.RightShift)))
                    {
                        if (field.PrevSelectable != null)
                        {
                            EventSystem.current.SetSelectedGameObject(field.PrevSelectable.gameObject, null);
                            field.PrevSelectable.OnPointerDown(new PointerEventData(EventSystem.current));
                            field.PrevSelectable.OnPointerUp(new PointerEventData(EventSystem.current));
                        }
                    }
                    else
                    {
                        if (field.NextSelectable != null)
                        {
                            EventSystem.current.SetSelectedGameObject(field.NextSelectable.gameObject, null);
                            field.NextSelectable.OnPointerDown(new PointerEventData(EventSystem.current));
                            field.NextSelectable.OnPointerUp(new PointerEventData(EventSystem.current));
                        }
                    }
                    break;
                }
            }
        }
    }
}
