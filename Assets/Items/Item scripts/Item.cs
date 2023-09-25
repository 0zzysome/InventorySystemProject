using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName = "Inventory/Item" )]
public class Item : ScriptableObject
{
    //replaces unitys default name 
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public int amount = 1;
    public bool stackable = false;
    public int maxStack = 20;
    public GameObject objectRef;
    public List<GameObject> itemStack;

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
}
