using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTakeDamage : MonoBehaviour
{
    public static EnemyTakeDamage Instance;
    public float maxHp;
    public float currentHp;
    public Animator anim;
    public Image hpbar;

    private void Start()
    {
        anim = GetComponent<Animator>();
        hpbar = gameObject.GetComponentInChildren<Image>();
    }
    private void Update()
    {
        if (hpbar != null) 
        {
            hpbar.fillAmount = currentHp / maxHp;
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(20);
        }
    }
    private void Awake()
    {
        Instance = this;
    }
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
    }

}
