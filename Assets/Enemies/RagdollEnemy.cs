using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RagdollEnemy : MonoBehaviour
{
    public Rigidbody body;
    public NavMeshAgent agent;
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Interactable>();
        ;
        if (other.tag == "Item") 
        {
            
        }
        Interactable interactableComponent = other.GetComponent<Interactable>();
        //checks if it acctually saved a component correctly. 
        if (interactableComponent != null)
        {
            agent.isStopped = true;
            body.isKinematic = false;
            body.useGravity = true;
            other.isTrigger = false;
        }
    }
}
