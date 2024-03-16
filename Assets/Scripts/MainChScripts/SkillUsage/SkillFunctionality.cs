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

    public PlayerSwordDamage playerSwordDamage;
    BarScripts barScripts;

    public Animator animator;
    private void Start()
    {
        playerSwordDamage = PlayerAttackCombo.main.sword.GetComponent<PlayerSwordDamage>();

        animator = GetComponent<Animator>();
        barScripts = GetComponent<BarScripts>();
        playerSkill = GetComponent<PlayerSkill>();
        skill1 = playerSkill.skill1;
        skill2 = playerSkill.skill2;
        skill3 = playerSkill.skill3;
        skill4 = playerSkill.skill4;
    }
    private void Update()
    {
        ResetCooldowns();
        UpdateCooldownUI(skill1, barScripts.SetCooldownSkill1);
        UpdateCooldownUI(skill2, barScripts.SetCooldownSkill2);
        UpdateCooldownUI(skill3, barScripts.SetCooldownSkill3);
        UpdateCooldownUI(skill4, barScripts.SetCooldownSkill4);
    }

    private void UpdateCooldownUI(Skill skill, Action<Skill> updateUI)
    {
        updateUI(skill);
    }

    public void UseSkill1()
    {
        
        if (skill1.cooldown <= 0 && CharacterMovement.main.onMelee)
        {
            playerSwordDamage.damageAmount = skill1.damage;
            CharacterMovement.main.LookAtMouse();
            animator.SetTrigger("MeleeSkill1");
            PlayerAttackCombo.main.isHitting = true;
            PlayerAttackCombo.main.canMove = false;
        }
        else return;

        skill1.SetCooldown();
        barScripts.SetCooldownSkill1(skill1);
    }

    public void UseSkill2()
    {
        if (skill2.cooldown <= 0 && CharacterMovement.main.onMelee)
        {
            CharacterMovement.main.LookAtMouse();
            animator.SetTrigger("MeleeSkill2");
            PlayerAttackCombo.main.isHitting = true;
            PlayerAttackCombo.main.canMove = false;
        }
        else return;

        skill2.SetCooldown();
        barScripts.SetCooldownSkill2(skill2);
    }
    public void UseSkill3() 
    {
        if (skill3.cooldown <= 0 && CharacterMovement.main.onMelee)
        {
            
            int duration = skill3.level + 5;
            animator.SetTrigger("MeleeSkill3");
            Player.main.BoostStats(duration);
            PlayerAttackCombo.main.isHitting = true;
            PlayerAttackCombo.main.canMove = false;
            Player.main.UpdateValues();
        }
        else return;

        skill3.SetCooldown();
        barScripts.SetCooldownSkill3(skill3);
    }
    public void UseSkill4()
    {
        if (skill4.cooldown == 0) Debug.Log("Skill 4 used!"); else return;
        skill4.SetCooldown();
        barScripts.SetCooldownSkill4(skill4);
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
        }
       
    }
}
