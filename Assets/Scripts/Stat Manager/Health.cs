using UnityEngine;

public class Health : MonoBehaviour
{
    
    [SerializeField] private  int currentHealth;
    public static int maxHealth = 100;

    private void Awake()
    {
        currentHealth = maxHealth; // Initialize health to the max at start.
    }
private void Update() {
    Debug.Log(currentHealth);
}
    public void ApplyDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Optional: Trigger some damage taken response, like a sound or animation.
        }
    }

    public void SetHealth(int health)
    {
        currentHealth = Mathf.Clamp(health, 0, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (gameObject.CompareTag("Player"))
        {
            var playerController = PlayerController.Instance;
            playerController.Die();
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            EnemyController.isDead = true;
        }
    }
}