using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHıdeInventory : MonoBehaviour
{
    [SerializeField] public GameObject CraftingUI;
    [SerializeField] public GameObject inventoryUI;
    
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
        CraftingUI.SetActive(false);
        isInventoryVisible = !isInventoryVisible;
        inventoryUI.SetActive(isInventoryVisible);
        
    }
}
