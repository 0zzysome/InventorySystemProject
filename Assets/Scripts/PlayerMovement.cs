using Unity.VisualScripting;
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

    [Header("Throwing")]
    
    public static float throwStrengthMult = 10f;
    public float throwCooldown;
    float nextUppdate = 0f;
    
    
    Vector3 direction;
    Rigidbody rb;
    EquipmentManager equipmentManager;
    private void Start()
    {
        equipmentManager = EquipmentManager.Instance;
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
        
        UseEquiped();
        StayOnHand();
    }
    private void FixedUpdate()
    {
        movePlayer();
        
    }
    private void LateUpdate()
    {
        
        AltUse();
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
    public void AltUse()
    {
        //makes shure hand is not emty
        if (equipmentManager.currentEquipment[0] != null)
        {

            if (Input.GetMouseButtonDown(1))
            {
               //makes shure you are not in inventory/other menu
                if (Cursor.visible == false)
                {
                    if (Time.time > nextUppdate)
                    {
                        //Debug.Log("throwing  " + equipmentManager.currentEquipment[0].name);
                        equipmentManager.currentEquipment[0].AlternativeUse();
                        
                        nextUppdate = Time.time + throwCooldown;
                    }

                }
            }
        }
    }

    public void UseEquiped()
    {
        if (equipmentManager.currentEquipment[0] != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                equipmentManager.currentEquipment[0].Use();
            }
        }
    }
    public void StayOnHand()
    {
        
        if (equipmentManager.IsHoldingItem)
        {
            if (equipmentManager.currentEquipment[0] != null)
            {
                
                    equipmentManager.currentEquipment[0].objectRef.transform.position = equipmentManager.handPosition.position;

                    equipmentManager.currentEquipment[0].objectRef.transform.rotation = equipmentManager.handPosition.rotation;
                
                
            }

        }
    }
}
