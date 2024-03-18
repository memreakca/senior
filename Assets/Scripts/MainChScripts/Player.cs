using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player main;

    public PlayerLevel playerLevel;
    public InventoryObject inventory;
    public InventoryObject equipment;
    public QuestListObject questList;
    public PlayerSwordDamage playerSwordDamage;
    public BarScripts healthbar;
    public BarScripts manabar;
    public TextMeshProUGUI staticInterfaceTXT;

    public GameObject buffeffect;
    public GameObject HealingEffect;
    public GameObject ManaEffect;

    public Image selectedHpConsumable;
    public Image selectedSpConsumable;

    private int boostedINT, boostedAGl, boostedDEF, boostedSTR ,boostedVIT;
    private float boostCooldown;

    [Header("Used Values")]
    public float HP = 100;
    public float SP = 75;
    public float hpRegen;
    public float spRegen;

    [Header("Max Values")]
    public int maxHP;
    public int maxSP;
    public int DEF;
    public int STR;
    public int AGL;
    public int VIT;
    public int INT;


    [Header("Base Values ")]
    [SerializeField] public int baseDefense;
    [SerializeField] public int baseHP;
    [SerializeField] public int baseSP;
    [SerializeField] public int basestrength;
    [SerializeField] public int baseagility;
    [SerializeField] public int basevitality;
    [SerializeField] public int baseintelligence;

    [Header("Modified Values ")]
    public int mdfDefense;
    public int mdfHP ;
    public int mdfSP ;
    public int mdfstrength ;
    public int mdfagility ;
    public int mdfvitality ;
    public int mdfintelligence ;

    public Attribute[] attributes;
    private void Awake()
    {
        main = this;
    }

    public float CalculateDamage(float _damage)
    {
        _damage = _damage - DEF * 0.2f;
        return _damage;
    }
    public void TakeDamage(float damage)
    {
        
        HP = HP - CalculateDamage(damage);
    }
    private void Start()
    {
        healthbar = GetComponent<BarScripts>();
        manabar = GetComponent<BarScripts>();
        playerLevel = GetComponent<PlayerLevel>();

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

    public void UseConsumableHPItem(ItemObject consumableItem, int amount)
    {
        if(HP == maxHP) { return; }
        ApplyConsumableHP(consumableItem, amount);
        inventory.UseConsumable(consumableItem, amount);
       
    }
    public void UseConsumableSPItem(ItemObject consumableItem, int amount)
    {
        if (SP == maxSP) { return; }
        ApplyConsumableSP(consumableItem, amount);
        inventory.UseConsumable(consumableItem, amount);

    }

    public void BoostStats(float cooldown)
    {
        boostCooldown = cooldown;
        boostedINT = baseintelligence *2;
        boostedAGl = baseagility *2;
        boostedDEF = baseDefense *2;
        boostedSTR = basestrength *2;
        boostedVIT = basevitality * 2;

    }

    private ItemObject GetConsumableHPItem()
    {
        // Example: Get the first consumable item from the inventory
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            if (inventory.GetSlots[i].ItemObject != null && inventory.GetSlots[i].ItemObject.type == ItemType.ConsumableHP)
            {
                return inventory.GetSlots[i].ItemObject;
            }
        }

        return null;
    }
    private ItemObject GetConsumableSPItem()
    {
        // Example: Get the first consumable item from the inventory
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            if (inventory.GetSlots[i].ItemObject != null && inventory.GetSlots[i].ItemObject.type == ItemType.ConsumableSP)
            {
                return inventory.GetSlots[i].ItemObject;
            }
        }

        return null;
    }

    private void ApplyConsumableHP(ItemObject consumableItem, int amount)
    {
        if (consumableItem.type == ItemType.ConsumableHP)
        {
            if (consumableItem.data.buffs != null && consumableItem.data.buffs.Length > 0)
            {
                foreach (var buff in consumableItem.data.buffs)
                {
                    switch (buff.attribute)
                    {
                        case Attributes.HP:

                            IncreaseHealth(buff.value * amount);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
    private void ApplyConsumableSP(ItemObject consumableItem, int amount)
    {
        if (consumableItem.type == ItemType.ConsumableSP)
        {
            if (consumableItem.data.buffs != null && consumableItem.data.buffs.Length > 0)
            {
                foreach (var buff in consumableItem.data.buffs)
                {
                    switch (buff.attribute)
                    {
                        case Attributes.SP:
                            IncreaseMana(buff.value * amount);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    private void IncreaseHealth(float amount)
    {
        HealingEffect.SetActive(true);
        HP += amount / 2;
        StartCoroutine(IncreaseHealthCoroutine(amount));
    }

    private System.Collections.IEnumerator IncreaseHealthCoroutine(float amount)
    { 
        yield return new WaitForSeconds(1f); 

        HP += amount/2;
        HP = Mathf.Clamp(HP, 0, maxHP); 
        HealingEffect.SetActive(false); 
    }

    private void IncreaseMana(float amount)
    {
        ManaEffect.SetActive(true);
        SP += amount / 2;
        StartCoroutine(IncreaseManaCoroutine(amount));
    }

    private System.Collections.IEnumerator IncreaseManaCoroutine(float amount)
    {
        yield return new WaitForSeconds(1f);

        SP += amount / 2;
        SP = Mathf.Clamp(SP, 0, maxSP);
        ManaEffect.SetActive(false);
    }
    private void UpdateSelectedConsumableUI()
    {
        if (GetConsumableHPItem() != null)
        {
            selectedHpConsumable.sprite = GetConsumableHPItem().uiDisplay;
            selectedHpConsumable.color = new Color(1, 1, 1, 1);
        }
        else selectedHpConsumable.color = new Color(0, 0, 0, 0);

        if (GetConsumableSPItem() != null)
        {
            selectedSpConsumable.sprite = GetConsumableSPItem().uiDisplay;
            selectedSpConsumable.color = new Color(1, 1, 1, 1);
        }
        else selectedSpConsumable.color = new Color(0, 0, 0, 0);

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

    public void UpdateStaticInterface()
    {
        string statsString = string.Format("Max HP: {0:F1}\nMax SP: {1:F1}\nDEF: {2:F1}\nHP Regen: {3:F1}\nSP Regen: {4:F1}\nSTR: {5}\nAGL: {6}\nVIT: {7}\nINT: {8}",
                                           maxHP, maxSP, DEF , hpRegen, spRegen, STR, AGL, VIT, INT);
        staticInterfaceTXT.text = statsString;
    }
    private void Update()
    {
        if (boostCooldown > 0)
        {
            buffeffect.SetActive(true);
            boostCooldown -= Time.deltaTime;
        }
        else if ( boostCooldown <=  0 && boostCooldown != -1)
        {
            buffeffect.SetActive(false);
            boostCooldown = -1;
            UpdateValues();
        }


        UpdateSelectedConsumableUI();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!inventory.HasItemType(ItemType.ConsumableHP)) { Debug.Log("No Consumable Hp Item"); return; }
            ItemObject consumableItem = GetConsumableHPItem();
            int amountToUse = 1;
            UseConsumableHPItem(consumableItem, amountToUse);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!inventory.HasItemType(ItemType.ConsumableSP)) { Debug.Log("No Consumable Sp Item"); return; }
            ItemObject consumableItem = GetConsumableSPItem();
            int amountToUse = 1;
            UseConsumableSPItem(consumableItem, amountToUse);
        }

        if (SP < maxSP)
        {
            SP += Time.deltaTime * spRegen;
            Mathf.Clamp(SP, 0 , maxSP);
        }

        if (HP < maxHP)
        {
            HP += Time.deltaTime * hpRegen;
            Mathf.Clamp(HP, 0, maxHP);
        }

        healthbar.SetHealth(HP, maxHP);
        manabar.SetMana(SP, maxSP);

        if (Input.GetKeyDown(KeyCode.Space)) 
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
                case Attributes.Defence: mdfDefense = attributes[i].value.modifiedValue; break;
                case Attributes.Agility: mdfagility = attributes[i].value.modifiedValue; mdfHP = attributes[i].value.modifiedValue * 25; break;
                case Attributes.Intelligence: mdfintelligence = attributes[i].value.modifiedValue; mdfSP = attributes[i].value.modifiedValue * 20; break;
                case Attributes.Strength: mdfstrength = attributes[i].value.modifiedValue; break;
                case Attributes.Vitality: mdfvitality = attributes[i].value.modifiedValue; break;
            }
        }

        if (boostCooldown > 0)
        {
            DEF = boostedDEF + mdfDefense;
            maxHP = baseHP + mdfHP;
            maxSP = baseSP + mdfSP;
            STR = boostedSTR + mdfstrength;
            VIT = boostedVIT + mdfvitality;
            INT = boostedINT + mdfintelligence;
            AGL = boostedAGl + mdfagility;
            if (AGL > 10) { hpRegen = AGL * 0.6f; } else hpRegen = AGL * 0.2f;
            if (INT > 10) { spRegen = INT * 0.6f; } else spRegen = INT * 0.2f;
            UpdateStaticInterface();
        }
        else

        {
            DEF = baseDefense + mdfDefense;
            maxHP = baseHP + mdfHP;
            maxSP = baseSP + mdfSP;
            STR = basestrength + mdfstrength;
            VIT = basevitality + mdfvitality;
            INT = baseintelligence + mdfintelligence;
            AGL = baseagility + mdfagility;
            if (AGL > 10) { hpRegen = AGL * 0.6f; } else hpRegen = AGL * 0.2f;
            if (INT > 10) { spRegen = INT * 0.6f; } else spRegen = INT * 0.2f;
            UpdateStaticInterface();

        }
        
    }

    internal void UpdateBaseStats()
    {
        baseHP += playerLevel.currentLevel * 100;
        baseSP += playerLevel.currentLevel * 30;
        basestrength += playerLevel.currentLevel ;
        baseagility += playerLevel.currentLevel ;
        baseintelligence += playerLevel.currentLevel ;
        basevitality += playerLevel.currentLevel ;
        UpdateValues();
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