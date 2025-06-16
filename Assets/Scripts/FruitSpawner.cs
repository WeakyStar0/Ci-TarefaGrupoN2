using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public static FruitSpawner Instance;  // Instância única

    public GameObject[] fruits; // Prefabs das frutas
    public float spawnInterval = 0.7f; // Tempo entre frutas
    public float minX = 0f; // Ajusta para a largura do Canvas
    public float maxX = 1080f;
    public float spawnY = 0f; // Posição Y acima do ecrã (ajusta conforme o Canvas)

    void Awake()
    {
        // Se já existe uma instância, destrói esta (para não duplicar)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Define esta instância como a única
        Instance = this;

        // Não destrói este objeto ao carregar cenas
        DontDestroyOnLoad(transform.root.gameObject);
    }

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
