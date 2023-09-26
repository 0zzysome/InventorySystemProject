using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Prop Item", menuName = "Inventory/Item/Prop")]
public class PropItem : Item
{

    public override void Use()
    {
        base.Use();
        // has to be before or else the item equiped will try and add an item to a full inventory and just delete it. 
        RemoveFromInventory();
        //equips item in the list 
        EquipmentManager.Instance.Equip(this);
        
    }
}
