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
    
    private Animator playerAnimator;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        CheckPlayerState();
    }

    private void CheckPlayerState()
    {
        bool isJump = PlayerController.Instance.IsJump;
        bool isWalking = PlayerController.Instance.IsWalking;
        bool isRunning = PlayerController.Instance.IsRunning;
        bool isWalkingBackward = PlayerController.Instance.IsWalkingBackward;
        bool isStrafeLeft = PlayerController.Instance.IsStrafeLeft;
        bool isStrafeRight = PlayerController.Instance.IsStrafeRight;
        
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
            playerAnimator.SetInteger("State", walkState);
        }
        else
        {
            playerAnimator.SetInteger("State", idleState);
        }
    }
}