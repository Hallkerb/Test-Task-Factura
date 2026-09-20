using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private GameManager gameManager;
    
    [SerializeField] private Button startButton;
    [SerializeField] private Button restartButton;

    public event Action OnStart;
    public event Action OnRestart;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;

        gameManager.OnEnd -= ReactOnEndGame;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.OnEnd += ReactOnEndGame;
    }

    private void OnEnable()
    {
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(StartGame);
        restartButton.onClick.RemoveListener(RestartGame);
    }

    private void StartGame()
    {
        startButton.gameObject.SetActive(false);
        
        OnStart?.Invoke();
    }

    private void RestartGame()
    {
        restartButton.gameObject.SetActive(false);
        
        OnRestart?.Invoke();

        startButton.gameObject.SetActive(true);
    }

    private void ReactOnEndGame()
    {
        restartButton.gameObject.SetActive(true);
    }
}
