using UnityEngine;

/// <summary>
/// Controls the visual behavior of a collectible package, making it float
/// up and down while slowly rotating around the Y-axis.
/// </summary>
public class FloatingPackage : MonoBehaviour
{
    // --- Public Properties for Customization (Visible in the Inspector) ---

    [Header("Rotation Settings")]
    [Tooltip("The speed at which the package rotates, in degrees per second.")]
    [SerializeField] private float rotationSpeed = 50.0f;

    [Header("Floating Settings")]
    [Tooltip("The maximum distance the package moves up/down from its starting position.")]
    [SerializeField] private float amplitude = 0.5f;

    [Tooltip("The speed (frequency) of the vertical floating wave.")]
    [SerializeField] private float frequency = 1.0f;

    // --- Private Variables ---

    // Stores the initial world position to keep the floating motion centered.
    private Vector3 _startPos;

    // ----------------------------------------------------------------------

    void Start()
    {
        // Save the object's starting position in the world when the game begins.
        _startPos = transform.position;
    }

    void Update()
    {
        // 1. Handle Rotation (Spinning)
        // Rotate the GameObject around the Y-axis (Vector3.up).
        // Time.deltaTime ensures the rotation speed is frame-rate independent.
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

        // 2. Handle Floating (Vertical Movement)
        // Use the Sine wave function (Mathf.Sin) for smooth, oscillating motion.
        // Time.time gives a continuously increasing value for the wave's phase.
        float time = Time.time * frequency;
        float newY = _startPos.y + Mathf.Sin(time) * amplitude;

        // Apply the new position, keeping X and Z fixed.
        transform.position = new Vector3(_startPos.x, newY, _startPos.z);
    }
}