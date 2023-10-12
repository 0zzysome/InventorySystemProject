using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
   
    public float timeInScene = 4;
    float timeWhenDestroyed;
    bool hasDroppedItem = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            if (!hasDroppedItem) // makes shure that only one item is dropped.
            {
                Inventory.Instance.DroppRandomItem();
                hasDroppedItem = true;
            }
        }
    }
    private void OnTriggerStay(Collider other) // checks if its already 
    {
        if (other.CompareTag("Player"))
        {
            if (!hasDroppedItem) // makes shure that only one item is dropped.
            {
                Inventory.Instance.DroppRandomItem();
                hasDroppedItem = true;
            }
        }
    }
    void Start()
    {
        //makes a timer for when it shuld be destroyed
        timeWhenDestroyed = Time.time + timeInScene;
        hasDroppedItem= false;
    }
    void Update()
    {
        //destroys projectile when timer is up.
        if(timeWhenDestroyed < Time.time) 
        {
            // make it destry enemy and not script.
             
            Destroy(gameObject);
        }
    }
}
