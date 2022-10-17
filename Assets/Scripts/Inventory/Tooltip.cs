using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private RectTransform canvasRectTransform;

    private RectTransform backgroundRectTransform;
    private RectTransform rectTransform;
    private TextMeshProUGUI text;
    [SerializeField] private Vector2 padding = new Vector2(8f, 8f);

    #region Singleton
    public static Tooltip instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        backgroundRectTransform = transform.Find("Panel").GetComponent<RectTransform>();
        rectTransform = transform.GetComponent<RectTransform>();
        text = transform.Find("Text").GetComponent<TextMeshProUGUI>();

        //setText("Hello Worldjigorjiojojfieojfeifijei\nfeijfieoafe\niiijfeijfie\n111");
        HideTooltip();
    }
    #endregion

    private void setText(string tooltipString)
    {
        text.SetText(tooltipString);
        text.ForceMeshUpdate();

        Vector2 textSize = text.GetRenderedValues(false);
        backgroundRectTransform.sizeDelta = textSize + padding;
    }

    private void Update()
    {
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

        anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, 0, canvasRectTransform.rect.width - backgroundRectTransform.rect.width);
        anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, 0, canvasRectTransform.rect.height - backgroundRectTransform.rect.height);

        rectTransform.anchoredPosition = anchoredPosition;
    }

    // ToDo. Might be better ways
    static public void ShowTooltip(string tooltipText)
    {
        if (instance.gameObject.activeSelf == true)
            return;
        instance.gameObject.SetActive(true);
        instance.setText(tooltipText);
    }

    static public void HideTooltip()
    {
        instance.gameObject.SetActive(false);
    }
}
