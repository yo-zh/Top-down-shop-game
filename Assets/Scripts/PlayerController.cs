using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] float speed = 2.5f;
    private float horizontalInput;
    private float forwardInput;
    private Vector3 moveDirection;

    [SerializeField] GameObject player;
    private Rigidbody rb;

    [Header("MousePosition")]
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    public Vector3 lookDirection;
    Plane plane = new Plane(Vector3.down, 1.5f);

    [Header("Jump")]
    [SerializeField] bool isGrounded;
    private LayerMask groundLayer;
    [SerializeField] float groundDrag;
    [SerializeField] bool canJump = true;
    [SerializeField] float jumpCooldown;
    [SerializeField] float jumpForce;
    [SerializeField] float airMultiplier;
    private Vector3 lastPosition;
    private float airTime;

    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        groundLayer = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        PlayerInput();
        SpeedLimit();

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f + 0.1f, groundLayer);

        if (isGrounded)
        {
            rb.drag = groundDrag;
            airTime = 0;
        }
        else
        {
            rb.drag = 0;
            airTime += Time.deltaTime;
        }

        if (! isGrounded && airTime > 5) {
            Debug.Log("Looks like you're stuck, moving back");
            rb.MovePosition(lastPosition);
        }

        //World pisition of a cursor for rotation
        screenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (plane.Raycast(ray, out float distance))
        {
            worldPosition = ray.GetPoint(distance);
            worldPosition.y = transform.position.y;
            lookDirection = worldPosition - transform.position;
            rb.MoveRotation(Quaternion.LookRotation(lookDirection, Vector3.up));
        }                    
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        forwardInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space) && isGrounded && canJump)
        {
            lastPosition = rb.position;
            canJump = false;
            Jump();
            Invoke(nameof(AllowJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = new Vector3(horizontalInput, 0.0f, forwardInput);

        if (isGrounded)
        {
            rb.AddRelativeForce(moveDirection.normalized * speed, ForceMode.Force);
        }
        else if (!isGrounded)
        {
            rb.AddRelativeForce(moveDirection.normalized * speed * airMultiplier, ForceMode.Force);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void AllowJump()
    {
        canJump = true;
    }

    private void SpeedLimit()
    {
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
        
        if (horizontalVelocity.magnitude > speed)
        {
            Vector3 limitedVelocity = horizontalVelocity.normalized * speed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }
}
