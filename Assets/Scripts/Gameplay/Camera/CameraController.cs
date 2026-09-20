using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    private GameManager gameManager;

    private Transform car;

    [SerializeField] private Vector3 offset = new Vector3(0, 10, -10);

    private void Awake()
    {
        levelManager.OnCarSpawned += InitializeCar;
    }

    private void OnDestroy()
    {
        levelManager.OnCarSpawned -= InitializeCar;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void LateUpdate()
    {
        if (car != null)
            transform.position = car.position + offset;
    }

    private void InitializeCar(BaseCar car)
    {
        this.car = car.transform;
    }
}
