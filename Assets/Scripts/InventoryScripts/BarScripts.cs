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

}
