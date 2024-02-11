using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class StoneSlayer : Quest
{
    private void Start()
    {
        Debug.Log("StoneKiller Assigned.");
        QuestName = "Stone Killer";
        QuestDescription = "Kill 1 Stone Golem";
        itemReward = Player.main.inventory.database.Items[2].data;
        ExperienceReward = 100;
        Goals = new List<Goal>
        {
            new KillGoal(this,1, QuestDescription, false, 0, 1)
        };

        Goals.ForEach(g => g.Init());
    }
}
