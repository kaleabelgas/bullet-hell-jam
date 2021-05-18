using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    [SerializeField] private List<ButtonTab> buttonTabs;
    [SerializeField] private Sprite tabidle;
    [SerializeField] private Sprite tabHover;
    [SerializeField] private Sprite tabActive;
    [SerializeField] private ButtonTab selectedTab;
    [SerializeField] private ButtonTab defaultTab;

    [SerializeField] private List<GameObject> objectsToSwap;

    private void OnEnable()
    {
        OnTabSelected(defaultTab);
    }

    public void Subscribe(ButtonTab button)
    {
        if (buttonTabs.Equals(null))
            buttonTabs = new List<ButtonTab>();

        buttonTabs.Add(button);
    }

    public void OnTabEnter(ButtonTab button)
    {
        ResetTabs();
        if(selectedTab == null || button != selectedTab)
        {
            button.background.sprite = tabHover;
        }
    }

    public void OnTabExit(ButtonTab button)
    {
        ResetTabs();
    }

    public void OnTabSelected(ButtonTab button)
    {
        selectedTab = button;
        //AudioManager.instance.Play("button");
        ResetTabs();
        button.background.sprite = tabActive;
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
                objectsToSwap[i].SetActive(false);
        }
    }

    public void ResetTabs()
    {
        foreach(ButtonTab button in buttonTabs)
        {
            if (selectedTab != null && button == selectedTab) { continue; }
            button.background.sprite = tabidle;
        }
    }
}
