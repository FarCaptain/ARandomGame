using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;
    private List<InventorySlot[]> slots = new List<InventorySlot[]>();
    private int numberOfItemTypes;

    public Transform PanelParent;
    private Transform[] itemParents;
    public GameObject inventoryUI;

    private void OnDestroy()
    {
        Inventory.instance.onItemChangedCallback -= UpdateUI;
    }

    void Start()
    {
        // Hey this is the intialization problem
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        numberOfItemTypes = System.Enum.GetNames(typeof(ItemType)).Length;

        //itemParents = PanelParent.
        itemParents = new Transform[numberOfItemTypes];
        for (int i = 0; i < numberOfItemTypes; i++)
            itemParents[i] = PanelParent.GetChild(i);

        for (int i = 0; i < numberOfItemTypes; i++)
        {
            InventorySlot[] temp = itemParents[i].GetComponentsInChildren<InventorySlot>();
            slots.Add(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.I))
        //{
        //    inventoryUI.SetActive(!inventoryUI.activeSelf);
        //}
    }

    void UpdateUI()
    {
        //ToDo.Update Only One Panel
        Debug.Log("Updating UI");

        for (int i = 0; i < numberOfItemTypes; i++)
        {
            for (int j = 0; j < slots[i].Length; j++)
            {
                slots[i][j].AddItem(inventory.items[i][j]);
            }
        }
}
}
