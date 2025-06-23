using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private Transform obstacleParent;
    public float obstacleSpawnTime = 3f;
    [Range(0, 1)] public float obstacleSpawnTimeFactor = 0.1f;
    [Range(0, 1)] public float obstacleSpeedFactor = 0.2f;

    public float obstacleSpeed = 4f;

    private float _obstacleSpawnTime;
    private float _obstacleSpeed;
    private float timeAlive;
    private float timeUntilObstacleSpawn;

    private void Start()
    {
        timeAlive = 1f;
        GameManager.Instance.onplay.AddListener(ClearObstacles); // Clear obstacles when the game starts
        GameManager.Instance.onGameOver.AddListener(ClearObstacles); // Clear obstacles when the game ends
        GameManager.Instance.onplay.AddListener(ResetFactors);
    }

    private void Update()
    {
        if (GameManager.Instance.isPlaying)
        {
            timeAlive += Time.deltaTime; // Increment time alive only if the game is playing

            CalculateFactors();
            SpawnLoop();
        }
    } // Updates the time alive and calculates the spawn time and speed of obstacles, then calls the spawn loop to handle obstacle spawning.

    private void SpawnLoop()
    {
        timeUntilObstacleSpawn += Time.deltaTime;

        if (timeUntilObstacleSpawn >= _obstacleSpawnTime)
        {
            Spawn();
            timeUntilObstacleSpawn = 0f;
        }
    } // Handles the spawning of obstacles at regular intervals, adjusting the spawn time based on how long the game has been running.

    private void ResetFactors()
    {
        timeAlive = 1f;
        _obstacleSpawnTime = obstacleSpawnTime;
        _obstacleSpeed = obstacleSpeed;
    } // Resets the time alive and obstacle factors when the game starts or restarts.
    private void ClearObstacles()
    {
        foreach (Transform child in obstacleParent)
        {
            Destroy(child.gameObject); // Destroys all obstacles under the obstacleParent
        }
    }
    private void CalculateFactors()
    {
        _obstacleSpawnTime = obstacleSpawnTime / Mathf.Pow(timeAlive, obstacleSpawnTimeFactor);
        _obstacleSpeed = obstacleSpeed * Mathf.Pow(timeAlive, obstacleSpeedFactor);
    } // Calculates the spawn time and speed of obstacles based on how long the game has been running, adjusted by specified factors.

    private void Spawn()
    {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        GameObject spawnedObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);
        spawnedObstacle.transform.SetParent(obstacleParent);

        Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();
        obstacleRB.velocity = Vector2.left * _obstacleSpeed; // tells obstacle to move left at the specified speed
    }
}
