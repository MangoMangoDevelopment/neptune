using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIClearpathButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text text;
    public Color normalTextColor;
    public Color highlightTextColor;

    void Start()
    {
        text.color = normalTextColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = highlightTextColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = normalTextColor;
    }
}