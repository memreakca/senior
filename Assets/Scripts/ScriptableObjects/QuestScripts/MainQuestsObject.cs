using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Quest ", menuName = "Quest System/Quest")]
public class MainQuestsObject : ScriptableObject
{
    public bool isCompleted = false;
    public string questName;
    public QuestType questType;
    public GatheredItems[] itemsToGather;
    public InventoryObject inventoryObject;

    public void CompleteQuest()
    {    
        switch (questType)
        {
            case QuestType.Gather:
                bool allItemsFound = true;
                if (!isCompleted) 
                    {
                    for (int i = 0; i < itemsToGather.Length; i++)
                    {
                        if (!inventoryObject.ContainsItem(itemsToGather[i].item, itemsToGather[i].amount))
                        {
                            allItemsFound = false;
                            break;
                        }
                    }
                    if (allItemsFound)
                    {
                        Debug.Log("You have all the items , Quest Completed");
                        isCompleted = true;
                    }
                    else
                    {
                        Debug.Log("You dont have enough items to finish quest");
                    }
                }
                
            break;
            case QuestType.Kill: break;
            case QuestType.Craft: break;
        }
    }
}
[System.Serializable]
public class GatheredItems
{
    public Item item;
    public int amount;
}

public enum QuestType
{
    Gather,
    Kill,
    Craft
}