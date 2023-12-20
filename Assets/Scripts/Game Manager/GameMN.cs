using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMN : Singleton<GameMN>
{
    
    
    private int maxHealth;
    private int currentHealth;
    public int MaxHealth => maxHealth;
  

    
    
    private void Start()
    {
        GameInit();
    }

    private void GameInit()
    {
        CursorSetting();
        GameSetting();
    }
    
    private void InitializeHealth(GameObject entity)
    {
        if (entity.CompareTag("Player"))
        {
            maxHealth = 100;
        }
        else if (entity.CompareTag("Enemy"))
        {
            maxHealth = 50;
        }
        currentHealth = maxHealth;
    }

    public void CheckAndSetHealth(GameObject entity)
    {
        InitializeHealth(entity);
    }

    private void CursorSetting()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void GameSetting()
    {
        Application.targetFrameRate = 144;
    }
}