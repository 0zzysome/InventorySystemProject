
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void Interact() 
    {
        Debug.Log("interacting with " + transform.name);
    }
}
