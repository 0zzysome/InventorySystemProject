using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;

    Inventory inventory;

    InventorySlot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.Instance;
        //now whenever the onItemChangedCallBack is triggered the updateui will fire
        inventory.onItemChangedCallBack += UpdateUI;

        //saves all the inventory slots of children
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void UpdateUI()
    {
        //tests every slot for 
        for (int i = 0; i < slots.Length; i++) 
        {
            //add the item to inventory UI if it is in the list
            if (i < inventory.getItems().Count) 
            {

                

                slots[i].AddItem(inventory.getItems()[i]);

            }
            //otherwise make the UI slot emty
            else 
            {
                slots[i].ClearSlot();
            }
        }
    }
}
