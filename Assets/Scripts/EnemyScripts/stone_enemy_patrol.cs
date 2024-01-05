using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class stone_enemy_patrol : MonoBehaviour
{
    public float patrolRadius = 5f;
    public float patrolInterval = 3f;  // Time interval to choose a new random destination
    public float attackRange = 1f;
    public float sightRange = 10f;

    [SerializeField] private Transform patrolPoint;
    private NavMeshAgent navMeshAgent;
    private float timer;
    private Transform player;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on the enemy GameObject.");
        }

        timer = patrolInterval;
        SetRandomPatrolDestination(); // Set initial destination
    }

    void Update()
    {
        if (navMeshAgent != null)
        {
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
        navMeshAgent.SetDestination(player.position);
        animator.SetBool("isRunning", true);
        animator.SetBool("isAttacking", false);
    }
    bool CanSeePlayer()
    {
        return Vector3.Distance(transform.position, player.position) <= sightRange;
    }
    void Patrol()
    {
        if (navMeshAgent.remainingDistance < 1f)
        {
            timer -= Time.deltaTime;
            animator.SetBool("isIdle", true);
            animator.SetBool("isRunning", false);
            // Choose a new random destination if the timer reaches zero
            if (timer <= 0f)
            {
                SetRandomPatrolDestination();
                timer = patrolInterval;
            }
        }
        animator.SetBool("isRunning", true);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isIdle", false);
    }

    void SetRandomPatrolDestination()
    {
        // Set a new random destination within the patrol radius
        Vector2 randomPoint = UnityEngine.Random.insideUnitCircle * patrolRadius;
        Vector3 destination = patrolPoint.position + new Vector3(randomPoint.x, 0f, randomPoint.y);

        // Set the destination for the NavMeshAgent
        navMeshAgent.SetDestination(destination);
    }

    void CheckForAttack()
    {
        // Check if the player is within attack range
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            AttackPlayer();
        }
        else
        {
            // Trigger the "Idle" animation when not attacking
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isIdle", true);
        }
    }
    void AttackPlayer()
    {
        // Implement your attack logic here
        Debug.Log("Enemy attacking player!");
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", true);
        animator.SetBool("isIdle", true);
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
