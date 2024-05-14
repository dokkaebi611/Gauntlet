using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 에네미 프리팹
    public float spawnInterval = 0.4f; // 스폰 간격

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            // 에네미 생성
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            // 에네미가 태그 "Bullet"과 충돌할 때를 감지하는 스크립트 추가
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
            // 충돌한 게임 오브젝트의 색 변경
            Renderer renderer = collision.gameObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.red; // 원하는 색상으로 변경 가능
            }

            // 충돌 횟수 카운트
            if (spawner != null)
            {
                spawner.OnEnemyCollision();
            }
        }
    }
}