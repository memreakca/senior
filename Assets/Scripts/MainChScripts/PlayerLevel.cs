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
    public float holdExp;

    
    private void Start()
    {
        CombatEvents.OnEnemyDeathC += EnemyToExperience;
        playerSkill = GetComponent<PlayerSkill>();
        currentLevel = 1;
        neededLvlExp = 200;
    }

    public void EnemyToExperience(IEnemy enemy)
    {
        GainExp(enemy.Experience);
    }
    public void GainExp(float expAmount)
    {
        if(currentLevel == maxlevel) { neededLvlExp = 0; return; }

        float deltaExp = neededLvlExp - currentExp;

        if (expAmount > deltaExp)
        {
            holdExp = expAmount - deltaExp;
            expAmount = deltaExp;
            Debug.Log("HoldExp: " + holdExp);
        }
        else holdExp = 0;

        
        currentExp += expAmount;
        

        if (currentExp >= neededLvlExp)
        {
            LevelUp();
            GainExp(holdExp);
            holdExp = 0;
        }

    }
    public void LevelUp()
    {
        currentLevel++;
        currentExp = 0;
        playerSkill.unusedSkillPoints++;
        neededLvlExp = CalculateExperienceToLevelUp();
        Player.main.UpdateBaseStats();
    }
    private int CalculateExperienceToLevelUp()
    {
        int baseExp = 200;
        float expScaleFactor = 1.3f;
        return (int)(baseExp * Mathf.Pow(expScaleFactor, currentLevel - 1));
    }
  

   

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            GainExp(500);
        }
    }
}
