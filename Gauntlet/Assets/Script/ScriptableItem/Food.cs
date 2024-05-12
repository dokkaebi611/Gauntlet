using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "NewFood", menuName = "Inventory/Food")]
public class Food : Item
{
    public int healthRecoveryAmount;
}