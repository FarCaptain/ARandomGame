using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITabBehavior : MonoBehaviour
{
    [SerializeField] private Transform tabParent;
    [SerializeField] private Transform tabContentParent;

    private Transform[] tabs;
    private Transform[] tabContents;

    void Start()
    {

        tabs = new Transform[tabParent.childCount];
        for (int i = 0; i < tabs.Length; i++)
            tabs[i] = tabParent.GetChild(i);

        tabContents = new Transform[tabContentParent.childCount];
        for (int i = 0; i < tabContents.Length; i++)
            tabContents[i] = tabContentParent.GetChild(i);

    }

    public void switchTab(int tabIndex)
    {
        foreach (Transform tab in tabs)
            tab.GetComponent<Image>().enabled = (tab != tabs[tabIndex]);

        foreach (Transform content in tabContents)
            content.gameObject.SetActive(content == tabContents[tabIndex]);
    }

}
