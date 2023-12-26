using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarScripts : MonoBehaviour
{
    [SerializeField] public Slider healthSlider;
    public TextMeshProUGUI healthText;
    [SerializeField] public Slider manaSlider;
    public TextMeshProUGUI manaText;

    [SerializeField] public Image skill1; [SerializeField] public TextMeshProUGUI skill1cdtxt;
    [SerializeField] public Image skill2; [SerializeField] public TextMeshProUGUI skill2cdtxt;
    [SerializeField] public Image skill3; [SerializeField] public TextMeshProUGUI skill3cdtxt;
    [SerializeField] public Image skill4; [SerializeField] public TextMeshProUGUI skill4cdtxt;

    public void SetHealth(float hp, float maxHp)
    {
        healthSlider.value = hp / maxHp;
        healthText.text = $"{hp:F0}/{maxHp:F0}";
    }

    public void SetMana(float sp, float maxSp) 
    { 
        manaSlider.value = sp / maxSp;
        manaText.text = $"{sp:F0}/{maxSp:F0}";
    }

    public void SetCooldownSkill1(Skill _skill)
    {
        skill1.fillAmount = _skill.cooldown / _skill.maxCooldown;
        skill1cdtxt.text = (_skill.cooldown == 0) ? $" " : $"{_skill.cooldown:F1} ";
    }
    public void SetCooldownSkill2(Skill _skill)
    {
        skill2.fillAmount = _skill.cooldown / _skill.maxCooldown;
        skill2cdtxt.text = (_skill.cooldown == 0) ? $" " : $"{_skill.cooldown:F1} ";
    }
    public void SetCooldownSkill3(Skill _skill)
    {
        skill3.fillAmount = _skill.cooldown / _skill.maxCooldown;
        skill3cdtxt.text = (_skill.cooldown == 0) ? $" " : $"{_skill.cooldown:F1} ";
    }

    public void SetCooldownSkill4(Skill _skill)
    {
        skill4.fillAmount = _skill.cooldown / _skill.maxCooldown;
        skill4cdtxt.text = (_skill.cooldown == 0) ? " " : $"{_skill.cooldown:F1} ";
    }

}
