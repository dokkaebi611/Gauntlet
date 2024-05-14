using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public int health = 30;
    private bool isDestroyed = false;
    public GameObject enemyPrefab;
    public float spawnInterval = 1f;

    private Renderer[] renderers;
    private Color originalColor;
    private int lastHealthCheckpoint;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        originalColor = renderers[0].material.color;
        lastHealthCheckpoint = health;
        StartCoroutine(SpawnEnemyRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (!isDestroyed)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0 && !isDestroyed)
        {
            Destroy(gameObject);
            isDestroyed = true;
        }
        else if (health / 10 < lastHealthCheckpoint / 10) 
        {
            lastHealthCheckpoint = health;
            ChangeRandomColor();
        }
    }

    private void ChangeRandomColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        SetColor(randomColor);
    }

    private void SetColor(Color color)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = color;
        }
    }
}
