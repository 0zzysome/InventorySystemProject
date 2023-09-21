using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float airDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMult;
    bool readyToJump;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool isGrounded;

    

    public Transform orientation;
    float horizontalInput;
    float verticalInput;



    Vector3 direction;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();   
        rb.freezeRotation = true;
        
        ResetJump();
    }
    // Update is called once per frame
    void Update()
    {
        //checks if player is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        
        ReadInput();
        SpeedControll();
        ChangeDrag();

    }
    private void FixedUpdate()
    {
        movePlayer();
    }
    void ReadInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //jump handler 
        if (Input.GetButton("Jump") && readyToJump && isGrounded) 
        {
            readyToJump = false;

            Jump();
            //letts you keep jumping
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    void movePlayer() 
    {
        //makes it so you walk in the direction you are looking
        direction = orientation.forward * verticalInput + orientation.right * horizontalInput;


        if(isGrounded)
        {
            rb.AddForce(direction.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(direction.normalized * moveSpeed * 10f * airMult, ForceMode.Force);
        }
        
        
    }
    private void ChangeDrag()
    {
        if (isGrounded) 
        {
            rb.drag = groundDrag;
        }
        else if (!isGrounded)
        {
            rb.drag = airDrag;
        }
    }
    private void SpeedControll() 
    {
        Vector3 flatVel = new Vector3 (rb.velocity.x, 0f, rb.velocity.z);
        //is the player faster than the movement speed
        if (flatVel.magnitude > moveSpeed)
        {
            //creates the limited velocity, aka max speed
            Vector3 limit = flatVel.normalized * moveSpeed;
            //applies it to the rigidbody
            rb.velocity = new Vector3(limit.x, rb.velocity.y, limit.z);
        }
    }
    private void Jump()
    {
        // resets y velocity to make consistent jumps.
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
