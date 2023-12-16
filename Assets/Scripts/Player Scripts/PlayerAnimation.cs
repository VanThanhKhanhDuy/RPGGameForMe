using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : Singleton<PlayerAnimation>
{
    const int idleState = 0;
    const int walkState = 1;
    const int runState = 2;
    const int jumpState = 4; // Changed from 4 to 3 for consistency
    
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

        if (isJump)
        {
            playerAnimator.SetInteger("State", jumpState);
        }
        else if (isRunning)
        {
            playerAnimator.SetInteger("State", runState);
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