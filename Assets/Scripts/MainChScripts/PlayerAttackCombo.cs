using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCombo : MonoBehaviour
{
    private CharacterMovement chmov;

    private Animator animator;
 
    public bool isHitting;
    public float timeSinceAttack;
    public int currentAttack = 1;
    private void Start()
    {
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
            Debug.Log(currentAttack.ToString());
            currentAttack++;
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
    public void ResetHit()
    {
        isHitting = false;
    }
}
