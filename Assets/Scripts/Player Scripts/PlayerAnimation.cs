using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : Singleton<PlayerAnimation>
{
    private Animator playerAnimator;
    private string currentState;
    
    private int lastAttackIndex = 0;
    
    private const string IDLE = "Idle";
    private const string WALK = "Walk";
    private const string SPRINT = "Sprint";
    private const string JUMP = "Jump";
    private const string WALKBACKWARD = "WalkBackward";
    private const string SPRINTLEFT = "SprintLeft";
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
        while (attackIndex == lastAttackIndex)
        {
            attackIndex = Random.Range(1, 4);
        }
        lastAttackIndex = attackIndex;

        string attackAnimationName = "Attack" + attackIndex;
        playerAnimator.CrossFade(attackAnimationName, 0.3f);
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
            ChangeAnimationState(DEATH);
            return;
        }
        
        if (isJump)
        {
            ChangeAnimationState(JUMP);
        }
        else if (isWalkingBackward)
        {
            ChangeAnimationState(WALKBACKWARD);
        }
        else if (isRunning)
        {
            if (isStrafeLeft)
            {
                ChangeAnimationState(SPRINTLEFT);
            }
            else if (isStrafeRight)
            {
                ChangeAnimationState(SPRINTRIGHT);
            }
            else
            {
                ChangeAnimationState(SPRINT);
            }
        }
        else if (isWalking)
        {
            if (isWalkingLeft)
            {
                ChangeAnimationState(WALKLEFT);
            }
            else if (isWalkingRight)
            {
                ChangeAnimationState(WALKRIGHT);
            }
            else
            {
                ChangeAnimationState(WALK);
            }
        }
        else
        {
            ChangeAnimationState(IDLE);
        }
    }

    private void ChangeAnimationState(string newState, float transitionDuration = 0.1f)
    {
        if (currentState == newState) return;
        try
        {
            playerAnimator.CrossFade(newState, transitionDuration);
            currentState = newState;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to change animation state. Error: " + e.Message);
        }
    }
}