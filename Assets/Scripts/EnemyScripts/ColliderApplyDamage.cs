using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderApplyDamage : MonoBehaviour
{
    private float damageAmount;
    private stone_enemy_sc enemysc;
    public bool damageApplied = false;
    private void Start()
    {
        enemysc = GetComponentInParent<stone_enemy_sc>();
        damageAmount = enemysc.damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!damageApplied && other.CompareTag("Player"))
        {
            Player.main.TakeDamage(damageAmount);
            damageApplied = true;
        }
    }

}
