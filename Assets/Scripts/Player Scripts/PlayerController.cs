using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private const float speed = 7.5f;
    private const float jumpSpeed = 8.0f;
    private const float gravity = 20.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;

    [HideInInspector]
    bool canMove = true;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CheckPlayerMovement();
    }

    private void Movement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove)
        {
            moveDirection.y = jumpSpeed;
        }
    }

    private void CheckPlayerMovement()
    {
        if (characterController.isGrounded)
        {
            Movement();
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }
}