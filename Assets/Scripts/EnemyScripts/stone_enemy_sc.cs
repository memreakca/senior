using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stone_enemy_sc : MonoBehaviour
{
    public Animator animator;

    public float hp;
    public float damage;

    [SerializeField] private ItemObject lootItem;
    [SerializeField] private GroundItem lootPackage;
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) 
        {
            Die();
            Invoke("DestroyGameObject",2);
            SpawnLoot();
        }
    }

    public void Die()
    {
        Debug.Log("Animationtrgigerred");
        animator.SetTrigger("Die");        
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    public void SpawnLoot()
    {
        Vector3 lootPosition = transform.position + new Vector3(0, 1, 0);
        var obj = Instantiate(lootPackage.lootPackage,lootPosition, Quaternion.identity);
        lootPackage.item = lootItem;
    }
}
