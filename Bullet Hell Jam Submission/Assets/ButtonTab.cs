using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTab : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] TabGroup TabGroup;
    public Image background;

    private void Start()
    {
        background = GetComponent<Image>();
        TabGroup.Subscribe(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TabGroup.OnTabExit(this);
    }

}
