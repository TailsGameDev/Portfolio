using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform leftLimit = null;
    [SerializeField]
    private Transform rightLimit = null;
    [SerializeField]
    private Transform bottomLimit = null;

    [SerializeField]
    private float minTimeToSpawn = 0.0f;
    [SerializeField]
    private float maxTimeToSpawn = 0.0f;

    [SerializeField]
    private Fruit[] fruitPrefabs = null;

    private float nextTimeToSpawn;

    private void Awake()
    {
        nextTimeToSpawn = Time.time + Random.Range(minTimeToSpawn, maxTimeToSpawn);
    }

    private void Update()
    {
        if (Time.time > nextTimeToSpawn)
        {
            // Instantiate fruit at a random position and rotation
            Fruit fruitInstance;
            {
                Fruit randomFruitPrefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

                float randomXPosition = Random.Range(leftLimit.position.x, rightLimit.position.x);
                Vector3 newFruitPosition = transform.position + (Vector3.right * randomXPosition);

                const float MAX_ROTATION = 360f;
                Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0f, MAX_ROTATION));

                fruitInstance = Instantiate(randomFruitPrefab, newFruitPosition, randomRotation, parent: transform);
                fruitInstance.Initialize(bottomLimit);
            }

            nextTimeToSpawn = Time.time + Random.Range(minTimeToSpawn, maxTimeToSpawn);
        }
    }
}