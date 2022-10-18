using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    private CanvasGroup canvasGroup;
    private Inventory inventory;
    private int panelIndex;
    private int itemIndex;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        inventory = Inventory.instance;
        // the panel this type of Item belongs to, uses the index of the TabContent
        panelIndex = transform.parent.parent.GetSiblingIndex();
        itemIndex = transform.parent.GetSiblingIndex();

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // to handle the render order
        inventory.originalParent = transform.parent;
        transform.SetParent(inventory.draggedObjectParent);
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(inventory.originalParent);
        transform.localPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Index = " + itemIndex);
        Item item = inventory.items[panelIndex][itemIndex];
        if (item == null)
            Debug.LogError("WTF");
        string name = item.name;
        if (item.isUsable == false)
            name += " (Unusable)";
        Tooltip.ShowTooltip(name + ": \n" + item.description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.HideTooltip();
    }
}
