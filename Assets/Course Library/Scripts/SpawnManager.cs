using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    // Variables to get the player controller script and the prefabs of the obstacles, bombs and money
    private PlayerController playerControllerScript;
    public GameObject bombPrefab;
    public GameObject moneyPrefab;
    public GameObject[] obstacles = new GameObject[2];

    // Variables to set the spawn position of the obstacles, bombs and money
    private Vector3 spawnObstaclePosition = new Vector3(20, 0, 0);
    private Vector3 spawnBombPosition = new Vector3(20, 8, 0);
    private Vector3 spawnMoneyPosition = new Vector3(20, 5, 0);

    // Variables to set the spawn rate and delay of the obstacles, bombs and money
    private float spawnObstacleDelay = 2f;
    private float spwanObstacleRate = 3f;

    private float spawnBombDelay = 7f;
    private float spwanBombRate = 9f;

    private float spawnMoneyDelay = 4f;
    private float spwanMoneyRate = 10f;
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        // Methods to spawn obstacles, bombs and money at a certain rate and delay since the game is started

        InvokeRepeating("SpawnObstacles", spawnObstacleDelay, spwanObstacleRate);
        InvokeRepeating("SpawnBombObstacles", spawnBombDelay, spwanBombRate);
        InvokeRepeating("SpawnMoneyObstacles", spawnMoneyDelay, spwanMoneyRate);

    }

    void Update()
    {

    }

    private void SpawnObstacles()
    {
        // Conditional to spawn obstacles only if the game is not over

        if (playerControllerScript.isGameOver == false)
        {
            int obstacleIndex = UnityEngine.Random.Range(0, obstacles.Length);
            Instantiate(obstacles[obstacleIndex],spawnObstaclePosition, obstacles[obstacleIndex].transform.rotation);
        }
    }

    private void SpawnBombObstacles()
    {
        // Conditional to spawn bombs only if the game is not over

        if (playerControllerScript.isGameOver == false)
        {
            Instantiate(bombPrefab, spawnBombPosition, bombPrefab.transform.rotation);
        }
    }

    private void SpawnMoneyObstacles()
    {
        // Conditional to spawn money only if the game is not over

        if (playerControllerScript.isGameOver == false)
        {
            Instantiate(moneyPrefab, spawnMoneyPosition, moneyPrefab.transform.rotation);
        }
    }
}
