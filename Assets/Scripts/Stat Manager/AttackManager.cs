using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private int playerDamage = 20;
    private int enemyDamage = 10;

    private void Start()
    {
        SetDamageForAll();
    }

    private void SetDamageForAll()
    {
        SetDamageForTag("Player", playerDamage);
        SetDamageForTag("Enemy", enemyDamage);
    }

    private void SetDamageForTag(string tag, int damage)
    {
        var objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (var obj in objects)
        {
            var attackComponent = obj.GetComponent<Attack>();
            if (attackComponent == null)
            {
                obj.AddComponent<Attack>().SetDamage(damage);
            }
        }
    }
}