using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderApplyDamage : MonoBehaviour
{
    private float damageAmount;
    private stone_enemy_sc enemysc;
    private bool damageApplied = false;
    [SerializeField] private Player player;
    private void Start()
    {
        player = GetComponent<Player>();
        enemysc = GetComponentInParent<stone_enemy_sc>();
        damageAmount = enemysc.damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called");
        if (!damageApplied && other.CompareTag("Player"))
        {
            
            Debug.Log("Collision with player detected!");

            other.SendMessage("TakeDamage", damageAmount, SendMessageOptions.DontRequireReceiver);
            damageApplied = true;  // Set the flag to true to indicate that damage has been applied
            Debug.Log("Damage applied!");
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        
        if (damageApplied && other.CompareTag("Player"))
        {
            damageApplied = false;
        }
    }
}
