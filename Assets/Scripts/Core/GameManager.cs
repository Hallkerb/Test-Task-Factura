using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private LevelManager levelManager;

    private UIManager uiManager;
    [SerializeField] private FinishLine finishLine;

    private BaseCar car;

    public event Action OnStart;
    public event Action OnEnd;
    public event Action OnWin;
    public event Action OnLose;
    public event Action OnRestart;

    private void Awake()
    {
        Instance = this;

        levelManager.OnCarSpawned += InitializeCar;
    }

    private void Start()
    {
        uiManager = UIManager.Instance;

        uiManager.OnStart += StartGame;
        uiManager.OnRestart += RestartGame;
        finishLine.OnFinished += Win;
    }

    private void RestartGame()
    {
        OnRestart?.Invoke();
    }

    private void StartGame()
    {
        OnStart?.Invoke();
    }

    private void EndGame()
    {
        OnEnd?.Invoke();
    }

    private void Win()
    {
        OnWin?.Invoke();
        EndGame();

        Debug.Log("You Finished!");
    }

    private void Lose(IHealth health)
    {
        OnLose?.Invoke();
        EndGame();
    }

    private void InitializeCar(BaseCar car)
    {
        car.GetComponent<IHealth>().OnDied += Lose;

        this.car = car.GetComponent<BaseCar>();
    }
}
