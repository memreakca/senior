using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHıdeInventory : MonoBehaviour
{
    public GameObject inventoryUI;
    public bool isInventoryVisible=false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("is toggled");
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isInventoryVisible = !isInventoryVisible;
        inventoryUI.SetActive(isInventoryVisible);
    }
}
