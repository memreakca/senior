using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class PlayerRangedAttack : MonoBehaviour
{
    [SerializeField] public Transform RangedAttackPoint;

    [SerializeField] public GameObject firePrefab;

    public CinemachineVirtualCamera virtualCamera;
    private PlayerAttackCombo playerAttack;
    private CharacterMovement charmov;
    private Animator animator;
    [SerializeField] private LayerMask layerMask;
    public Vector3 mousePosition;
    public string enemyTag = "Enemy";

    public bool isAttacking;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttackCombo>();
        charmov = GetComponent<CharacterMovement>();
    }
    private void Update()
    {
        if (charmov.onMelee || charmov.isEquipping || isAttacking) { return; }
        AttackRanged();
    }
    public void AttackRanged()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isAttacking)
            {
                mousePosition = Input.mousePosition;
                playerAttack.canMove = false;
                isAttacking = true;
                animator.SetTrigger("RangedAttack");
                charmov.LookAtMouse();
            }
            
        }
    }

    public void SpawnFireball()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue,layerMask))
        {
                
                Vector3 cursorPosition = new Vector3(hit.point.x, RangedAttackPoint.position.y, hit.point.z);
                GameObject fireball = Instantiate(firePrefab, RangedAttackPoint.position, Quaternion.identity);
                fireball.transform.LookAt(hit.transform);
                fireball.GetComponent<FireBall>().SetDirection((cursorPosition - transform.position).normalized);
            
        }
    }

    public void ResetRangedAttack()
    {
        isAttacking = false;
    }
}
