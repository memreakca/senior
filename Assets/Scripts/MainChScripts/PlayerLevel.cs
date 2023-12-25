using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public PlayerSkill playerSkill;
    public float currentExp;
    public int currentLevel = 1;
    public int maxlevel = 20;
    public float neededLvlExp;
    

    
    private void Start()
    {
        playerSkill = GetComponent<PlayerSkill>();
        currentLevel = 1;
        neededLvlExp = 200;
    }
    public void GainExp(int expAmount)
    {
        if(currentLevel == maxlevel) { return; }
        currentExp += expAmount;
        if (currentExp >= neededLvlExp ) { LevelUp(); }
    }
    private int CalculateExperienceToLevelUp()
    {
        int baseExp = 200;
        float expScaleFactor = 1.3f;
        return (int)(baseExp * Mathf.Pow(expScaleFactor, currentLevel - 1));
    }
  

    public void LevelUp()
    {
        currentLevel++;
        playerSkill.unusedSkillPoints++;
        neededLvlExp = CalculateExperienceToLevelUp();
        currentExp = 0;
        Player.main.UpdateBaseStats();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GainExp(5000);
        }
    }
}
