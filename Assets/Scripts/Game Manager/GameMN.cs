using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameMN : Singleton<GameMN>
{
    
    void Start()
    {
        
    }
    
    void Update()
    {
        Init();
    }

    private void Init()
    {
        CursorSetting();
        GameSetting();
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
