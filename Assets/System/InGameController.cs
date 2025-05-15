using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameController : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pauseMenu;

    public void OnRestartButton()
    {

        GameManager.Instance.RestartGame();
    }

    public void OnPauseButton()
    {
        pauseMenu.SetActive(true);
    }
}
