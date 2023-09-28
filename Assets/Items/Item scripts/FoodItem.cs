using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Food Item", menuName = "Inventory/Item/Food")]
public class FoodItem : Item
{
    public int healthRestoration;
    public override void UseInInventory()
    {
        base.UseInInventory();
        EquipItem();
        
    }
    public override void AlternativeUse()
    {
        base.AlternativeUse();
        ThrowItem(this);
    }
}
