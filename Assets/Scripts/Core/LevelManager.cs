using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private GameManager gameManager;
    [SerializeField] private SpawnableType carType = SpawnableType.Car;
    [SerializeField] private SpawnableType enemyType = SpawnableType.Stickman;

    [SerializeField] private Transform enemiesContainer;
    [SerializeField] private BoxCollider enemySpawnArea;
    [SerializeField] private Vector3 carSpawnPosition = Vector3.zero;

    [SerializeField] private int enemyCount = 20;

    private BaseCar car;
    public BaseCar Car => car;

    public Vector3 CarSpawnPosition => carSpawnPosition;

    public event Action<BaseCar> OnCarSpawned;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;

        gameManager.OnRestart -= Restart;
    }

    private void Start()
    {
        SpawnCar();
        SpawnEnemies();

        gameManager = GameManager.Instance;
        gameManager.OnRestart += Restart;
    }

    private void Restart()
    {
        SpawnManager.Instance.DespawnAll();

        SpawnCar();
        SpawnEnemies();
    }
    
    private void SpawnCar()
    {
        car = SpawnManager.Instance.Spawn(carType, carSpawnPosition, Quaternion.identity, transform).GetComponent<BaseCar>();

        OnCarSpawned?.Invoke(Car);
    }

    private void SpawnEnemies()
    {
        float minX = enemySpawnArea.bounds.min.x;
        float maxX = enemySpawnArea.bounds.max.x;
        float minZ = enemySpawnArea.bounds.min.z;
        float maxZ = enemySpawnArea.bounds.max.z;

        for(int i = 0; i < enemyCount; i++)
        {
            float positionX = UnityEngine.Random.Range(minX, maxX);
            float positionZ = UnityEngine.Random.Range(minZ, maxZ);

            Vector3 spawnPosition = new Vector3(positionX, 0, positionZ);
            Quaternion spawnRotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);

            SpawnManager.Instance.Spawn(enemyType, spawnPosition, spawnRotation, enemiesContainer);
        }
    }
}
