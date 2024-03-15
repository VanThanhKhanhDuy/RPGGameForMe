using UnityEngine;

public class Attack : MonoBehaviour
{
    private int damageAmount;

    public void SetDamage(int damage)
    {
        damageAmount = damage;
    }

    public void PerformAttack(Health target)
    {
        target.ApplyDamage(damageAmount);
    }
}