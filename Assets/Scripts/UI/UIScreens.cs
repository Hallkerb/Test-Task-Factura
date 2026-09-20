using UnityEngine;

public class UIScreens : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject loseMenu;

    private void Start()
    {
        gameManager = GameManager.Instance;

        gameManager.OnWin += ActivateWinMenu;
        gameManager.OnLose += ActivateLoseMenu;
        gameManager.OnRestart += DeactivateWinMenu; 
        gameManager.OnRestart += DeactivateLoseMenu;
    }

    private void OnDestroy()
    {
        gameManager.OnWin -= ActivateWinMenu;
        gameManager.OnLose -= ActivateLoseMenu;
        gameManager.OnRestart -= DeactivateWinMenu; 
        gameManager.OnRestart -= DeactivateLoseMenu;
    }

    public void ActivateWinMenu()
    {
        winMenu.SetActive(true);
    }

    public void DeactivateWinMenu()
    {
        winMenu.SetActive(false);
    }

    public void ActivateLoseMenu()
    {
        loseMenu.SetActive(true);
    }

    public void DeactivateLoseMenu()
    {
        loseMenu.SetActive(false);
    }
}
