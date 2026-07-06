using UnityEngine;

public class SpinMovement : MonoBehaviour
{
    private float spinSpeed = 50f;

    // Method to rotate the object around its Y-axis
    void Update()
    {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}
