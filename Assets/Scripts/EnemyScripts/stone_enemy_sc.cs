using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stone_enemy_sc : MonoBehaviour
{
    public static stone_enemy_sc instance;
    public Animator animator;

    public float hp;
    public float damage;

    [SerializeField] private ItemObject lootItem;
    [SerializeField] private GroundItem lootPackage;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnLoot()
    {
        Vector3 lootPosition = transform.position + new Vector3(0, 1, 0);
        var obj = Instantiate(lootPackage.lootPackage,lootPosition, Quaternion.identity);
        obj.GetComponent<GroundItem>().item = lootItem;
    }
}
