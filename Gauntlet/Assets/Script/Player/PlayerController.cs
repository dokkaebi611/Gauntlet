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
    public int keys = 0; // Number of keys player has

    private Vector2 move;
    private float nextFireTime = 0f;
    private float nextHealthDecreaseTime = 0f;

    // PlayerInput component reference
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.onActionTriggered += OnActionTriggered;
        health = maxHealth; // Initial health setting
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
        // Adjust movement to the x and z axes
        Vector3 movement = new Vector3(move.x, 0, move.y);

        if (movement != Vector3.zero) // Only rotate if there is movement
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 1.5f * Time.deltaTime);
        }

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
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
        if (other.CompareTag("Food"))
        {
            Food food = other.GetComponent<Food>();
            if (food != null)
            {
                health += food.healthRecoveryAmount;
                health = Mathf.Clamp(health, 0, maxHealth);
                Destroy(other.gameObject);
            }
        }
        if (other.CompareTag("MagicPotion"))
        {
            // Handle magic potion interaction
        }
        if (other.CompareTag("Treasure"))
        {
            // Handle treasure interaction
        }
        if (other.CompareTag("Key"))
        {
            keys++;
            Destroy(other.gameObject);
            Debug.Log("Key collected! Total keys: " + keys);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            if (keys > 0)
            {
                keys--;
                Destroy(collision.gameObject);
                Debug.Log("Door opened! Remaining keys: " + keys);
            }
            else
            {
                Debug.Log("You need a key to open this door.");
                // Additional feedback or blocking movement can be added here
            }
        }
    }
}
