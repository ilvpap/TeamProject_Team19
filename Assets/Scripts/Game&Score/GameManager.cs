using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
// using UnityEngine.SceneManagement; // 중복으로 인해 주석 처리
using System;

public enum SceneType
{
    Main,
    InGame,
    Current
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    [SerializeField] private Player player;
    private PlayerStats playerStats;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button startButton;
    [SerializeField] private AudioClip bgmClip;

    private AudioSource createdAudioSource;
    private bool isGameOver = false;
    public bool IsGameOver => isGameOver;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        isGameOver = false;

        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
        if (player != null)
        {
            playerStats = player.Stat;
        }
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(() => GameOver(false));
        }
        if (bgmClip != null)
        {
            createdAudioSource = gameObject.AddComponent<AudioSource>();
            createdAudioSource.clip = bgmClip;
            createdAudioSource.loop = true;
            createdAudioSource.Play();
        }
        if (startButton != null)
        {
            startButton.onClick.AddListener(() => LoadScene(SceneType.InGame));
        }
    }

    private void Update()
    {
        if (!isGameOver && playerStats != null && playerStats.CurHp <= 0)
        {
            GameOver(false);
        }
    }

    public void GameOver(bool isClear = false)
    {
        isGameOver = true;
        if (createdAudioSource != null)
        {
            createdAudioSource.Stop();
        }
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.SaveHighScore();
        }
        if (playerStats != null)
        {
            Debug.Log($"[GameOver] Player HP : {playerStats.CurHp} / {playerStats.MaxHp}");
        }
        else
        {
            Debug.Log("게임 오버!");
            Time.timeScale = 0f;
            if (gameOverText != null)
            {
                gameOverText.text = "Game Over";
                gameOverText.gameObject.SetActive(true);
            }
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        else
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    public void LoadScene(SceneType type)
    {
        Time.timeScale = 1f;
        switch (type)
        {
            case SceneType.Main:
                SceneManager.LoadScene("MainScene");
                break;
            case SceneType.InGame:
                SceneManager.LoadScene("InGameScene");
                break;
            case SceneType.Current:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
        }
    }
}
