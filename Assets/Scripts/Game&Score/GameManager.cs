using UnityEngine;
using UnityEngine.SceneManagement;
// [추가]
using UnityEngine.UI;

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

    // [추가] Player 및 PlayerStats 참조를 위해 필드 선언
    [SerializeField] private Player player;
    private PlayerStats playerStats;

    // [추가] UI 버튼 연결을 위한 필드
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    private bool isGameOver = false;
    public bool IsGameOver => isGameOver;

    private void Awake()
    {
        // 싱글톤 할당
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
        // 게임 시작 시 기본값
        isGameOver = false;

        // [추가] 인스펙터에서 player를 지정하지 않았다면 씬에서 찾아 할당
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }

        // [추가] player가 유효하다면, 그 안의 PlayerStats를 참조
        if (player != null)
        {
            playerStats = player.Stat;
        }

        // [추가] 인스펙터에서 restartButton, quitButton이 지정되어 있다면 이벤트 연결
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
    }

    /// 게임 종료 처리 (isClear가 true면 클리어로 처리)
    public void GameOver(bool isClear = false)
    {
        isGameOver = true;

        // [기존 코드 - 주석 처리]
        // ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        // if (scoreManager != null)
        // {
        //     scoreManager.SaveHighScore();
        // }

        // [수정 코드 - 싱글톤 연결]
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.SaveHighScore();
        }

        // [추가] PlayerStats 정보를 확인하거나 로그 출력 등 추가 동작 가능
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
        }
    }

    /// 재시작
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// 게임 종료(에디터 상에서는 플레이 중단)
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
