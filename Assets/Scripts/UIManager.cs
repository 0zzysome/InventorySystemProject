using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject inGameHUD;
    [SerializeField] GameObject inventoryHUD;
    [SerializeField] TMP_Text valueText; 
    private bool isInInventory;
    
    Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.Instance;
        inventory.onItemChangedCallBack += UpdateMoney;
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
            Time.timeScale = 0f;
            isInInventory = true;
            return;
        }
        // toggles inventory off
        if (Input.GetButtonDown("OpenInventory") && isInInventory == true)
        {
            ShowInGameHUD();
            Time.timeScale = 1f;
            isInInventory = false;
            return;
        }
    }
    public void HideAllHUDs() 
    {
        inGameHUD.SetActive(false);
        inventoryHUD.SetActive(false);  
    }
    public void ShowInventory() 
    {
        HideAllHUDs();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        inventoryHUD.SetActive(true);
    }
    public void ShowInGameHUD()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        HideAllHUDs();
        inGameHUD.SetActive(true );
    }
    void UpdateMoney() 
    {
        
        float total = 0; 
        for (int i = 0; i < inventory.items.Count; i++)
        {
            total += inventory.items[i].totalWorth;
            //Debug.Log("added " + inventory.items[i].totalWorth + " to total");
        }
        //if there is an item equiped
        if(EquipmentManager.Instance.currentEquipment[0] != null) 
        {
            //add it to the total
            EquipmentManager.Instance.currentEquipment[0].UppdateWorth();
            total += EquipmentManager.Instance.currentEquipment[0].totalWorth;
        }
        valueText.text = total.ToString();
        //Debug.Log("Uppdated total worth "+ total);
    }
}
