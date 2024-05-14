using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // ���׹� ������
    public float spawnInterval = 0.4f; // ���� ����

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            // ���׹� ����
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            // ���׹̰� �±� "Bullet"�� �浹�� ���� �����ϴ� ��ũ��Ʈ �߰�
            EnemyCollisionHandler collisionHandler = enemy.AddComponent<EnemyCollisionHandler>();
            collisionHandler.Initialize(this);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}

public class EnemyCollisionHandler : MonoBehaviour
{
    private EnemySpawner spawner;

    public void Initialize(EnemySpawner enemySpawner)
    {
        spawner = enemySpawner;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // �浹�� ���� ������Ʈ�� �� ����
            Renderer renderer = collision.gameObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.red; // ���ϴ� �������� ���� ����
            }

            // �浹 Ƚ�� ī��Ʈ
            if (spawner != null)
            {
                spawner.OnEnemyCollision();
            }
        }
    }
}