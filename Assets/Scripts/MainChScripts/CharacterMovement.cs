using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private LayerMask targetmask;
    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    public float moveSpeed = 5.0f;
    private Rigidbody rb;

    private float rotationSpeed =  13f ;
    private Animator animator;
    private bool isMoving;
    public Transform orientation;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

  
    public void Update()
    {
        CharacterMove();
       
    }
    public void CharacterMove()
    {
        Vector3 cameraForward = virtualCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        

        isMoving = Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f ;

        animator.SetBool("isMoving", isMoving);
        Vector3 targetDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
    public void LookAtMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue, targetmask))
        {
            Vector3 cursorPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(cursorPosition);
        }
    }
}
