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
    

    public virtual void UseInInventory() 
    {
        Debug.Log("Interacted with " + name + " in the inventory");
        
    }
    public virtual void Use() 
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
}
public enum EquipSlot { Weapon, Chest, Head }