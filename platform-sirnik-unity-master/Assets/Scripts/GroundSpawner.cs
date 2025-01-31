using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPrefab; // ������ �����
    public GameObject[] cactusPrefabs; // ������ �������� ��������
    public Transform player; // ��������� ������
    public float spawnDistance = 10f;
    public float groundLength = 5f; 
    public float initialCactusSpawnChance = 0.02f; // ��������� ����������� ��������� �������
    public float increaseRate = 0.01f; // �������� ���������� �����������
    public float minGapBetweenCacti = 1f; // ����������� ���������� ����� ���������
    public float maxGapBetweenCacti = 3f; // ������������ ���������� ����� ���������
    private float lastGroundX;
    private int lastCactusIndex = -1;
    private float cactusSpawnChance;

    void Start()
    {
        lastGroundX = 0;
        cactusSpawnChance = initialCactusSpawnChance; // ��������� ��������� �����������
        SpawnGround();
    }

    void Update()
    {
        if (player.position.x > lastGroundX - spawnDistance)
        {
            SpawnGround();
            IncreaseSpawnChance(); // ���������� ����������� ����� �������� �����
        }
    }

    void SpawnGround()
    {
        // ��������� �����
        Vector3 newGroundPosition = new Vector3(lastGroundX + groundLength, 0, 0);
        Instantiate(groundPrefab, newGroundPosition, Quaternion.identity);
        lastGroundX += groundLength;

        // ��������� ��������
        if (Random.value < cactusSpawnChance)
        {
            // ��������� �������
            int cactusIndex;
            do
            {
                cactusIndex = Random.Range(0, cactusPrefabs.Length);
            } while (cactusIndex == lastCactusIndex); // �������: �� ��������� ������ ������

            lastCactusIndex = cactusIndex;

            float cactusX = newGroundPosition.x + Random.Range(minGapBetweenCacti, maxGapBetweenCacti);
            Instantiate(cactusPrefabs[cactusIndex], new Vector3(cactusX, 1.5f, 0), Quaternion.identity); // ������� Y ��� ������
        }
    }

    void IncreaseSpawnChance()
    {
        cactusSpawnChance += increaseRate; // ���������� �����������
  
        if (cactusSpawnChance > 1f)
        {
            cactusSpawnChance = 1f; // ������������ ����������� 100%
        }
    }
}
