using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//derives from interactable 
public class ItemPickup : Interactable
{
    public Item item;
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }
    void PickUp()
    {
        //Debug.Log("Picking up "+ item.name );
        item.SaveObject(gameObject);
        bool wasPickedUp = Inventory.Instance.Add( item );
        if (wasPickedUp) 
        { 

            gameObject.SetActive( false );
        }


    }
}
