using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCombo : MonoBehaviour
{
    private CharacterMovement chmov;

    private Animator animator;
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public static int numberOfClick = 0;
    float lastClickTime = 0;
    float maxComboDelay = 1;

    private void Start()
    {
        animator = GetComponent<Animator>();
        chmov = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("combo1"))
        {
            animator.SetBool("combo1", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("combo2"))
        {
            animator.SetBool("combo2", false);
            numberOfClick = 0;
        }

        if(Time.time - lastClickTime < maxComboDelay)
        {
            numberOfClick = 0;
        }
        if(Time.time > nextFireTime)
        {
            if(Input.GetMouseButtonDown(0)) { AttackClick(); }
        }
    }

    public void AttackClick()
    {
        Debug.Log(numberOfClick.ToString());
        lastClickTime = Time.time;
        numberOfClick++;
        if (numberOfClick == 1) 
        {
            animator.SetBool("combo1", true);
        }
        numberOfClick = Mathf.Clamp(numberOfClick, 0, 2);

        if (numberOfClick >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("combo1"))
        {
            
            animator.SetBool("combo1", false);
            animator.SetBool("combo2", true);
        }
    }
}
