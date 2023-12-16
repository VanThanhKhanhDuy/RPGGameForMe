using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameMN : Singleton<GameMN>
{
    
    private void Start()
    {
        GameInit();
    }
    private void GameInit()
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
