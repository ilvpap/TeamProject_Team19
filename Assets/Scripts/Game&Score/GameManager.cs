using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.SaveHighScore();
        }
        if (playerStats != null)
        {
            Debug.Log($"[GameOver] Player HP : {playerStats.CurHp} / {playerStats.MaxHp}");
        }
        if (isClear)
        {
            Debug.Log("게임 클리어!");
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
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
