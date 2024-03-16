using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour, IInteractable
{
    public ItemObject item;
    public int amount;
    public InventoryObject inventory;
    private void Start()
    {

    }
    public void Interact()
    {
        Debug.Log("interacted wtih player");
        Item _item = new Item(item);
        inventory.AddItem(_item, amount);
        Destroy(gameObject);
    }
}