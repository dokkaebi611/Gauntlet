using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : PlayerController
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MagicPotion"))
        {
            magicPotionCount += 2;
            Destroy(other.gameObject);
            Debug.Log("Magic potion collected! Total potions: " + magicPotionCount);
        }
    }
}
