using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class b_spider_enemy_sc : MonoBehaviour , IEnemy
{
    public int EnemyID { get; set; }
    public int Experience { get; set; }

    public Animator anim;

    [SerializeField] private ItemObject lootItem;
    [SerializeField] private GroundItem lootPackage;

    public float timeBetweenAttacks = 2f;
    public float attackCd;
    private SphereCollider attackHitbox;
    public Transform player;
    [SerializeField] public float damage = 30f;
    public float attackRange;
    public bool isAttacking;
    public bool isdead = false;
    public float hp;
    public int maxHp;
    private NavMeshAgent navMeshAgent;
    public EnemyTakeDamage enemytakendmg;
    public bool isMoving = true;
    private void Start()
    {
        float dmg = GetComponentInChildren<ColliderApplyDamage>().damageAmount = damage;
        attackCd = timeBetweenAttacks;
        Experience = 250;
        EnemyID = 3;
        hp = maxHp;
        enemytakendmg = GetComponent<EnemyTakeDamage>();
        enemytakendmg.maxHp = maxHp;
        enemytakendmg.currentHp = hp;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        lootItem = Player.main.inventory.database.Items[7];
        navMeshAgent = GetComponent<NavMeshAgent>();
        attackHitbox = GetComponentInChildren<SphereCollider>();
        
    }
    private void Update()
    {
        if (isdead) return;
        if (enemytakendmg.currentHp <= 0) Die();

        attackCd -= Time.deltaTime;

        MoveTowardsPlayer();
        CheckForAttack();

    }

    public void EnableHitbox()
    {
        attackHitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        attackHitbox.enabled = false;
    }
    public void Attack()
    {
        transform.LookAt(player);
        anim.SetBool("isMoving", false);
        isMoving = false;
        navMeshAgent.isStopped = true;

        Attack1();
       

    }

    private void Attack1()
    {
        isAttacking = true;
        anim.SetTrigger("Attack1");
    }
    private void Attack2()
    {
        transform.LookAt(player);
        anim.SetBool("isMoving", false);
        isMoving = false;
        navMeshAgent.isStopped = true;
        isAttacking = true;
        anim.SetTrigger("Attack2");
    }
    public void Die()
    {
        isdead = true;
        CombatEvents.EnemyDied(this);
        anim.SetTrigger("Die");
        int randomNumber = Random.Range(1, 101);
        if (randomNumber < 6) { SpawnLoot(); }
        SpawnerNest.Instance.enemiesAlive--;

    }

    public void RollForRangedAttack()
    {
        if (isdead || isAttacking) { return; }
        float randomvalue = Random.value;
        if (randomvalue < 0.1) 
        {
            Attack2();
        }
    }
    public void MoveTowardsPlayer()
    {
        if (isdead || isAttacking) { return; }

        navMeshAgent.SetDestination(player.position);
        anim.SetBool("isMoving", true);
    }
    void CheckForAttack()
    {
        if (isAttacking) { return; }
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {

            if (attackCd > 0) 
            { 
                anim.SetBool("isMoving", false);
                anim.SetBool("isAttacking", false); 
                return;
            }
            Attack();
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

    public void FinishAtttack()
    {
        navMeshAgent.isStopped = false;
        attackCd = timeBetweenAttacks;
        isMoving = true;
        isAttacking = false;
        anim.SetBool("isMoving", true);
        bool damageapplied = GetComponentInChildren<ColliderApplyDamage>().damageApplied = false;

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
