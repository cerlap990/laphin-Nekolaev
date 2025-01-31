using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPrefab; // Префаб земли
    public GameObject[] cactusPrefabs; // Массив префабов кактусов
    public Transform player; // Трансформ игрока
    public float spawnDistance = 10f;
    public float groundLength = 5f; 
    public float initialCactusSpawnChance = 0.02f; // Начальная вероятность появления кактуса
    public float increaseRate = 0.01f; // Скорость увеличения вероятности
    public float minGapBetweenCacti = 1f; // Минимальное расстояние между кактусами
    public float maxGapBetweenCacti = 3f; // Максимальное расстояние между кактусами
    private float lastGroundX;
    private int lastCactusIndex = -1;
    private float cactusSpawnChance;

    void Start()
    {
        lastGroundX = 0;
        cactusSpawnChance = initialCactusSpawnChance; // Установка начальной вероятности
        SpawnGround();
    }

    void Update()
    {
        if (player.position.x > lastGroundX - spawnDistance)
        {
            SpawnGround();
            IncreaseSpawnChance(); // Увеличение вероятности после создания земли
        }
    }

    void SpawnGround()
    {
        // Генерация земли
        Vector3 newGroundPosition = new Vector3(lastGroundX + groundLength, 0, 0);
        Instantiate(groundPrefab, newGroundPosition, Quaternion.identity);
        lastGroundX += groundLength;

        // Генерация кактусов
        if (Random.value < cactusSpawnChance)
        {
            // Генерация кактуса
            int cactusIndex;
            do
            {
                cactusIndex = Random.Range(0, cactusPrefabs.Length);
            } while (cactusIndex == lastCactusIndex); // Условие: не повторять дважды подряд

            lastCactusIndex = cactusIndex;

            float cactusX = newGroundPosition.x + Random.Range(minGapBetweenCacti, maxGapBetweenCacti);
            Instantiate(cactusPrefabs[cactusIndex], new Vector3(cactusX, 1.5f, 0), Quaternion.identity); // Меняйте Y для высоты
        }
    }

    void IncreaseSpawnChance()
    {
        cactusSpawnChance += increaseRate; // Увеличение вероятности
  
        if (cactusSpawnChance > 1f)
        {
            cactusSpawnChance = 1f; // Максимальная вероятность 100%
        }
    }
}
