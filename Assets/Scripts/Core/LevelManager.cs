using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instanse;

    [SerializeField] private SpawnableType car = SpawnableType.Car;
    [SerializeField] private SpawnableType enemy = SpawnableType.Stickman;

    [SerializeField] private Transform enemiesContainer;
    [SerializeField] private BoxCollider enemySpawnArea;

    [SerializeField] private int enemyCount = 20;

    private BaseCar _car;
    public BaseCar Car => _car;

    public event Action<BaseCar> OnCarSpawned;

    private void Awake()
    {
        Instanse = this;
    }

    private void Start()
    {
        SpawnCar();
        SpawnEnemies();

        GameManager.Instance.OnRestart += Restart;
    }

    private void Restart()
    {
        SpawnManager.Instance.DespawnAll();

        SpawnCar();
        SpawnEnemies();
    }
    
    private void SpawnCar()
    {
        _car = SpawnManager.Instance.Spawn(car, Vector3.zero, Quaternion.identity, transform).GetComponent<BaseCar>();

        OnCarSpawned?.Invoke(Car);
    }

    private void SpawnEnemies()
    {
        for(int i = 0; i < enemyCount; i++)
        {
            float positionX = UnityEngine.Random.Range(enemySpawnArea.bounds.min.x, enemySpawnArea.bounds.max.x);
            float positionZ = UnityEngine.Random.Range(enemySpawnArea.bounds.min.z, enemySpawnArea.bounds.max.z);

            Vector3 spawnPosition = new Vector3(positionX, 0, positionZ);
            Quaternion spawnRotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);

            SpawnManager.Instance.Spawn(enemy, spawnPosition, spawnRotation, enemiesContainer);
        }
    }
}
