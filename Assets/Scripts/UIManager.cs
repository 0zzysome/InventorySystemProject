using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject inGameHUD;
    [SerializeField] GameObject InventoryHUD;
    


    // Start is called before the first frame update
    void Start()
    {
        HideAllHUDs();
        inGameHUD.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetButtonDown("OpenInventory"))
        {
            ShowInventory();
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
}
