using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 20f;
    public float fireDelay = 0.1f;
    public int maxHealth = 2000;
    public int health;
    public int healthDecreaseRate = 1;
    public int healthDecreaseInterval = 1; // 1 second
    public int score = 0; // Player's score
    public int magicPotionCount = 0;

    private Vector2 move;
    private float nextFireTime = 0f;
    private float nextHealthDecreaseTime = 0f;

    // PlayerInput component reference
    private PlayerInput playerInput;

    // Reference to Inventory
    private Inventory inventory;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.onActionTriggered += OnActionTriggered;
        health = maxHealth; // Initial health setting

        // Initialize inventory
        inventory = GetComponent<Inventory>();
        if (inventory == null)
        {
            inventory = gameObject.AddComponent<Inventory>();
        }
    }

    private void OnDestroy()
    {
        playerInput.onActionTriggered -= OnActionTriggered;
    }

    private void OnActionTriggered(InputAction.CallbackContext context)
    {
        if (context.action.name == "Move")
        {
            OnMove(context);
        }
        else if (context.action.name == "Attack")
        {
            OnFire(context);
        }
        else if (context.action.name == "UseMagicPotion")
        {
            OnUseMagicPotion(context);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed && Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + fireDelay;
        }
    }

    public void OnUseMagicPotion(InputAction.CallbackContext context)
    {
        if (context.performed && magicPotionCount > 0)
        {
            UseMagicPotion();
            magicPotionCount--;
            Debug.Log("Magic Potion used. Remaining potions: " + magicPotionCount);
        }
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Common initialization code
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        movePlayer();
        DecreaseHealthOverTime();
    }

    protected virtual void movePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0, move.y);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        if (move != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(move.x, move.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = bulletSpawn.forward * bulletSpeed;
        Destroy(bullet, 2f);
    }

    private void DecreaseHealthOverTime()
    {
        if (Time.time >= nextHealthDecreaseTime)
        {
            health -= healthDecreaseRate;
            nextHealthDecreaseTime = Time.time + healthDecreaseInterval;

            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Debug.Log("Player has died");
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemHolder itemHolder = other.GetComponent<ItemHolder>();
        if (itemHolder != null)
        {
            inventory.AddItem(itemHolder.item);

            if (itemHolder.item is Food food)
            {
                health += food.healthRecoveryAmount;
                health = Mathf.Clamp(health, 0, maxHealth);
            }
            else if (itemHolder.item is Treasure treasure)
            {
                score += treasure.scoreValue;
                Debug.Log("Treasure collected! Score: " + score);
            }
            else if (itemHolder.item is MagicPotion)
            {
                magicPotionCount++;
                Debug.Log("Magic potion collected! Total potions: " + magicPotionCount);
            }

            Destroy(other.gameObject);
        }
    }

    private void UseMagicPotion()
    {
        if (magicPotionCount > 0)
        {
            // Define the range dimensions
            float rangeX = 40f;
            float rangeY = 2f;
            float rangeZ = 40f;

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            Vector3 playerPosition = transform.position;

            foreach (GameObject enemy in enemies)
            {
                Vector3 enemyPosition = enemy.transform.position;
                float distanceX = Mathf.Abs(enemyPosition.x - playerPosition.x);
                float distanceY = Mathf.Abs(enemyPosition.y - playerPosition.y);
                float distanceZ = Mathf.Abs(enemyPosition.z - playerPosition.z);

                // Check if the enemy is within the specified range
                if (distanceX <= rangeX && distanceY <= rangeY && distanceZ <= rangeZ)
                {
                    Destroy(enemy);
                }
            }

            Debug.Log("Magic Potion used. Destroyed enemies within range: " + rangeX + "x" + rangeY + "x" + rangeZ);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            if (inventory.HasKey())
            {
                inventory.UseKey();
                Destroy(collision.gameObject);
                Debug.Log("Door opened!");
            }
            else
            {
                Debug.Log("You need a key to open this door.");
            }
        }

        // Check if the collision object has an Enemy tag
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Define damage from the enemy
            int damageFromEnemy = 20;

            health -= damageFromEnemy;
            Debug.Log("Player hit by enemy! Remaining health: " + health);

            if (health <= 0)
            {
                Die();
                Debug.Log("Player has died.");
            }
        }
    }
}