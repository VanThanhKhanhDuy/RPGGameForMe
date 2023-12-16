using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    
    private Rigidbody rb;
    
    private const float speed = 7.5f;
    private const float jumpForce = 8.0f;
    
    
    private bool isGrounded = true;
    private bool canMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        CheckPlayerMovement();
    }

    private void Movement()
    {
        if (canMove)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            move = transform.TransformDirection(move);
            rb.MovePosition(rb.position + move * speed * Time.deltaTime);
        }
    }

    private void Jump()
    {
        if (canMove && isGrounded)
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            isGrounded = false;
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
        }
    }
}