using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFunctionality : MonoBehaviour
{   
    PlayerSkill playerSkill;

    Skill skill1;
    Skill skill2;
    Skill skill3;
    Skill skill4;

    private void Start()
    {
        playerSkill = GetComponent<PlayerSkill>();
        skill1 = playerSkill.skill1;
        skill2 = playerSkill.skill2;
        skill3 = playerSkill.skill3;
        skill4 = playerSkill.skill4;
    }
    private void Update()
    {
        ResetCooldowns();
    }


    public void UseSkill1()
    {
        if (skill1.cooldown == 0) Debug.Log("Skill 1 used!"); else return;
        skill1.SetCooldown();
    }

    public void UseSkill2()
    {
        if (skill2.cooldown == 0) Debug.Log("Skill 2 used!"); else return;
        skill2.SetCooldown();
    }
    public void UseSkill3() 
    { 
        if (skill3.cooldown == 0) Debug.Log("Skill 3 used!"); else return;
        skill3.SetCooldown();
    }
    public void UseSkill4()
    {
        if (skill4.cooldown == 0) Debug.Log("Skill 4 used!"); else return;
        skill4.SetCooldown();
    }

    public void SetPlayerSkill(PlayerSkill _playerSkill)
    {
        playerSkill = _playerSkill;
    }
    private void ResetCooldowns()
    {
        ResetCooldown(skill1);
        ResetCooldown(skill2);
        ResetCooldown(skill3);
        ResetCooldown(skill4);
    }

    public void ResetCooldown(Skill skill)
    {
        if (skill.cooldown > 0)
        {
            skill.cooldown -= Time.deltaTime;
            skill.cooldown = Mathf.Max(0, skill.cooldown);
            Debug.Log($"Cooldown for {skill.name} reset. New cooldown: {skill.cooldown}");
        }
       
    }
}
