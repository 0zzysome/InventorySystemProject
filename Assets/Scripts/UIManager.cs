using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject inGameHUD;
    [SerializeField] GameObject InventoryHUD;

    private bool isInInventory;

    // Start is called before the first frame update
    void Start()
    {
        //used becuase othewise the inventory could not load and have its values saved
        //leading to fist item not being saved
        Invoke("ShowInGameHUD", 0.1f);
        
    }

    private void Update()
    {
        // toggles inventory on
        if (Input.GetButtonDown("OpenInventory") && isInInventory == false)
        {
            ShowInventory();
            isInInventory = true;
            return;
        }
        // toggles inventory off
        if (Input.GetButtonDown("OpenInventory") && isInInventory == true)
        {
            ShowInGameHUD();
            isInInventory = false;
            return;
        }
    }
    public void HideAllHUDs() 
    {
        inGameHUD.SetActive(false);
        InventoryHUD.SetActive(false);  
    }
    public void ShowInventory() 
    {
        HideAllHUDs();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        InventoryHUD.SetActive(true);
    }
    public void ShowInGameHUD()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        HideAllHUDs();
        inGameHUD.SetActive(true );
    }
}
