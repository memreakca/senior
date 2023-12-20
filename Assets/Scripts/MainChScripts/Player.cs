using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject equipment;
    public QuestListObject questList;


    [Header("Used Values")]
    public float HP = 100;
    public float SP = 75;
    public float hpRegen;

    [Header("Max Values")]
    public int maxHP;
    public int maxSP;
    public int STR;
    public int AGL;
    public int VIT;
    public int INT;


    [Header("Base Values ")]
    [SerializeField] public int baseHP;
    [SerializeField] public int baseSP;
    [SerializeField] public int basestrength;
    [SerializeField] public int baseagility;
    [SerializeField] public int basevitality;
    [SerializeField] public int baseintelligence;

    [Header("Modified Values ")]
    public int mdfHP ;
    public int mdfSP ;
    public int mdfstrength ;
    public int mdfagility ;
    public int mdfvitality ;
    public int mdfintelligence ;

    public Attribute[] attributes;

    private void Start()
    {
       

        for (int i = 0; i < attributes.Length; i++)
        {     
            attributes[i].SetParent(this);
        }
        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }
        UpdateValues();
        HP = maxHP; SP = maxSP;
    }

    
    public void OnBeforeSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemID == -1)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print(string.Concat("Removed ", _slot.ItemObject, " on ", _slot.parent.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i]);//playerýn attribute deðerleri azalýr
                    }
                }
                UpdateValues();

                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }
    public void OnAfterSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemID == -1)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print(string.Concat("Placed ", _slot.ItemObject, " on ", _slot.parent.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.AddModifier(_slot.item.buffs[i]);//playerýn attribute deðerleri artar
                    }
                }
                UpdateValues();

                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        var groundItem = other.GetComponent<GroundItem>();
        if (groundItem)
        {
            Item _item = new Item(groundItem.item);
            inventory.AddItem(_item, 1);
            
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            questList.mainQuestObjects[0].CompleteQuest();
        }
        if (HP < maxHP)
        {
            HP += Time.deltaTime * hpRegen;
        }
        else { HP = maxHP; }

        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            inventory.Save();
            equipment.Save();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
            equipment.Load();
        }

    }

    public void AttributeModified(Attribute attribute)
    {
        Debug.Log(string.Concat(attribute.type, "is updated , New Value = ", attribute.value.ModifiedValue));
    }
    private void OnApplicationQuit()
    {
        inventory.Clear();
        equipment.Clear();
    }

    public void UpdateValues()
    {
        if (maxHP < HP) { HP = maxHP; }
        if (maxSP < SP ) { SP = maxSP; } 

        for (int i = 0; i < attributes.Length; i++)
        {
            switch (attributes[i].type)
            {
                case Attributes.Agility: mdfagility = attributes[i].value.modifiedValue; mdfHP = attributes[i].value.modifiedValue * 25; break;
                case Attributes.Intelligence: mdfintelligence = attributes[i].value.modifiedValue; mdfSP = attributes[i].value.modifiedValue * 20; break;
                case Attributes.Strength: mdfstrength = attributes[i].value.modifiedValue; break;
                case Attributes.Vitality: mdfvitality = attributes[i].value.modifiedValue; break;
            }
        }

        maxHP = baseHP + mdfHP;
        maxSP = baseSP + mdfSP;
        STR = basestrength + mdfstrength;
        VIT = basevitality + mdfvitality;
        INT = baseintelligence + mdfintelligence;
        AGL = baseagility + mdfagility;
        hpRegen = AGL * 0.2f;
        
    }
}

[System.Serializable]
public class Attribute
{
    [System.NonSerialized]
    public Player parent;
    public Attributes type;
    public ModifiableInt value;

    public void SetParent(Player _parent)
    {
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
    }

    public void AttributeModified()
    {
        parent.AttributeModified(this);
    }
}