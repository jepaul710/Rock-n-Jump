using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{

    // Variables to set the speed of the obstacles and barriers, the left bound of the scene and the player controller script
    public static float objectSpeed = 10f;
    private float leftBound = -12;
    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        // Conditional to move the obstacles and barriers only when the game is not over
        
        if (playerControllerScript.isGameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * objectSpeed);
        }

        // Conditional to destroy the obstacles and barriers when they go out of the scene

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
        else if (transform.position.x < leftBound && gameObject.CompareTag("Barrier"))
        {
            Destroy(gameObject);
        }
    }
}
