using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    const int idleState = 0;
    const int walkState = 1;
    const int runState = 2;
    const int Attack1State = 3;
    const int Attack2State = 4;
    const int Attack3State = 5;
    
    private Animator enemyAnimator;

    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }
    

    public void SetIdle()
    {
        // Set the enemy to idle state
        enemyAnimator.SetInteger("State", idleState);
    }

    public void SetWalk()
    {
        // Set the enemy to walk state
        enemyAnimator.SetInteger("State", walkState);
    }

    public void SetRun()
    {
        // Set the enemy to run state
        enemyAnimator.SetInteger("State", runState);
    }

    public void SetAttack()
    {
        int randomAttack = Random.Range(Attack1State, Attack3State + 1);
        enemyAnimator.SetInteger("State", randomAttack);
    }
}