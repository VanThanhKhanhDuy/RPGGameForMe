using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator enemyAnimator;

    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    public void SetIdle()
    {
        // Set the enemy to idle state
        enemyAnimator.SetInteger("State", 0);
    }

    public void SetWalk()
    {
        // Set the enemy to walk state
        enemyAnimator.SetInteger("State", 1);
    }

    public void SetRun()
    {
        // Set the enemy to run state
        enemyAnimator.SetInteger("State", 2);
    }

    public void SetAttack()
    {
        enemyAnimator.SetInteger("State", 3);
    }
}