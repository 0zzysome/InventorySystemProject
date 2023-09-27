using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "New Prop Item", menuName = "Inventory/Item/Prop")]
public class PropItem : Item
{

    public override void UseInInventory()
    {
        base.UseInInventory();
        EquipItem();
        
    }
    public override void Use()
    {
        base.Use();
        ThrowItem();
    }
    public void ThrowItem()
    {
        //EquipmentManager.Instance.ThrowItem(this);
    }
}
