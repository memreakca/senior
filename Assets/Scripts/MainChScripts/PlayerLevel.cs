using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    
    public float currentExp;
    public int currentLevel = 1;
    public int maxlevel = 20;
    public float neededLvlExp;
    

    
    private void Start()
    {
        currentLevel = 1;
        neededLvlExp = 200;
    }

    private int CalculateExperienceToLevelUp()
    {
        int baseExp = 200;
        float expScaleFactor = 1.3f;
        return (int)(baseExp * Mathf.Pow(expScaleFactor, currentLevel - 1));
    }
    public void GainExp(int expAmount)
    {
        if(currentLevel == maxlevel) { return; }
        currentExp += expAmount;
        if (currentExp >= neededLvlExp && currentLevel < maxlevel) { LevelUp(); }
    }

    public void LevelUp()
    {
        currentLevel++;
        
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
