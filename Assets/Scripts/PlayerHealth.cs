using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //can only be one playerhealth
    #region singleton
    public static PlayerHealth Instance;

    private void Awake()
    {
        //should only be one inventory becuase of static, so if there are more then this error will show
        if (Instance != null)
        {
            Debug.LogWarning("More than one Playerhealth!!");
            return;
        }

        Instance = this;
    }
    #endregion

    public int currentHealth;
    public int maxHealth;

    public UIHealth healthBar;
    

    private void Start()
    {
        
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
    }
    public bool ChangeHealth(int changeAmount) 
    {
        
        int oldHealth = currentHealth;
        currentHealth += changeAmount;
        //ses if you are at max health
        //you are not
        //makes shure it never goes above max health 
        if (currentHealth >= maxHealth) 
        {
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth); 
            
        }
       
        //kills player
        if (currentHealth <= 0 ) 
        {
            Destroy(gameObject);
            Debug.Log("player died");
            return true;
        }
        // checks if health changed
        healthBar.SetHealth(currentHealth);
        if (currentHealth == oldHealth) 
        {
            return false;
        }
        else 
        {
            return true;
        }
        
        
        
        
        

        

        
        
    }
}
