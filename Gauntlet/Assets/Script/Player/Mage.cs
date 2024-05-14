using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : PlayerController
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MagicPotion"))
        {
            magicPotionCount += 2; // 다른 캐릭터는 +1이지만 마법사는 +2
            Destroy(other.gameObject); // 포션 객체는 회수
            Debug.Log("Magic potion collected! Total potions: " + magicPotionCount);
        }
    }
}
