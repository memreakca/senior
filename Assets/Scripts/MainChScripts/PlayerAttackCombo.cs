using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerAttackCombo : MonoBehaviour
{
    public static PlayerAttackCombo main;
    private CharacterMovement chmov;
    [SerializeField] public GameObject sword;
    private BoxCollider SwordHitbox;
    public PlayerSwordDamage playerSwordDamage;

    private Animator animator;

    [SerializeField] public float hit1Damage;
    [SerializeField] public float hit2Damage;
    [SerializeField] public float hit3Damage;

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
        playerSwordDamage = sword.GetComponent<PlayerSwordDamage>();
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
            setComboDamage();
            Debug.Log(currentAttack.ToString() + " DMG" + playerSwordDamage.damageAmount.ToString());

            timeSinceAttack = 0f;
        }

    }

    public void setComboDamage()
    {
        if(currentAttack == 1)
        {
            playerSwordDamage.damageAmount = hit1Damage;
        }
        else if (currentAttack == 2)
        {
            playerSwordDamage.damageAmount = hit2Damage;
        }
        else if (currentAttack == 3)
        {
            playerSwordDamage.damageAmount = hit3Damage;
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
