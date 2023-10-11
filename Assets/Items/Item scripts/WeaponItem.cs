using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New weapon Item", menuName = "Inventory/Item/Weapon")]
public class WeaponItem : Item
{

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
    public override void Use()
    {
        base.Use();
        Debug.Log("not implemented anything yet");
    }
    void swing() 
    {
            
    }
}
