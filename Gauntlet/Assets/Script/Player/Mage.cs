using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : PlayerController
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MagicPotion"))
        {
            magicPotionCount += 2; // �ٸ� ĳ���ʹ� +1������ ������� +2
            Destroy(other.gameObject); // ���� ��ü�� ȸ��
            Debug.Log("Magic potion collected! Total potions: " + magicPotionCount);
        }
    }
}
