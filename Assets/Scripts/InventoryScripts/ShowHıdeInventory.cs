using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHıdeInventory : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject CraftingUI;
    public bool isInventoryVisible;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }


    public void ToggleInventory()
    {
       
        isInventoryVisible = !isInventoryVisible;
        inventoryUI.SetActive(isInventoryVisible);
        CraftingUI.SetActive(false);
    }
}
