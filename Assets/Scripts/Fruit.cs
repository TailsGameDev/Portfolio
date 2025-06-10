using UnityEngine;
using UnityEngine.EventSystems;

public class Fruit : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private float speed = 0.0f;
    [SerializeField]
    private float forceMultiplier = 0.0f;
    [SerializeField]
    private float maxForce = 0.0f;
    [SerializeField]
    private float minForce = 0.0f;

    [SerializeField]
    private FruitPiece fruitPiece1 = null;
    [SerializeField]
    private FruitPiece fruitPiece2 = null;


    private Transform cachedBottomLimit;


    public void Initialize(Transform bottomLimit)
    {
        cachedBottomLimit = bottomLimit;
    }

    private void Update()
    {
        // Move fruit down
        transform.position = transform.position + (speed * Time.deltaTime * Vector3.down);

        // Destroy fruit when it gets too far down
        if (transform.position.y < cachedBottomLimit.position.y)
        {
            Destroy(gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Instantiate two fruit pieces and destroy the original
        FruitPiece piece = Instantiate(fruitPiece1, transform.position, transform.rotation, parent: transform.parent);
        FruitPiece piece2 = Instantiate(fruitPiece2, transform.position, transform.rotation, parent: transform.parent);

        // Set adjusted delta based on event data delta to fit min and max bounds
        Vector2 adjustedDelta;
        {
            float magnitude = eventData.delta.magnitude;
            if (magnitude < minForce)
            {
                // Set adjustedDelta to up in case it's zero, or in case it's juts very low, then set it to minimal value
                if (Mathf.Approximately(magnitude, 0.0f))
                {
                    adjustedDelta = minForce * Vector2.up;
                }
                else
                {
                    adjustedDelta = minForce * eventData.delta.normalized;
                }
            }
            else
            {
                // Set adjusted delta to max value in case it's too big, or just the normal value otherwise
                if (eventData.delta.magnitude > maxForce)
                {
                    adjustedDelta = maxForce * eventData.delta.normalized;
                }
                else
                {
                    adjustedDelta = eventData.delta;
                }
            }
        }

        // Calculate directional forces with offset
        const float OFFSET = 15;
        Vector3 forceDirection1 = Quaternion.Euler(0, 0, OFFSET) * adjustedDelta;
        Vector3 forceDirection2 = Quaternion.Euler(0, 0, -OFFSET) * adjustedDelta;

        // Apply the forces with rotationMultiplier
        piece.Initialize(force: forceDirection1 * forceMultiplier, (transform.eulerAngles.z < 180.0f) ? 1.0f : -1.0f);
        piece2.Initialize(force: forceDirection2 * forceMultiplier, (transform.eulerAngles.z < 180.0f) ? -1.0f : 1.0f);

        // Destroy the original fruit
        Destroy(gameObject);
    }
}
