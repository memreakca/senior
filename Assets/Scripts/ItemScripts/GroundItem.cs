using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour 
{
    public ItemObject item;
    public GameObject lootPackage;
    public InventoryObject inventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Item _item = new Item(item);
            inventory.AddItem(_item, 1);

            Destroy(gameObject);
        }
    }
}
