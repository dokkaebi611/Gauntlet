using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : PlayerController
{

    private Inventory inventory;
    private void Awake()
    {
        health = maxHealth; // Initial health setting

        // Initialize inventory
        inventory = GetComponent<Inventory>();
        if (inventory == null)
        {
            inventory = gameObject.AddComponent<Inventory>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MagicPotion"))
        {
            magicPotionCount += 2;
            Debug.Log("Magic potion collected! Total potions: " + magicPotionCount);
            Destroy(other.gameObject);
        }
        else
        {
            ItemHolder itemHolder = other.GetComponent<ItemHolder>();
            if (itemHolder != null)
            {
                if (itemHolder.item is Food food)
                {
                    health += food.healthRecoveryAmount;
                    health = Mathf.Clamp(health, 0, maxHealth);
                    Debug.Log("Food collected! Health restored. Current health: " + health);
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
    }
}
