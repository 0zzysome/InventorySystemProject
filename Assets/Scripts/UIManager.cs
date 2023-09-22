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
        ShowInGameHUD();
    }

    private void Update()
    {
        if (Input.GetButtonDown("OpenInventory")&& isInInventory == false)
        {
            ShowInventory();
            isInInventory = true;
            return;
        }
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
