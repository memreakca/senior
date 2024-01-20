using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;

public class stone_enemy_patrol : MonoBehaviour
{
    public float patrolRadius = 5f;
    public float patrolInterval = 3f;  // Time interval to choose a new random destination
    public float attackRange = 1f;
    public float sightRange = 10f;
    public float attackDuration = 2.5f;
    public float timeBetweenAttacks = 1f;

    public stone_enemy_sc instance;
    [SerializeField] public Transform patrolPoint;
    private NavMeshAgent navMeshAgent;
    private float timer;
    private Transform player;
    private Animator animator;
    private bool isWaiting;
    private bool isAttacking;
    private SphereCollider handhitbox;

    


    void Start()
    {
        handhitbox = GetComponentsInChildren<SphereCollider>()[0];
        handhitbox.enabled = false;
        instance = gameObject.GetComponent<stone_enemy_sc>(); 
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on the enemy GameObject.");
        }

        timer = patrolInterval;
        SetRandomPatrolDestination(); // Set initial destination
        isWaiting = false;
        isAttacking = false;
}
   
    void Update()
    {
        
        timeBetweenAttacks -= Time.deltaTime;

        if (navMeshAgent != null)
        {
            if (isAttacking) return;
            if (timeBetweenAttacks > 0) return;

            if (CanSeePlayer())
            {
                MoveTowardsPlayer();
                CheckForAttack();
            }
            else
            {
                Patrol();
            }
        }
    }
    void MoveTowardsPlayer()
    {
        if (CanSeePlayer())
        {
            navMeshAgent.SetDestination(player.position);

            animator.SetBool("isRunning", true);
            animator.SetBool("isAttacking", false);

            navMeshAgent.isStopped = false;

            isAttacking = false;
        }
        else
        {

            navMeshAgent.isStopped = true;

            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", false);
        }
    }
    bool CanSeePlayer()
    {
        return Vector3.Distance(transform.position, player.position) <= sightRange;
    }
    void Patrol()
    {
        if (isWaiting)
        {
            // Continue waiting for the timer
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                isWaiting = false;
                SetRandomPatrolDestination();
            }
        }
        else if (navMeshAgent.remainingDistance < 0.4f)
        {
            // Start waiting for the timer when the enemy reaches its destination
            isWaiting = true;
            timer = patrolInterval;
        }

        // Trigger the "Run" animation while patrolling
        animator.SetBool("isRunning", !isWaiting && !CanSeePlayer());
        animator.SetBool("isAttacking", false);

        if (!isAttacking)
        {
            navMeshAgent.isStopped = false;
        }
    }

    public void Die()
    {
        Debug.Log("Animationtrgigerred");
        navMeshAgent.speed = 0;
        animator.SetTrigger("Die");
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);

        Invoke("DestroyGameObject", 1.75f);
        instance.SpawnLoot();
    }


  
    void SetRandomPatrolDestination()
    {
        NavMeshHit hit;
        Vector3 randomPoint = transform.position;

        for (int i = 0; i < 30; i++) 
        {
            Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * patrolRadius;
            randomPoint = patrolPoint.position + new Vector3(randomOffset.x, 0f, randomOffset.y);

            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                break; 
            }
        }

        // Set the destination for the NavMeshAgent
        navMeshAgent.SetDestination(randomPoint);
    }

    void CheckForAttack()
    {
        
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            AttackPlayer();
        }
        else
        {
            // Trigger the "Idle" animation when not attacking
            animator.SetBool("isRunning", true);
            animator.SetBool("isAttacking", false);
        }
        
        
    }
    void AttackPlayer()
    {
        // Implement your attack logic here

        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", true);
        activateHandHitboxes();
        navMeshAgent.isStopped = true;

        if (!isAttacking)
        {
            isAttacking = true;

            Invoke("FinishAttack", attackDuration);
        
        }
    }

    void activateHandHitboxes()
    {
        handhitbox.enabled= true;
    }
    void deactivateHandHitboxes()
    {
        handhitbox.enabled = false;
        
    }
    void FinishAttack()
    {
        // This function is called when the attack animation duration is complete
        isAttacking= false;
        timeBetweenAttacks = 3;
        // Resume NavMeshAgent movement
        deactivateHandHitboxes();
        navMeshAgent.isStopped = false;
        
        Debug.Log("invoked");

        // You can add additional logic here if needed
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(patrolPoint.position, patrolRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
