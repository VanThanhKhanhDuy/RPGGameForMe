using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public const int maxHealth = 10;
    private static int currentHealth;
    public static int CurrentHealth{
        get { return currentHealth; } set { currentHealth = value; }
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            PlayerController.Instance.Die();
        }
    }
}
