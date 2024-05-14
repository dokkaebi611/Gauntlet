using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : PlayerController
{
    public int extraDamage = 10;
    public int enemiesThreshold = 5;
    public bool isDamageBoosted = false;

    protected override void Update()
    {
        base.Update();
        CheckEnemiesAndBoostDamage();
    }

    void CheckEnemiesAndBoostDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5.0f);
        int enemyCount = 0;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
                enemyCount++;
        }

        if (enemyCount >= enemiesThreshold)
        {
            if (!isDamageBoosted)
            {
                Debug.Log("Increased damage mode activated!");
                isDamageBoosted = true;
            }
        }
        else if (isDamageBoosted)
        {
            Debug.Log("Normal damage mode restored.");
            isDamageBoosted = false;
        }
    }
}