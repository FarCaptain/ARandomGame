using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    public Equipment[] currentEquipments;
    Inventory inventory;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public event OnEquipmentChanged onEquipmentChanged;

    #region Singleton
    public static EquipmentManager instance;

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

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipments = new Equipment[numSlots];
    }
    #endregion

    private void Start()
    {
        // Equipment init has dependency on Inventory, no good
        inventory = Inventory.instance;
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = currentEquipments[slotIndex];
        if ( oldItem != Item.empty)
        {
            inventory.Add(oldItem);
        }

        onEquipmentChanged?.Invoke(newItem, oldItem);

        currentEquipments[slotIndex] = newItem;
        Debug.Log($"Equip {newItem} at {slotIndex}");
    }

    public void Unequip(int slotIndex)
    {
        Equipment oldItem = currentEquipments[slotIndex];
        if (oldItem != null)
        {
            inventory.Add(oldItem);
            currentEquipments[slotIndex] = null;
        }

        //ToDo. Might be better with only TypeId?
        onEquipmentChanged?.Invoke(null, oldItem);
    }

    public bool IsEquiping(string equipmentName)
    {
        foreach (Equipment item in currentEquipments)
        {
            if (item != null && item.name == equipmentName)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsEquipingWithType(EquipmentSlot slotId, string equipmentName)
    {
        //Debug.Log($"Finding {equipmentName} at {(int)slotId}");
        Equipment equipment = currentEquipments[(int)slotId];
        if (equipment != null && equipment.name == equipmentName)
            return true;
        if (equipment != null)
        {
            Debug.Log($"{equipmentName} not equals to {equipment.name}");
        }
        else
        {
            //Debug.Log($"{equipmentName} not exist");
        }
        
        return false;
    }
}
