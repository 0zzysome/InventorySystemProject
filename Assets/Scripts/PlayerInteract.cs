using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    Camera cam;
    public float reach;
    
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            if (Cursor.visible == false)
            {
                RaycastHit hit;
                if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, reach)) 
                { 
                    //saves the component 
                    Interactable interactableComponent = hit.collider.GetComponent<Interactable>();
                    //checks if it acctually saved a component correctly. 
                    if (interactableComponent != null ) 
                    {
                        interactableComponent.Interact();
                    
                    }
                
                }
            }

            
        }
    }
}
