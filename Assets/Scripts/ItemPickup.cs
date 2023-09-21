
using UnityEngine;
//derives from interactable 
public class ItemPickup : Interactable
{
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }
    void PickUp()
    {
        Debug.Log("Picking up item");
        //add to inventory 
        Destroy(gameObject);

    }
}
