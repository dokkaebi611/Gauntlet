using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public int playerSpeed;
    public int playerHealth;
    public int playerScore;
    public int keys;
    public int potions;
    public int joysticknum;
    public bool keyboardOption;

    Vector2 movementInput;
    bool actionInput;

    void Update()
    {
        PlayerMove();
        if (playerHealth <= 0)
        {
            Destroy(gameObject);
        }
        Attack();
    }



    void PlayerMove()
    {
        Vector3 moveDirection = new Vector3(movementInput.x, 0, movementInput.y);
        transform.position += moveDirection * Time.deltaTime * playerSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        
    }

    public void Attack()
    {
        // if player use the spacebar player will fire the bullet prefab
        // bullet will go straight forward
        // if bullet collide with enemy kill the destory enemy object
        // if bullet collide with enemy spawner 3 times, then bullet will destory the enemy spawner
        // if bullet collide with wall, it will destroy itself
    }
}
