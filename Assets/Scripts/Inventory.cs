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

    //deligate???
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public int inventorySpace = 20;

    //use funtion to refrence cuase public is messy
    private List<Item> items = new List<Item>(); 
    
    public bool add(Item item)
    {
        if(!item.isDefaultItem)
        {
            if(items.Count >= inventorySpace) 
            {
                Debug.Log("no room for more items");
                //returns to avoid adding more than space alows
                return false;
            }
            items.Add(item);
            //makes shure funtion is not emty 
            if(onItemChangedCallBack != null) 
            {
                onItemChangedCallBack.Invoke();
            }
        }
        return true;
    }
    public void remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }
    public List<Item> getItems() 
    {
        return items;
        
    }
}
