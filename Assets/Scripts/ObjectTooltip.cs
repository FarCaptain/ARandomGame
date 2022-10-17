using UnityEngine;
using TMPro;

public class ObjectTooltip : MonoBehaviour
{
    public RectTransform canvasRectTransform;

    private RectTransform backgroundRectTransform;
    private RectTransform rectTransform;
    private TextMeshProUGUI text;
    [SerializeField] private Vector2 padding = new Vector2(8f, 8f);
    [SerializeField] new private Camera camera;

    private void Awake()
    {
        backgroundRectTransform = transform.Find("Panel").GetComponent<RectTransform>();
        rectTransform = transform.GetComponent<RectTransform>();
        text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    public void setText(string tooltipString)
    {
        text.SetText(tooltipString);
        text.ForceMeshUpdate();

        Vector2 textSize = text.GetRenderedValues(false);
        backgroundRectTransform.sizeDelta = textSize + padding;
    }

    public void SetAnchor(Transform objectAnchor)
    {
        Vector2 anchoredPosition = camera.WorldToScreenPoint(objectAnchor.position) / canvasRectTransform.localScale.x;

        anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, 0, canvasRectTransform.rect.width - backgroundRectTransform.rect.width);
        anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, 0, canvasRectTransform.rect.height - backgroundRectTransform.rect.height);

        rectTransform.anchoredPosition = anchoredPosition;
    }
}
