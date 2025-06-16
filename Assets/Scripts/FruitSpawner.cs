using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject[] fruits; // Prefabs das frutas
    public float spawnInterval = 0.7f; // Tempo entre frutas
    public float minX = 0f; // Ajusta para a largura do Canvas
    public float maxX = 1080f;
    public float spawnY = 0f; // Posição Y acima do ecrã (ajusta conforme o Canvas)

    void Start()
    {
        StartCoroutine(SpawnFruits());
    }

    IEnumerator SpawnFruits()
    {
        while (true)
        {
            SpawnFruit();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnFruit()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), spawnY, 0);
        GameObject newFruit = Instantiate(fruits[Random.Range(0, fruits.Length)], spawnPosition, Quaternion.identity, transform);
        newFruit.AddComponent<FruitMovement>(); // Adiciona o movimento à fruta
    }
}
