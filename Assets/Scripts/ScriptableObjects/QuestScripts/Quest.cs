using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Quest : MonoBehaviour
{
    public List<Goal> Goals = new List<Goal>();
    public string QuestName;
    public string QuestDescription;
    public int ExperienceReward;
    public Item itemReward;
    public bool Completed;

    private void Update()
    {
        if (Completed) return;
    }
    public void CheckGoals()
    {
        Completed = Goals.All(g => g.Completed);
    }

    public void GiveReward()
    {
        if (itemReward != null)
        {
            Player.main.inventory.AddItem(itemReward, 1);
        }
        Player.main.playerLevel.GainExp(ExperienceReward);
    }

}
