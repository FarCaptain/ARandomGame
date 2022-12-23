using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryEquipmentSlot : InventorySlot
{

    private EquipmentManager equipmentManager;
    public EquipmentSlot slotType;

    public override void Start()
    {
        base.Start();
        equipmentManager = EquipmentManager.instance;
        equipmentManager.onEquipmentChanged += UpdateSlot;

        RefreshSlotUI();
    }

    public override void OnDrop(PointerEventData eventData)
    {
        //if (RectTransformUtility.RectangleContainsScreenPoint(slotTransform, Input.mousePosition))
        //{
        //    GameObject draggedItem = eventData.pointerDrag;
        //    if (draggedItem != null)
        //    {
        //        draggedItem.GetComponent<>
        //        equipmentManager.currentEquipments[]
        //    }
        //}
    }

    public void UpdateSlot(Equipment newItem, Equipment oldItem)
    {
        if (newItem == null && oldItem == null)
            return;

        EquipmentSlot type = newItem ? newItem.equipSlot : oldItem.equipSlot;
        if (type != slotType)
            return;

        AddItem(newItem);
    }

    public void UnequipItem()
    {
        equipmentManager.Unequip((int)slotType);
    }

    public void RefreshSlotUI()
    {
        ClearSlot();
        equipmentManager.RefreshEquipmentUI();
    }
}
