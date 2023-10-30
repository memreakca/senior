using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private LayerMask targetmask;
    public float moveSpeed = 5.0f;
    private Rigidbody rb;

    private Vector3 moveDirection;
    public Transform orientation;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

  
    public void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 1f, ForceMode.Force);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,float.MaxValue,targetmask))
        {
            Vector3 cursorPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(cursorPosition);
        }
    }
}
