using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy 
{
    int EnemyID { get; set; }
    int Experience { get; set; }
    void Die();
    void TakeDamage(float damage);
    void Attack();
}
