using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;

    private void Awake()
    {
        Instance = this;

    }

    Item[] currentEquipment;

    Inventory inventory;
    private void Start()
    {
        inventory = Inventory.Instance;
        //makes the list of equipable items the same size as there can be items equiped
        int numSlots = System.Enum.GetNames(typeof(EquipSlot)).Length;
        currentEquipment = new Item[numSlots];
    }
    public void Equip(Item newitem) 
    {
        int slotIndex = (int)newitem.equipSlot;
        Item olditem = null;
        // if there is an item already in that space 
        if (currentEquipment[slotIndex] != null)
        {
            //save the old item 
            olditem = currentEquipment[slotIndex];
            //put the item back in the inventory
            Debug.Log("CREATED OLD ITEM");
            olditem.stackable = false;
            inventory.add(olditem);
            olditem.stackable = true;
        }

        currentEquipment[slotIndex] = newitem;
    }
}
