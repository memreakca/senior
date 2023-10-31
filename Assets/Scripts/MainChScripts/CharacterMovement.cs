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

    private bool isMoving;
    private Vector3 moveDirection;
    public Transform orientation;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

  
    public void Update()
    {
        CharacterMove();

        
        if (isMoving) { LookAtMouse(); }
        

       
    }

    public void CheckIsMoving()
    {
        
    }
    public void CharacterMove()
    {
        Vector3 cameraForward = virtualCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        isMoving = Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f || Input.GetMouseButtonDown(0) ;

        Vector3 movement = cameraForward * verticalInput + virtualCamera.transform.right * horizontalInput;
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
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
