using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : Singleton<PlayerController>
{
    private Rigidbody rb;
    private Attack attackComponent;
    
    #region Move Stat
    private const float speed = 6f;
    private const float sprintMultiplier = 1.4f;
    private const float strafeMultiplier = 0.6f;
    private const float jumpForce = 40.0f;
    #endregion
    
    #region Move State
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
    private bool isAttack = false;
    private bool isDeath = false;
    
    public Vector3 move = Vector3.zero;
    
    public bool CanMove => canMove;
    public bool IsJump => isJump;
    public bool IsWalking => isWalking;
    public bool IsRunning => isRunning;
    public bool IsWalkingBackward => isWalkingBackward;
    public bool IsStrafeLeft => isStrafeLeft;
    public bool IsStrafeRight => isStrafeRight;
    public bool IsWalkingLeft => isWalkingLeft;
    public bool IsWalkingRight => isWalkingRight;
    public bool IsAttack => isAttack;
    public bool IsDeath => isDeath;
    #endregion
    
    
    
    private void Start()
    {
        Init();
    }
    
    private void Update()
    {
        GetPlayerState();
        CheckAttackWithMove();
    }
    private void Init()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        attackComponent = GetComponent<Attack>();
    }
    
    public void Die()
    {
        canMove = false;
        isDeath = true;
    }
    private void CheckForDeath()
    {
        
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
        if (!canMove) return;
        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");
        float currentSpeed = speed * (isWalkingBackward ? 0.5f : 1f) * (isRunning ? sprintMultiplier : 1f)
                                   * (isStrafeLeft || isStrafeRight ? strafeMultiplier : 1f) * (isWalkingLeft || isWalkingRight ? strafeMultiplier : 1f);

        Vector3 move = new Vector3(horInput, 0f, verInput);
        rb.MovePosition(rb.position + transform.TransformVector(move) * currentSpeed * Time.deltaTime);
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

    private void GetPlayerState()
    {
        isWalking = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
        isWalkingBackward = Input.GetAxis("Vertical") < 0;
        isRunning = isWalking && !isWalkingBackward && Input.GetKey(KeyCode.LeftShift);
        isStrafeLeft = isRunning && Input.GetAxis("Horizontal") < 0;
        isStrafeRight = isRunning && Input.GetAxis("Horizontal") > 0;
        isWalkingLeft = !isRunning && Input.GetAxis("Horizontal") < 0;
        isWalkingRight = !isRunning && Input.GetAxis("Horizontal") > 0;
    }
    private void Attack()
    {
        if (!isAttack && canMove)
        {
            StartCoroutine(AttackRoutine());
            AttackTarget();
        }
    }
    private void AttackTarget()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100))
        {
            Health healthComponent = hit.transform.GetComponent<Health>();
            if (healthComponent != null)
            {
                attackComponent.PerformAttack(healthComponent); // Perform the attack on the target's health component
            }
        }
    }
    private IEnumerator AttackRoutine()
    {
        if (IsIdle())
        {
            canMove = false;
            isAttack = true;
            PlayerAnimation.Instance.TriggerAttack();
            yield return new WaitForSeconds(0.5f);
            canMove = true;
            isAttack = false;
        }
    }
    private bool IsIdle()
    {
        return !isJump && !isWalking && !isRunning && !isWalkingBackward && !isStrafeLeft && !isStrafeRight && !isWalkingLeft && !isWalkingRight && isGrounded;
    }
}