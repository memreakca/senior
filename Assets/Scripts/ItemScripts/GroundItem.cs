using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour 
{
    public ItemObject item;
    public GameObject lootPackage;
    public InventoryObject inventory;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Item _item = new Item(item);
            inventory.AddItem(_item, 1);

            Destroy(gameObject);
        }
    }


}
