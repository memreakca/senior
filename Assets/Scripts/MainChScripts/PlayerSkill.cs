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
    public SkillFunctionality skillFunctionality;

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
        skill1.Text = skill1text;
        skill2.Text = skill2text;
        skill3.Text = skill3text;
        skill4.Text = skill4text;

        skillFunctionality= GetComponent<SkillFunctionality>();
        skillFunctionality.SetPlayerSkill(this);
    }
    private void Update()
    {
        unusedSkillpointTXT.text = $"Unused Skill Points = {unusedSkillPoints}";
        Skill1Button();
        Skill2Button();
        Skill3Button();
        Skill4Button();
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

    public void SetSkillFunctionality(PlayerSkill skill)
    {
        skillFunctionality.SetPlayerSkill(skill);
    }

    public void Skill1Button()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            skillFunctionality.UseSkill1();
        }
    }
    public void Skill2Button()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            skillFunctionality.UseSkill2();
        }
    }
    public void Skill3Button()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            skillFunctionality.UseSkill3();
        }
    }
    public void Skill4Button()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            skillFunctionality.UseSkill4();
        }
    }

}
[System.Serializable]
public class Skill
{
    public string name;
    public float maxCooldown;
    public float cooldown;
    public int level;
    public int maxLevel = 5;
    public TextMeshProUGUI Text;

    public void Upgrade()
    {
        if (level < maxLevel)
        {
            level++;
            maxCooldown--;
            Text.text = (level == maxLevel) ? $"Skill Level = MAX" : $"Skill Level = {level}";
        }
        else
        {
            Debug.Log("Max LEVEL");
        }
    }
    public void SetCooldown()
    {
        cooldown = maxCooldown;
    }
}