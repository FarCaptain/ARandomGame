using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseObjectTooltip : MonoBehaviour
{

    [SerializeField] private GameObject objectTooltipTemplate;
    [SerializeField] private Transform anchor;
    [SerializeField] private float showDuration = 5.0f;
    private GameObject objectTooltip;
    private Transform canvas;

    [TextArea]
    public string description;
    // Start is called before the first frame update

    private void Start()
    {
        if (anchor == null)
            anchor = transform;
        canvas = objectTooltipTemplate.GetComponent<ObjectTooltip>().canvasRectTransform.transform;
    }

    private void OnMouseDown()
    {
        if (objectTooltip != null)
            return;

        objectTooltip = Instantiate(objectTooltipTemplate, transform.position, Quaternion.identity);
        // object Tooltip should be in the lowest layer on Canvas.
        objectTooltip.transform.SetParent(canvas);
        objectTooltip.transform.SetAsFirstSibling();
        objectTooltip.SetActive(true);

        ObjectTooltip tooltip = objectTooltip.GetComponent<ObjectTooltip>();

        tooltip.SetAnchor(anchor);

        tooltip.setText(description);
        StartCoroutine(StartTimer());
        //Destroy(objectTooltip, showDuration);
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(showDuration);
        Destroy(objectTooltip);
        //ToDo. Fade out
    }
}
