using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject[] obstaclePrefabs; // 장애물 프리팹
    [SerializeField] private GameObject[] enemyPrefabs;    // 적 프리팹
    [SerializeField] private GameObject[] itemPrefabs;     // 아이템 프리팹

    [SerializeField] private Transform[] spawnPoints;      // 스폰 위치 목록
    [SerializeField] private float initialSpawnInterval = 2f; // 초기에 스폰하는 간격
    [SerializeField] private float minSpawnInterval = 0.5f;    // 가장 빠른 스폰 간격
    [SerializeField] private float spawnIntervalDecreaseRate = 0.01f; // 스폰 간격 감소 속도

    private float currentSpawnInterval;
    private float timer;

    private void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        timer = 0f;
    }

    private void Update()
    {
        if(GameManager.Instance.IsGameOver) return;

        timer += Time.deltaTime;
        if(timer >= currentSpawnInterval)
        {
            SpawnRandom();
            timer = 0f;

            // 점차 스폰 간격 감소
            if(currentSpawnInterval > minSpawnInterval)
            {
                currentSpawnInterval -= spawnIntervalDecreaseRate;
            }
        }
    }

    /// <summary>
    /// 장애물, 적, 아이템 중 하나를 무작위로 선택하여 스폰
    /// </summary>
    private void SpawnRandom()
    {
        // 무엇을 스폰할지 랜덤 결정 (장애물, 적, 아이템 등)
        int spawnType = Random.Range(0, 3); // 0=장애물, 1=적, 2=아이템

        GameObject prefabToSpawn = null;
        switch(spawnType)
        {
            case 0: // 장애물
                if(obstaclePrefabs.Length > 0)
                    prefabToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
                break;
            case 1: // 적
                if(enemyPrefabs.Length > 0)
                    prefabToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                break;
            case 2: // 아이템
                if(itemPrefabs.Length > 0)
                    prefabToSpawn = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
                break;
        }

        // 실제 스폰
        if(prefabToSpawn != null && spawnPoints.Length > 0)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
