using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderApplyDamage : MonoBehaviour
{
    public float damageAmount;
    public bool damageApplied = false;
  
    private void OnTriggerEnter(Collider other)
    {
        if (!damageApplied && other.CompareTag("Player"))
        {
            Player.main.TakeDamage(damageAmount);
            damageApplied = true;
        }
    }
}
