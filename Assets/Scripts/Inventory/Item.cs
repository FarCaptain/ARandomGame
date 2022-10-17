using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    [TextArea]
    public string description;
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public bool isUsable = true;

    public static Item empty;

    public Sphinx.GameEvent onUseEvent;

    [HideInInspector]
    public int typeIndex;

    private void OnEnable()
    {
        string typeName = GetType().Name;
        switch (typeName)
        {
            case "Equipment":
                typeIndex = (int)ItemType.Clothes;
                break;
            case "Item":
                typeIndex = (int)ItemType.Items;
                break;
            case "Evidence":
                typeIndex = (int)ItemType.Evidence;
                break;
            case "Interact":
                typeIndex = (int)ItemType.Interact;
                break;
        }

        //Debug.Log(name + " is a " + typeName);
    }
    public virtual void Use()
    {
        if (!isUsable)
            return;
        Debug.Log("*Using " + name);

        if (onUseEvent != null)
            onUseEvent.Raise();
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}

enum ItemType
{
    Clothes,
    Items,
    Evidence,
    Interact
}
