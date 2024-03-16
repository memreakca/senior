using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordDamage : MonoBehaviour
{
    public float damageAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyTakeDamage>().TakeDamage(damageAmount);
        }
    }
}
