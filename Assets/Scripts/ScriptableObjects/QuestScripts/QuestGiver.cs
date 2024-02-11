using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour ,IInteractable
{
    public bool AssignedQuest;
    public bool Helped;
    [SerializeField] private GameObject quests;
    [SerializeField] private string questType;

    public Quest Quest;
    public void Interact()
    {
        if (!AssignedQuest && !Helped)
        {
            AssignQuest();
        }
        else if(AssignedQuest && !Helped)
        {
            CheckQuest();
        }
    }

    public void AssignQuest()
    {

        AssignedQuest = true;
        Quest = (Quest)quests.AddComponent(System.Type.GetType(questType));
        Debug.Log("Quest Assigned");
    }

    public void CheckQuest()
    {
        if (Quest.Completed) 
        {
            Quest.GiveReward();
            Helped = true;
            AssignedQuest  = false;
            Debug.Log("Thanks for help");
        }
        else
        {
            Debug.Log("It is not completed yet.");
        }
    }
}
