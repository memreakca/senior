using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;
public class stone_enemy_sc : MonoBehaviour
{
    public static stone_enemy_sc instance;
    public Animator animator;

    public float maxhp;
    public float hp;
    public float damage;

    public Image hpbar;
    private NavMeshAgent navMeshAgent;
    public bool isdead = false;

    [SerializeField] private ItemObject lootItem;
    [SerializeField] private GroundItem lootPackage;

    public UnityEvent OnEnemyDeath;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        hpbar = gameObject.GetComponentInChildren<Image>();
    }
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (isdead) return;
        if (Input.GetKeyDown(KeyCode.P))
        {
            hp -= 5;
            if (hp <= 0)
            {
                Die();
                return;
            }
            animator.SetTrigger("Hit");
            
        }
        hpbar.fillAmount = hp / maxhp;   
      
    }

    public void Die()
    {
        OnEnemyDeath.Invoke();
        Invoke("DestroyGameObject", 1.75f);
        isdead = true;
        navMeshAgent.speed = 0;
        animator.SetTrigger("Die");
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);
        Debug.Log("Animationtrgigerred");

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
