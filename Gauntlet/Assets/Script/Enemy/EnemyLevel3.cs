using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel3 : Enemy
{
    public GameObject bulletPrefab;  // Assign this in the Unity inspector
    public Transform bulletSpawnPoint;  // Typically a child of the enemy GameObject
    public float shootingInterval = 2f;
    private float shootingTimer;

    void Start()
    {
        health *= 3;
        shootingTimer = shootingInterval;
    }

    void Update()
    {
        base.MoveTowardsPlayer();
        HandleShooting();
    }

    void HandleShooting()
    {
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0)
        {
            Shoot();
            shootingTimer = shootingInterval;  // Reset timer
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            // Instantiate the bullet
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            // Calculate the direction to the player
            Vector3 directionToPlayer = (player.position - bulletSpawnPoint.position).normalized;

            // Apply force to the bullet towards the player
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = directionToPlayer * 10f;  // Set bullet speed as needed
            }
        }
        else
        {
            Debug.LogError("Bullet prefab or spawn point not assigned!");
        }
    }
}
