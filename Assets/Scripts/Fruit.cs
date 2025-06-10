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
        // Instantiate 2 fruit pieces and destroy this

        FruitPiece piece = Instantiate(fruitPiece1, transform.position, transform.rotation, parent: transform.parent);
        piece.Initialize(force: eventData.delta * forceMultiplier, (transform.eulerAngles.z < 180.0f) ? 1.0f : -1.0f);

        FruitPiece piece2 = Instantiate(fruitPiece2, transform.position, transform.rotation, parent: transform.parent);
        piece2.Initialize(force: eventData.delta * forceMultiplier, (transform.eulerAngles.z < 180.0f) ? -1.0f : 1.0f);

        Destroy(gameObject);
    }
}
