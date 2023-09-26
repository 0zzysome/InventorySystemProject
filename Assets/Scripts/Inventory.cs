using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //singleton 
    #region singleton
    public static Inventory Instance;
    
    private void Awake()
    {
        
        
            
        
        //should only be one inventory becuase of static, so if there are more then this error will show
        if (Instance != null) 
        {
            Debug.LogWarning("More than one inventory!!");
            return;
        }
        
        Instance = this;
    }
    #endregion

    //deligate
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;
    //needed becuase othewise the onItemChangedCallBack funtion would be null and not add the first item.
    private void Start()
    {
        ItemWasChanged();
    }

    public int inventorySpace = 20;
    public Transform dropPosition;

    //use funtion to refrence cuase public is messy
    private List<Item> items = new List<Item>(); 
    //handles everyting to doi with picking up and item (oh dear god)
    public bool add(Item item)
    {

        Item itemCopy = Instantiate(item);// creates a copy so code never changes actual scriptableobject
        if(!itemCopy.isDefaultItem)
        {
            if(items.Count >= inventorySpace) 
            {
                Debug.Log("no room for more items");
                //returns to avoid adding more than space alows
                return false;
            }
            //checks all items in list 
            for(int i = 0; i < items.Count; i++){
                //ses if the item is already in list and if it can stack
                if (items[i].name == itemCopy.name && itemCopy.stackable) 
                {
                    Debug.Log("was same as in inventory");
                    //the amount is less than the max stack 
                    if (items[i].amount < items[i].maxStack)
                    {
                        //uppdates amount
                        items[i].amount++;
                        //adds refrence of it to the list stack so can be used later
                        items[i].AddObjectToStack(itemCopy.objectRef);
                        Debug.Log("amount was increased on " + items[i].name);
                        ItemWasChanged();
                        return true;
                    }
                    else 
                    {
                        //otherwise make a new item in the list
                        items.Add(itemCopy);
                        ItemWasChanged();
                        return true;
                    }  
                }
            }
            Debug.Log("not found in inventory");
            items.Add(itemCopy);
            
            ItemWasChanged();
        }
        return true;
    }
    //drops the item in front of the player
    public void Drop(Item item)
    {
        item.amount--;
        if(item.amount <= 0)
        {
            items.Remove(item);
            item.objectRef.transform.position = dropPosition.position;
            item.objectRef.SetActive(true);
            
            ItemWasChanged();
        }
        else
        {
            //"drops" item in the itemstack 
            item.itemStack[item.amount - 1].transform.position = dropPosition.position;
            item.itemStack[item.amount-1].SetActive(true); 
            ItemWasChanged();
        }
        
    }
    // removes the item from inventory
    //used for equipment as you dont drop the equipment
    public void Remove(Item item)
    {
        
        items.Remove(item);
            
            

        ItemWasChanged();
        
        

    }


    //made to avoid lists being public problem.
    public List<Item> getItems() 
    {
        return items;
        
    }
    //makes shure funtion is not emty and call out to all funtions lisening to this call
    //uppdates UI
    public void ItemWasChanged() 
    {
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
        else 
        {
            Debug.Log("FUNCTION FOR UPDATEUI WAS NULL!!!!!");
        }
    }
}
