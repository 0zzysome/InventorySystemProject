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
}
