using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valkyrie : PlayerController
{
    public float damageReduction = 0.75f; // 받는 피해 감소

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int damageFromEnemy = (int)(20 * damageReduction); // 피해량 감소 적용
            health -= damageFromEnemy;
            Debug.Log("Reduced damage taken! Remaining health: " + health);
        }
    }
}
