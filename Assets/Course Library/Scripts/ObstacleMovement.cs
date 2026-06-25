using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    private float objectSpeed = 10f;
    private float leftBound = -12;
    private PlayerController playerControllerScript;
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerControllerScript.isGameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * objectSpeed);
        }

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
