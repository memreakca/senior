using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PlayerSkill : MonoBehaviour
{
    public static PlayerSkill instance;
    public GameObject SkillUI;

    public TextMeshProUGUI unusedSkillpointTXT;

    public TextMeshProUGUI skill1text;
    public TextMeshProUGUI skill2text;
    public TextMeshProUGUI skill3text;
    public TextMeshProUGUI skill4text;

    public Skill skill1;
    public Skill skill2;
    public Skill skill3;
    public Skill skill4;

    public int unusedSkillPoints = 1;

    private void Start()
    {
        unusedSkillPoints = 1;
        skill1.text = skill1text;
        skill2.text = skill2text;
        skill3.text = skill3text;
        skill4.text = skill4text;
    }
    private void Update()
    {
        unusedSkillpointTXT.text = $"Unused Skill Points = {unusedSkillPoints}";
    }
    private void Awake()
    {
        instance = this;
    }
    public void UpgradeSkill1()
    {
        if (skill1.level < skill1.maxLevel && unusedSkillPoints >= 1)
        {
            unusedSkillPoints--;
            skill1.Upgrade();
        }
        else
        {
            Debug.Log("Cannot upgrade Skill 1. Max level reached or not enough skill points.");
        }

    }
    public void UpgradeSkill2() 
    {
        if (skill2.level < skill2.maxLevel && unusedSkillPoints >= 1)
        {
            unusedSkillPoints--;
            skill2.Upgrade();
        }
        else
        {
            Debug.Log("Cannot upgrade Skill 2. Max level reached or not enough skill points.");
        }

    }
    public void UpgradeSkill3() 
    {
        if (skill3.level < skill3.maxLevel && unusedSkillPoints >= 1)
        {
            unusedSkillPoints--;
            skill3.Upgrade();
        }
        else
        {
            Debug.Log("Cannot upgrade Skill 3. Max level reached or not enough skill points.");
        }
    }
    public void UpgradeSkill4() 
    {
        if (skill4.level < skill4.maxLevel && unusedSkillPoints >= 1)
        {
            unusedSkillPoints--;
            skill4.Upgrade();
        }
        else
        {
            Debug.Log("Cannot upgrade Skill 4. Max level reached or not enough skill points.");
        }
    }

  
}
[System.Serializable]
public class Skill
{
    public string name;
    public int cooldown;
    public int level;
    public int maxLevel = 5;
    public TextMeshProUGUI text;

    public void Upgrade()
    {
        if (level < maxLevel)
        {
            level++;
            if (level == maxLevel)
            {
                text.text = $"Skill Level = MAX";
            }
            else
            {
                text.text = $"Skill Level = {level}";
            }
        }
        else
        {
            Debug.Log("Max LEVEL");
        }
    }
}