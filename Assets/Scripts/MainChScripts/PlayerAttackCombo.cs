using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCombo : MonoBehaviour
{
    public static PlayerAttackCombo main;
    private CharacterMovement chmov;
    [SerializeField] public GameObject sword;
    private BoxCollider SwordHitbox;
    
    private Animator animator;

    public bool canMove;
    public bool isHitting;
    public float timeSinceAttack;
    public int currentAttack = 1;
    private void Awake()
    {
        main = this;
    }
    private void Start()
    {
        SwordHitbox = sword.GetComponent<BoxCollider>();
        canMove = true;
        isHitting = false;
        animator = GetComponent<Animator>();
        chmov = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        if (chmov.isEquipping || isHitting) { return; }
        timeSinceAttack += Time.deltaTime;

        Attack();

    }

    public void Attack()
    {
        if (!chmov.onMelee) { return; }
        if(Input.GetMouseButton(0) && timeSinceAttack > 0.4f)
        {
            chmov.LookAtMouse();
            Debug.Log(currentAttack.ToString());
            currentAttack++;
            canMove = false;
            isHitting = true;
            chmov.isMoving = false;
            animator.SetBool("isMoving", false);

            if (currentAttack > 3)
            {
                currentAttack = 1;
            }

            if (timeSinceAttack > 1.0f)
            {
                currentAttack = 1;
            }

            animator.SetTrigger("MeleeAttack" + currentAttack);

            timeSinceAttack = 0f;
        }

    }

    public void ActivateSwordHitbox()
    {
        SwordHitbox.enabled = true;
    }
    public void DeactivateSwordHitbox()
    {
        SwordHitbox.enabled = false;
    }


    public void ResetHit()
    {
        isHitting = false;
    }
    public void ResetMove()
    {
        canMove = true;
    }
}
