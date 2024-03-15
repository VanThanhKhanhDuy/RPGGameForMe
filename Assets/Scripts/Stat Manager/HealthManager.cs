using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : Singleton<HealthManager>
{
    private void Start()
    {
        SetHealthForAll();
    }
    private void SetHealthForAll()
    {
        SetHealthForTag("Player", 50);
        SetHealthForTag("Enemy", 40);
    }

    private void SetHealthForTag(string tag, int health)
    {
        var objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (var obj in objects)
        {
            var healthComponent = obj.GetComponent<Health>() ?? obj.AddComponent<Health>();
            healthComponent.SetHealth(health);
        }
    }
}
