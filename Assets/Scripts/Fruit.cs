using UnityEngine;
using UnityEngine.EventSystems;

public class Fruit : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private float speed = 0.0f;
    [SerializeField]
    private float forceMultiplier = 0.0f;

    [SerializeField]
    private FruitPiece fruitPiece1 = null;
    [SerializeField]
    private FruitPiece fruitPiece2 = null;


    private Transform cachedFruitPiecesParent;


    public void Initialize(Transform fruitPiecesParent)
    {
        cachedFruitPiecesParent = fruitPiecesParent;
    }

    private void Update()
    {
        // Move fruit down
        transform.position = transform.position + (speed * Time.deltaTime * Vector3.down);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Instantiate two fruit pieces and destroy the original
        FruitPiece piece = Instantiate(fruitPiece1, transform.position, transform.rotation, parent: transform.parent);
        FruitPiece piece2 = Instantiate(fruitPiece2, transform.position, transform.rotation, parent: transform.parent);

        // Calculate directional forces with offset
        const float OFFSET = 15;
        Vector3 forceDirection1 = Quaternion.Euler(0, 0, OFFSET) * eventData.delta;
        Vector3 forceDirection2 = Quaternion.Euler(0, 0, -OFFSET) * eventData.delta;

        // Apply the forces with rotationMultiplier
        piece.Initialize(force: forceDirection1 * forceMultiplier, (transform.eulerAngles.z < 180.0f) ? 1.0f : -1.0f);
        piece2.Initialize(force: forceDirection2 * forceMultiplier, (transform.eulerAngles.z < 180.0f) ? -1.0f : 1.0f);

        // Destroy the original fruit
        Destroy(gameObject);
    }
}
