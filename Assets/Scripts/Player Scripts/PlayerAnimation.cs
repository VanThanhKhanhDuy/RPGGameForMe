using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : Singleton<PlayerAnimation>
{
    private Animator playerAnimator;
    private string currentState;
    
    private int lastAttackIndex = 0;
    #region Move State
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
    #endregion


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
        var playerController = PlayerController.Instance;
        bool isDeath = playerController.IsDeath;

        if (isDeath)
        {
            ChangeAnimationState(DEATH);
            return;
        }

        var animState = IDLE;
        if (playerController.IsJump) animState = JUMP;
        else if (playerController.IsWalkingBackward) animState = WALKBACKWARD;
        else if (playerController.IsRunning) animState = playerController.IsStrafeLeft ? SPRINTLEFT : (playerController.IsStrafeRight ? SPRINTRIGHT : SPRINT);
        else if (playerController.IsWalking) animState = playerController.IsWalkingLeft ? WALKLEFT : (playerController.IsWalkingRight ? WALKRIGHT : WALK);

        ChangeAnimationState(animState);
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