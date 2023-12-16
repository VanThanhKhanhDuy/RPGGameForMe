using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : Singleton<PlayerController>
{
    
    private Rigidbody rb;
    
    private const float speed = 7.5f;
    private const float sprintMultiplier = 1.4f;
    private const float jumpForce = 8.0f;
    
    
    private bool isGrounded = true;
    private bool canMove = true;
    private bool isJump = false;
    private bool isWalking = false;
    private bool isRunning = false;
    public Vector3 move = Vector3.zero;
    
    public bool IsJump => isJump;
    public bool IsWalking => isWalking;
    public bool IsRunning => isRunning;
    
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        GetPlayerState();
        CheckPlayerMovement();
    }

    private void Movement()
    {
        if (canMove)
        {
            float currentSpeed = speed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed *= sprintMultiplier;
            }
            move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            move = transform.TransformDirection(move);
            rb.MovePosition(rb.position + move * currentSpeed  * Time.deltaTime);
            Debug.Log(currentSpeed);
        }

        
    }

    private void Jump()
    {
        if (canMove && isGrounded)
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            isGrounded = false;
            isJump = true;
        }
    }

    private void CheckPlayerMovement()
    {
        Movement();

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJump = false;
        }
    }

    public void GetPlayerState()
    {
        isWalking = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
        isRunning = isWalking && Input.GetKey(KeyCode.LeftShift);
        if (isRunning)
        {
            isWalking = false;
        }
    }
}