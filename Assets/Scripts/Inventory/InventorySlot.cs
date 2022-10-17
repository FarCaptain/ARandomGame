using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image icon;
    private Item item;
    private RectTransform slotTransform;
    private Inventory inventory;
    private int panelIndex;
    private int slotIndex;

    public virtual void Start()
    {
        inventory = Inventory.instance;
        slotTransform = transform as RectTransform;
        slotIndex = transform.GetSiblingIndex();
        panelIndex = transform.parent.GetSiblingIndex();
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        if(RectTransformUtility.RectangleContainsScreenPoint(slotTransform, Input.mousePosition))
        {
            GameObject draggedItem = eventData.pointerDrag;
            if (draggedItem != null)
            {
                int sourceSlotIndex = inventory.originalParent.GetSiblingIndex();
                inventory.Swap(panelIndex, sourceSlotIndex, slotIndex);
            }
        }
    }

    public void AddItem(Item newItem)
    {
        if (newItem == Item.empty)
        {
            ClearSlot();
        }
        else
        {
            item = newItem;
            icon.sprite = item.icon;
            icon.enabled = true;
        }
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }
}
