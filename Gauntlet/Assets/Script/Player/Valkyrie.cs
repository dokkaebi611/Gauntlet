using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valkyrie : PlayerController
{
    public static GameManager Instance { get; private set; }

    public float damageReduction = 0.75f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int damageFromEnemy = (int)(20 * damageReduction);
            GameManager.Instance.playerHealth -= damageFromEnemy;
            Debug.Log("Reduced damage taken! Remaining health: " + GameManager.Instance.playerHealth);
        }
    }
}
