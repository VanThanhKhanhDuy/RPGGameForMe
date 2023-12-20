using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : Singleton<PlayerAnimation>
{
    private Animator playerAnimator;
    private string currentState;
    private float transitionDuration = 0.1f;
    
    private const string IDLE = "Idle";
    private const string WALK = "Walk";
    private const string RUN = "Run";
    private const string JUMP = "Jump";
    private const string WALKBACKWARD = "WalkBackward";
    private const string SPRINTLeFT = "SprintLeft";
    private const string SPRINTRIGHT = "SprintRight";
    private const string WALKLEFT = "WalkLeft";
    private const string WALKRIGHT = "WalkRight";
    private const string DEATH = "Death";
 
    

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
        int attackIndex = Random.Range(1, 4);
        string attackAnimationName = "Attack" + attackIndex;
        playerAnimator.CrossFade(attackAnimationName, transitionDuration);
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
        bool isDeath = PlayerController.Instance.IsDeath;

        if (isDeath)
        {
            ChangeAnimationState("Death");
            return;
        }
        
        if (isJump)
        {
            ChangeAnimationState("Jump");
        }
        else if (isWalkingBackward)
        {
            ChangeAnimationState("WalkBackward");
        }
        else if (isRunning)
        {
            if (isStrafeLeft)
            {
                ChangeAnimationState("SprintLeft");
            }
            else if (isStrafeRight)
            {
                ChangeAnimationState("SprintRight");
            }
            else
            {
                ChangeAnimationState("Sprint");
            }
        }
        else if (isWalking)
        {
            if (isWalkingLeft)
            {
                ChangeAnimationState("WalkLeft");
            }
            else if (isWalkingRight)
            {
                ChangeAnimationState("WalkRight");
            }
            else
            {
                ChangeAnimationState("Walk");
            }
        }
        else
        {
            ChangeAnimationState("Idle");
        }
    }

    private void ChangeAnimationState(string newState, float transitionDuration = 0.1f)
    {
        if (currentState == newState) return;

        playerAnimator.CrossFade(newState, transitionDuration);
        currentState = newState;
    }
}