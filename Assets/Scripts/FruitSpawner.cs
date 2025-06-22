using UnityEngine;
using UnityEngine.UI;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform leftLimit = null;
    [SerializeField]
    private Transform rightLimit = null;
    [SerializeField]
    private Transform topLimit = null;
    [SerializeField]
    private Transform bottomLimit = null;

    [SerializeField]
    private float minTimeToSpawn = 0.0f;
    [SerializeField]
    private float maxTimeToSpawn = 0.0f;

    [SerializeField]
    private int initialFruitsAmount = 0;

    [SerializeField]
    private Fruit[] fruitPrefabs = null;

    private float nextTimeToSpawn;


    private void Start()
    {
        // Instantiate a range of random fruits to fill the screen right from start
        // NOTE: Instantiate fruits at Start so Unity UI has time to set its position
        for (int f = 0; f < initialFruitsAmount; f++)
        {
            float randomXPosition = Random.Range(leftLimit.position.x, rightLimit.position.x);
            float randomYPosition = Random.Range(topLimit.position.y, bottomLimit.position.y);
            InstantiateFruitWithRandomRotation(fruitPosition: topLimit.position + Vector3.right * randomXPosition + Vector3.down * randomYPosition);
        }

        // Set time to spawn the next fruit
        nextTimeToSpawn = Time.time + Random.Range(minTimeToSpawn, maxTimeToSpawn);
    }

    private void Update()
    {
        if (Time.time > nextTimeToSpawn)
        {
            // Instantiate fruit at a random position and rotation
            {
                float randomXPosition = Random.Range(leftLimit.position.x, rightLimit.position.x);
                InstantiateFruitWithRandomRotation(fruitPosition: topLimit.position + Vector3.right * randomXPosition);
            }

            nextTimeToSpawn = Time.time + Random.Range(minTimeToSpawn, maxTimeToSpawn);
        }
    }
    private void InstantiateFruitWithRandomRotation(Vector3 fruitPosition)
    {
        Fruit randomFruitPrefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

        const float MAX_ROTATION = 360.0f;
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0f, MAX_ROTATION));

        Fruit fruitInstance = Instantiate(randomFruitPrefab, fruitPosition, randomRotation, parent: transform);
        fruitInstance.Initialize(bottomLimit);
    }
}