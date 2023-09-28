using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New Item",menuName = "Inventory/Item" )]
public class Item : ScriptableObject
{
    //replaces unitys default name 
    new public string name = "New Item";
    public Sprite icon = null;
    public EquipSlot equipSlot;
    public bool isDefaultItem = false;
    public int amount = 1;
    public bool stackable;
    public int maxStack = 20;
    public GameObject objectRef;
    public List<GameObject> itemStack;
    Inventory inventory;
    EquipmentManager equipmentManager;
    private void Awake()
    {
        inventory  = Inventory.Instance;
        equipmentManager = EquipmentManager.Instance;
    }
    public virtual void UseInInventory() 
    {
        Debug.Log("Interacted with " + name + " in the inventory");
        
    }
    public virtual void AlternativeUse() 
    {
        Debug.Log("Interacted with " + name + " while in hand");

    }
    public void SaveObject(GameObject obj ) 
    {
        objectRef = obj;
    }
    public void ClearObject()
    {
        objectRef = null;
    }
    public void AddObjectToStack(GameObject obj) 
    {
        
        itemStack.Add( obj );
        
    }
    public void RemoveFromInventory() 
    {
        Inventory.Instance.Remove(this);
    }

    public void ToggleIsTrigger(bool Toggle)
    {
        if (objectRef.GetComponent<MeshCollider>() != null)
        {
            objectRef.GetComponent<MeshCollider>().isTrigger = Toggle; 
        }
        else
        {
            Debug.LogError("ERROR: COULD NOT FIND MESH COLLIDER!!");
        }


    }
    public void EquipItem() 
    {
        // has to be before or else the item equiped will try and add an item to a full inventory and just delete it. 
        RemoveFromInventory();
        //equips item in the list 
        
        EquipmentManager.Instance.Equip(this);
    }

    public void ThrowItem(Item item)
    {
        if (item == null)
        {
            Debug.LogError("ERROR: player tried to  throw item but item was not found!");
            return;

        }
        item.amount--;
        //variable for later
        Vector3 saveScale;

        if (item.amount <= 0)
        {
            inventory.items.Remove(item);

            EquipmentManager.Instance.IsHoldingItem = false;
            // saves original scale
            saveScale = item.objectRef.transform.localScale;
            //removes the object form the hand. aka unparents it. 
            item.objectRef.transform.parent = null;
            //applies correct scale after unparent 
            item.objectRef.transform.localScale = saveScale;
            //
            item.objectRef.transform.position = inventory.throwPosition.position;
            //item becomes vissable (prob not needed but whatever)
            item.objectRef.SetActive(true);

            item.ToggleIsTrigger(false);

            //launches item
            if (item.objectRef.GetComponent<Rigidbody>() != null)
            {
                item.objectRef.transform.rotation = inventory.throwPosition.rotation;
                item.objectRef.GetComponent<Rigidbody>().AddForce(inventory.throwPosition.forward * PlayerMovement.throwStrengthMult, ForceMode.Impulse);
            }
            else
            {
                Debug.LogError("NO RIGIDBODY FOUND");
            }

            equipmentManager.currentEquipment[0] = null;
            inventory.ItemWasChanged();
        }
        // for the stacked items in list 
        else
        {
            // saves original scale
            saveScale = item.itemStack[item.amount - 1].transform.localScale;
            //removes the object form the hand. aka unparents it. 
            item.itemStack[item.amount - 1].transform.parent = null;
            //applies correct scale after unparent 
            item.itemStack[item.amount - 1].transform.localScale = saveScale;

            //sets posiotion to corrtect position.
            item.itemStack[item.amount - 1].transform.position = inventory.throwPosition.position;
            // makes item visable
            item.itemStack[item.amount - 1].SetActive(true);

            
            //launches item
            if (item.itemStack[item.amount - 1].GetComponent<Rigidbody>() != null)
            {
                item.objectRef.transform.rotation = inventory.throwPosition.rotation;
                item.itemStack[item.amount - 1].GetComponent<Rigidbody>().AddForce(inventory.throwPosition.forward * PlayerMovement.throwStrengthMult, ForceMode.Impulse);
            }
            else
            {
                Debug.LogError("NO RIGIDBODY FOUND");
            }
            inventory.ItemWasChanged();
        }
    }
}
public enum EquipSlot { Weapon, Chest, Head }