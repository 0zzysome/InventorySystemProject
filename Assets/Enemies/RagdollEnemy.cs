using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RagdollEnemy : MonoBehaviour
{
   
    public NavMeshAgent agent;
    
    public GameObject enemy;
    public GameObject enemyRagdoll;
    public float cooldownRagdoll;
    public float minRagdollTime;
    float cooldownEndsTime;
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
            if(Time.time > cooldownEndsTime) 
            {
                agent.isStopped = true;
                enemyRagdoll.SetActive(true);
                enemyRagdoll.transform.parent = null;
                enemy.SetActive(false);
                ragdollTime = other.GetComponent<Rigidbody>().mass;
                /*
                if (other.GetComponent<Rigidbody>() != null) 
                {
                    
                    Debug.Log("BOOM");
                    other.GetComponent<Rigidbody>().AddExplosionForce(6f, transform.position, 3f, 0.5f, ForceMode.Impulse);
                }
                */
                cooldownEndsTime = Time.time + cooldownRagdoll;
                Invoke(nameof(GetEnemyBack), ragdollTime);
                ragdollTime = minRagdollTime;
            }
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Interactable interactableComponent = other.GetComponent<Interactable>();
        //checks if it acctually saved a component correctly. 
        if (interactableComponent != null)
        {
            if (Time.time > cooldownEndsTime)
            {
                agent.isStopped = true;
                enemyRagdoll.SetActive(true);
                enemyRagdoll.transform.parent = null;
                enemy.SetActive(false);
                ragdollTime = other.GetComponent<Rigidbody>().mass;
                /*
                if (other.GetComponent<Rigidbody>() != null) 
                {
                    
                    Debug.Log("BOOM");
                    other.GetComponent<Rigidbody>().AddExplosionForce(6f, transform.position, 3f, 0.5f, ForceMode.Impulse);
                }
                */
                cooldownEndsTime = Time.time + cooldownRagdoll;
                Invoke(nameof(GetEnemyBack), ragdollTime);
                ragdollTime = minRagdollTime;
                
            }

        }
    }
    void GetEnemyBack() 
    {
        
        //makes enemy apear again
        enemy.SetActive(true);
        // saver new position to variable becuase you cant change each position induvidual
        temp.x = enemyRagdoll.transform.position.x;
        temp.y = enemyRagdoll.transform.position.y + 10f;
        temp.z = enemyRagdoll.transform.position.z;
        // applies the posiotion to the enemy
        enemy.transform.position = temp;
        //puts the ragdoll back as a child and hides it.
        enemyRagdoll.transform.parent = enemy.transform;
        enemyRagdoll.transform.localPosition = new Vector3(0,0,0);
        enemyRagdoll.transform.localRotation = Quaternion.identity;
        enemyRagdoll.SetActive(false);
    }
}
