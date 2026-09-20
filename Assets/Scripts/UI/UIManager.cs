using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    [SerializeField] private Button startButton;
    [SerializeField] private Button endButton;

    public event Action OnStart;
    public event Action OnRestart;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnEnd += ReactOnEndGame;
    }

    private void OnEnable()
    {
        startButton.onClick.AddListener(StartGame);
        endButton.onClick.AddListener(RestartGame);
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(StartGame);
        endButton.onClick.RemoveListener(RestartGame);
    }

    private void StartGame()
    {
        startButton.gameObject.SetActive(false);
        
        OnStart?.Invoke();
    }

    private void RestartGame()
    {
        endButton.gameObject.SetActive(false);
        
        OnRestart?.Invoke();

        startButton.gameObject.SetActive(true);
    }

    private void ReactOnEndGame()
    {
        endButton.gameObject.SetActive(true);
    }
}
