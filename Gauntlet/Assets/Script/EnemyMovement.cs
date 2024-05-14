using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player; 
    public float speed = 5.0f;
    private Rigidbody enemyRigidbody;

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 positionDifference = player.position - transform.position;
        positionDifference = positionDifference.normalized;

        enemyRigidbody.MovePosition(transform.position + positionDifference * speed * Time.deltaTime);
    }
}