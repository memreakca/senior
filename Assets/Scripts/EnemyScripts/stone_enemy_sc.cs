using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;
public class stone_enemy_sc : MonoBehaviour , IEnemy
{
    public int EnemyID { get; set; }
    public int Experience { get; set; }
    public static stone_enemy_sc instance;
    public Animator animator;

    public float maxhp;
    public float hp;
    public float damage;
    public EnemyTakeDamage enemytakendmg;
    private NavMeshAgent navMeshAgent;
    public bool isdead = false;

    [SerializeField] private ItemObject lootItem;
    [SerializeField] private GroundItem lootPackage;

    public UnityEvent OnEnemyDeath;

    private void Start()
    {
        enemytakendmg = GetComponent<EnemyTakeDamage>();
        enemytakendmg.maxHp = maxhp;
        enemytakendmg.currentHp = hp;

        navMeshAgent = GetComponent<NavMeshAgent>();
        EnemyID = 1;
        Experience = 200;
    }
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (isdead) return;
        if (enemytakendmg.currentHp <= 0) Die();
    }

    public void Attack()
    {

    }
    public void Die()
    {
        CombatEvents.EnemyDied(this);
        OnEnemyDeath.Invoke();
        Invoke("DestroyGameObject", 1.75f);
        isdead = true;
        navMeshAgent.speed = 0;
        animator.SetTrigger("Die");
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);

        SpawnLoot();
    }
    void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    public void SpawnLoot()
    {
        Vector3 lootPosition = transform.position + new Vector3(0, 1, 0);
        var obj = Instantiate(lootPackage.lootPackage,lootPosition, Quaternion.identity);
        obj.GetComponent<GroundItem>().item = lootItem;
        obj.GetComponent<GroundItem>().lootamount = 3;
    }
}
