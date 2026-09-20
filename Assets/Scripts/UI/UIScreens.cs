using UnityEngine;

public class UIScreens : MonoBehaviour
{
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject loseMenu;

    private void Start()
    {
        GameManager manager = GameManager.Instance;

        manager.OnWin += () => SetActiveWinMenu(true);
        manager.OnLose += () => SetActiveLoseMenu(true);
        manager.OnRestart += () => { SetActiveWinMenu(false); SetActiveLoseMenu(false); };
    }

    public void SetActiveWinMenu(bool value)
    {
        winMenu.SetActive(value);
    }

    public void SetActiveLoseMenu(bool value)
    {
        loseMenu.SetActive(value);
    }
}
