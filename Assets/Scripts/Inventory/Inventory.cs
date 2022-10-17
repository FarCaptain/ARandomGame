using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

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
    }
    #endregion

    public Transform draggedObjectParent;
    [HideInInspector] public Transform originalParent;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public int size = 12;

    // ToDo. visible in the inspector
    public List<List<Item>> items;

    private void Start()
    {
        items = new List<List<Item>>();
        int numOfTypes = System.Enum.GetNames(typeof(ItemType)).Length;
        for (int i = 0; i < numOfTypes; i++)
        {
            List<Item> tmp = new List<Item>();
            for (int j = 0; j < size; j++)
            {
                // initialize
                tmp.Add(Item.empty);
            }
            items.Add(tmp);
        }
    }

    public bool Add(Item item)
    {
        int typeIndex = item.typeIndex;
        if (items[typeIndex][size - 1] != null)
            return false;

        if (!item.isDefaultItem)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[typeIndex][i] == Item.empty)
                {
                    items[typeIndex][i] = item;
                    break;
                }
            }

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove(Item item)
    {
        int typeIndex = item.typeIndex;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[typeIndex][i] == item)
                items[typeIndex][i] = Item.empty;
            //thinkbout Garbage Collection
        }

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void Swap(int typeIndex, int srcItemIndex, int destItemIndex)
    {
        Item tempItem = items[typeIndex][destItemIndex];
        items[typeIndex][destItemIndex] = items[typeIndex][srcItemIndex];
        items[typeIndex][srcItemIndex] = tempItem;

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}