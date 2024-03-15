using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAnimation : MonoBehaviour
{
    private Attack attackComponent;
    private Animator enemyAnimator;
    private string currentState;
    private float transitionDuration = 0.1f;
    private bool isAttacking = false;

    private const string IDLE = "Idle";
    private const string WALK = "Walk";
    private const string RUN = "Run";
    private const string DIE = "Die";

    private void Start()
    {
        attackComponent = GetComponent<Attack>();
        if (attackComponent != null)
        {
            attackComponent.SetDamage(10); // Set the attack damage for the enemy
        }
        enemyAnimator = GetComponent<Animator>();
    }

    public void SetIdle()
    {
        ChangeAnimationState(IDLE);
    }

    public void SetWalk()
    {
        ChangeAnimationState(WALK);
    }

    public void SetRun()
    {
        ChangeAnimationState(RUN);
    }

    public void SetDie()
    {
        ChangeAnimationState(DIE);
    }

    public void SetAttack()
    {
        if (isAttacking) return; // Prevent new attack if already attacking

        isAttacking = true; // Set the attacking flag
        int attackIndex = Random.Range(1, 4);
        string attackAnimationName = "Attack" + attackIndex;
        enemyAnimator.CrossFade(attackAnimationName, transitionDuration);
        // You could use a coroutine to reset the flag after the animation duration
        StartCoroutine(ResetAttackFlag(attackAnimationName));
        AttackTarget();

    }
    private IEnumerator ResetAttackFlag(string animationName)
    {
        // Wait for the current attack animation to finish
        yield return new WaitForSeconds(enemyAnimator.GetCurrentAnimatorStateInfo(0).length);
        isAttacking = false; // Reset the attacking flag
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

    private void ChangeAnimationState(string newState, float transitionDuration = 0.1f)
    {
        if (currentState == newState) return;

        enemyAnimator.CrossFade(newState, transitionDuration);
        currentState = newState;
    }
}