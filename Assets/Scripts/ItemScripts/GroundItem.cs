using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour , IInteractable
{
    public static GroundItem Instance;
    public ItemObject item;
    public int lootamount;
    public GameObject lootPackage;
    public InventoryObject inventory;

    private void Awake()
    {
        Instance = this;
    }
    public void Interact()
    {
        Debug.Log("interacted wtih player");
        Item _item = new Item(item);
        inventory.AddItem(_item, lootamount);

        Destroy(gameObject);
    }
    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
            
    //    }
    //}


}
