using UnityEngine;

public class FruitMovement : MonoBehaviour
{
    public float speed = 100f;
    [SerializeField] private float destroyYLimit = -100f;
    [SerializeField] private float rotationSpeed = 100f; // Velocidade de rotação (editável no inspetor)

    void Update()
    {
        // Move para baixo no referencial global
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);

        // Roda o objeto no referencial local
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        // Destroi quando ultrapassa o limite definido
        if (transform.position.y < destroyYLimit)
        {
            Destroy(gameObject);
        }
    }
}
