using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RagdollEnemy : MonoBehaviour
{
   
    public NavMeshAgent agent;
    
    public GameObject enemy;
    public GameObject enemyRagdoll;

    public float minRagdollTime;

     float ragdollTime;
    private Vector3 temp;
    private void Awake()
    {
        enemyRagdoll.SetActive(false);
        ragdollTime = minRagdollTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        Interactable interactableComponent = other.GetComponent<Interactable>();
        //checks if it acctually saved a component correctly. 
        if (interactableComponent != null)
        { 
            agent.isStopped = true;
            enemyRagdoll.SetActive(true);
            enemyRagdoll.transform.parent = null;
            enemy.SetActive(false);
            ragdollTime = other.GetComponent<Rigidbody>().mass; 
            Invoke(nameof(getEnemyBack), ragdollTime);
            ragdollTime = minRagdollTime;
        }
    }

    void getEnemyBack() 
    {
        enemy.SetActive(true);
        temp = new Vector3(enemyRagdoll.transform.position.x, enemyRagdoll.transform.position.y + 10f, enemyRagdoll.transform.position.z);
        enemy.transform.position = temp;
        enemyRagdoll.transform.parent = enemy.transform;
        enemyRagdoll.transform.localPosition = new Vector3(0,0,0);
        enemyRagdoll.transform.localRotation = Quaternion.identity;
        enemyRagdoll.SetActive(false);
    }
}
