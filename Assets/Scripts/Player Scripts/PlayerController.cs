using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : Singleton<PlayerController>
{
    
    private Rigidbody rb;
    
    private const float speed = 7.5f;
    private const float sprintMultiplier = 1.4f;
    private const float strafeMultiplier = 0.6f;
    private const float jumpForce = 8.0f;
    
    
    private bool isGrounded = true;
    private bool canMove = true;
    private bool isJump = false;
    private bool isWalking = false;
    private bool isRunning = false;
    private bool isWalkingBackward = false;
    private bool isStrafeLeft = false;
    private bool isStrafeRight = false;
    private bool isWalkingLeft = false;
    private bool isWalkingRight = false;
    
    public Vector3 move = Vector3.zero;
    
    public bool IsJump => isJump;
    public bool IsWalking => isWalking;
    public bool IsRunning => isRunning;
    public bool IsWalkingBackward => isWalkingBackward;
    public bool IsStrafeLeft => isStrafeLeft;
    public bool IsStrafeRight => isStrafeRight;
    public bool IsWalkingLeft => isWalkingLeft;
    public bool IsWalkingRight => isWalkingRight;
    
    

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void Update()
    {
        CheckAttackWithMove();
    }

    private void CheckAttackWithMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        if (canMove)
        {
            GetPlayerState();
            CheckPlayerMovement();
        }
    }

    private void Movement()
    {
        if (canMove)
        {
            float currentSpeed = speed;
            if (isWalkingBackward)
            {
                currentSpeed *= 0.5f;
            }
            if (isRunning)
            {
                currentSpeed *= sprintMultiplier;
            }
            if (isStrafeLeft || isStrafeRight)
            {
                currentSpeed *= strafeMultiplier;
            }
            if (isWalkingLeft || isWalkingRight)
            {
                currentSpeed *= strafeMultiplier; // Adjust the speed for walking left or right
            }

            move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            move = transform.TransformDirection(move);
            rb.MovePosition(rb.position + move * currentSpeed * Time.deltaTime);
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
        isWalkingBackward = Input.GetAxis("Vertical") < 0;
        isRunning = isWalking && !isWalkingBackward && Input.GetKey(KeyCode.LeftShift);
        isStrafeLeft = isRunning && Input.GetAxis("Horizontal") < 0;
        isStrafeRight = isRunning && Input.GetAxis("Horizontal") > 0;
        isWalkingLeft = !isRunning && Input.GetAxis("Horizontal") < 0;
        isWalkingRight = !isRunning && Input.GetAxis("Horizontal") > 0;
    }
    public void Attack()
    {
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        if (IsIdle())
        {
            canMove = false; // Disable movement
            PlayerAnimation.Instance.TriggerAttack(); // Trigger the attack animation
            yield return new WaitForSeconds(1f); // Wait for the attack animation to finish
            canMove = true; // Re-enable movement
        }
    }
    private bool IsIdle()
    {
        // Check if the player is not performing any action
        return !isJump && !isWalking && !isRunning && !isWalkingBackward && !isStrafeLeft && !isStrafeRight && !isWalkingLeft && !isWalkingRight;
    }
}