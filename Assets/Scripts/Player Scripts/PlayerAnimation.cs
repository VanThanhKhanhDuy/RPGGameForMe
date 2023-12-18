using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : Singleton<PlayerAnimation>
{
    const int idleState = 0;
    const int walkState = 1;
    const int runState = 2;
    const int walkBackwardState = 3;
    const int jumpState = 4;
    const int runStrafeLeftState = 5;
    const int runStrafeRightState = 6;
    const int walkLeftState = 7;
    const int walkRightState = 8;
    const int Attack1State = 9;
    const int Attack2State = 10;
    const int Attack3State = 11;
    
    private Animator playerAnimator;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        CheckPlayerState();
    }
    public void TriggerAttack()
    {
        playerAnimator.SetInteger("State", idleState); // Set animation to idle
        StartCoroutine(AttackAnimationRoutine());
    }
    private IEnumerator AttackAnimationRoutine()
    {
        yield return null; // Wait one frame to ensure the idle state transition if needed

        // Randomly select an attack animation
        int randomAttack = Random.Range(Attack1State, Attack3State + 1);
        playerAnimator.SetInteger("State", randomAttack);

        // Optionally wait for the animation to finish before returning to idle
        // You may want to replace this with the actual duration of the longest attack animation
        yield return new WaitForSeconds(1f);
        playerAnimator.SetInteger("State", idleState);
    }

    private void CheckPlayerState()
    {
        bool isJump = PlayerController.Instance.IsJump;
        bool isWalking = PlayerController.Instance.IsWalking;
        bool isRunning = PlayerController.Instance.IsRunning;
        bool isWalkingBackward = PlayerController.Instance.IsWalkingBackward;
        bool isStrafeLeft = PlayerController.Instance.IsStrafeLeft;
        bool isStrafeRight = PlayerController.Instance.IsStrafeRight;
        bool isWalkingLeft = PlayerController.Instance.IsWalkingLeft;
        bool isWalkingRight = PlayerController.Instance.IsWalkingRight;
        
        if (isJump)
        {
            playerAnimator.SetInteger("State", jumpState);
        }
        else if (isWalkingBackward)
        {
            playerAnimator.SetInteger("State", walkBackwardState);
        }
        else if (isRunning)
        {
            if (isStrafeLeft)
            {
                playerAnimator.SetInteger("State", runStrafeLeftState);
            }
            else if (isStrafeRight)
            {
                playerAnimator.SetInteger("State", runStrafeRightState);
            }
            else
            {
                playerAnimator.SetInteger("State", runState);
            }
        }
         else if (isWalking)
        {
            if (isWalkingLeft)
            {
                playerAnimator.SetInteger("State", walkLeftState);
            }
            else if (isWalkingRight)
            {
                playerAnimator.SetInteger("State", walkRightState);
            }
            else if (isStrafeLeft)
            {
                playerAnimator.SetInteger("State", runStrafeLeftState);
            }
            else if (isStrafeRight)
            {
                playerAnimator.SetInteger("State", runStrafeRightState);
            }
            else
            {
                playerAnimator.SetInteger("State", walkState);
            }
        }
        else
        {
            playerAnimator.SetInteger("State", idleState);
        }
    }
}