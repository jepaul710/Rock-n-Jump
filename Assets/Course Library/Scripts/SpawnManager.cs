using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject conePrefab;
    public GameObject barrierPrefab;
    public GameObject bombPrefab;
    public GameObject moneyPrefab;

    private PlayerController playerControllerScript;

    private Vector3 spawnConePosition = new Vector3(20, 0, 0);
    private Vector3 spawnBarrierPosition = new Vector3(20, 0, 0);
    private Vector3 spawnBombPosition = new Vector3(20, 8, 0);
    private Vector3 spawnMoneyPosition = new Vector3(20, 5, 0);


    private float spawnConeDelay = 2f;
    private float spwanConeRate = 3f;

    private float spawnBarrierDelay = 9f;
    private float spwanBarrierRate = 15f;

    private float spawnBombDelay = 7f;
    private float spwanBombRate = 9f;

    private float spawnMoneyDelay = 4f;
    private float spwanMoneyRate = 10f;
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        InvokeRepeating("SpawnConeObstacles", spawnConeDelay, spwanConeRate);
        InvokeRepeating("SpawnBarrierObstacles", spawnBarrierDelay, spwanBarrierRate);
        InvokeRepeating("SpawnBombObstacles", spawnBombDelay, spwanBombRate);
        InvokeRepeating("SpawnMoneyObstacles", spawnMoneyDelay, spwanMoneyRate);

    }

    void Update()
    {

    }

    private void SpawnConeObstacles()
    {
        if (playerControllerScript.isGameOver == false)
        {
            Instantiate(conePrefab, spawnConePosition, conePrefab.transform.rotation);
        }
    }

    private void SpawnBarrierObstacles()
    {
        if (playerControllerScript.isGameOver == false)
        {
            Instantiate(barrierPrefab, spawnBarrierPosition, barrierPrefab.transform.rotation);
        }
    }

    private void SpawnBombObstacles()
    {
        if (playerControllerScript.isGameOver == false)
        {
            Instantiate(bombPrefab, spawnBombPosition, bombPrefab.transform.rotation);
        }
    }

    private void SpawnMoneyObstacles()
    {
        if (playerControllerScript.isGameOver == false)
        {
            Instantiate(moneyPrefab, spawnMoneyPosition, moneyPrefab.transform.rotation);
        }
    }
}
