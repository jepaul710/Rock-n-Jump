using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ObjectsMovement : MonoBehaviour
{
    // Variables to set the speed of the objects, the left bound of the scene and the player controller script
    private float objectSpeed = 5f;
    private float leftBound = -12;
    private PlayerController playerControllerScript;
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        // Conditional to move the objects only if the game is not over

        if (playerControllerScript.isGameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * objectSpeed, Space.World);
        }

        // Conditional to destroy the objects when they go out of the scene

        if (transform.position.x < leftBound && gameObject.CompareTag("Money"))
        {
            Destroy(gameObject);
        }
        else if (transform.position.x < leftBound && gameObject.CompareTag("Bomb"))
        {
            Destroy(gameObject);
        }
    }
}
