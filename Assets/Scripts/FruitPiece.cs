using UnityEngine;

public class FruitPiece : MonoBehaviour
{
    [SerializeField]
    private float gravitySpeed = 0.0f;
    [SerializeField]
    private float friction = 0.0f;
    [SerializeField]
    private float rotationFriction = 0.0f;

    private Vector3 cachedForce;
    private float cachedRotationMultiplier;

    public void Initialize(Vector3 force, float rotationMultiplier)
    {
        cachedForce = force;
        cachedRotationMultiplier = rotationMultiplier;
    }

    private void Update()
    {
        // Movement
        transform.position = transform.position + Time.deltaTime * (cachedForce + gravitySpeed * Vector3.down);

        // Friction
        cachedForce *= Mathf.Pow(1 - friction, Time.deltaTime);

        // Rotation
        float rotationForce = cachedForce.magnitude;
        float rotationSpeed = cachedRotationMultiplier * Mathf.Pow(1 - rotationFriction, Time.deltaTime) * rotationForce;
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
