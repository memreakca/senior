using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class spider_enemy_sc : MonoBehaviour, IEnemy
{
    public int EnemyID { get; set; }
    public int Experience { get; set; }

    public Animator anim;

    [SerializeField] private ItemObject lootItem;
    [SerializeField] private GroundItem lootPackage;
    public Transform player;

    public float currentHp;
    public int maxHp;
    private NavMeshAgent navMeshAgent;
    private void Start()
    {
        Experience = 200;
        EnemyID = 2;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        lootItem = Player.main.inventory.database.Items[7];
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        navMeshAgent.SetDestination(player.position);

        if(Input.GetKeyDown(KeyCode.M)) { TakeDamage(5); }
    }
  
    public void Attack()
    {
        anim.SetTrigger("Attack");
    }

    public void Die()
    {
        CombatEvents.EnemyDied(this);
        anim.SetTrigger("Die");
        int randomNumber = Random.Range(1, 101);
        if (randomNumber < 6) { SpawnLoot(); }

    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            navMeshAgent.speed = 0;
            Die();
            return;
        }
    }

    public void SpawnLoot()
    {
        Vector3 lootPosition = transform.position + new Vector3(0, 1, 0);
        var obj = Instantiate(lootPackage.lootPackage, lootPosition, Quaternion.identity);
        obj.GetComponent<GroundItem>().item = lootItem;
        obj.GetComponent<GroundItem>().lootamount = 1;
    }

    public void DestoryGameObject()
    {
        Destroy(gameObject);
    }
}
