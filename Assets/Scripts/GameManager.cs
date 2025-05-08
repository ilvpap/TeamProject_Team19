using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    [Header("게임 진행 설정")]
    [SerializeField] private float initialHealth = 100f;   // 초기 체력
    [SerializeField] private float healthDecreaseRate = 1f; // 초당 체력 감소량
    [SerializeField] private float collisionDamage = 20f;   // 장애물/적 충돌 시 감소량
    [SerializeField] private float finishScore = 1000f;     // 이 점수 도달 시 게임 클리어
    
    [Header("점수 설정")]
    [SerializeField] private float scoreIncreaseRate = 10f;  // 초당 점수 상승량

    [Header("UI 연동")]
    [SerializeField] private UIManager uiManager;  // UIManager 스크립트 레퍼런스
    [SerializeField] private PlayerController player; // 플레이어 레퍼런스

    private float currentHealth;
    private float currentScore;
    private bool isGameOver = false;
    private float highScore;

    public float CurrentHealth => currentHealth;
    public float CurrentScore => currentScore;
    public bool IsGameOver => isGameOver;

    private void Awake()
    {
        // 싱글톤 할당
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // 최고점수 로드
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
    }

    private void Start()
    {
        // 초기화
        currentHealth = initialHealth;
        currentScore = 0f;
        isGameOver = false;

        if(uiManager != null)
        {
            uiManager.UpdateHealthBar(currentHealth, initialHealth);
            uiManager.UpdateScoreText(currentScore);
            uiManager.UpdateHighScoreText(highScore);
        }
    }

    private void Update()
    {
        if (isGameOver) return;

        // 1) 체력 감소
        currentHealth -= healthDecreaseRate * Time.deltaTime;
        if(currentHealth <= 0f)
        {
            currentHealth = 0f;
            GameOver();
        }

        // 2) 점수 증가
        currentScore += scoreIncreaseRate * Time.deltaTime;

        // 3) 게임 클리어 조건 확인 (finishScore 도달)
        if(currentScore >= finishScore)
        {
            // 완주로 간주
            GameOver(true);
        }

        // UI 갱신
        if(uiManager != null)
        {
            uiManager.UpdateHealthBar(currentHealth, initialHealth);
            uiManager.UpdateScoreText(currentScore);
        }
    }

    /// <summary>
    /// 장애물 또는 적과 충돌 시 체력 감소
    /// </summary>
    public void TakeDamage()
    {
        if(isGameOver) return;

        currentHealth -= collisionDamage;
        if(currentHealth <= 0f)
        {
            currentHealth = 0f;
            GameOver();
        }
    }

    /// <summary>
    /// 아이템으로 체력 회복
    /// </summary>
    /// <param name="amount">회복량</param>
    public void RecoverHealth(float amount)
    {
        if(isGameOver) return;

        currentHealth += amount;
        if(currentHealth > initialHealth)
        {
            currentHealth = initialHealth;
        }
    }

    /// <summary>
    /// 아이템으로 점수 증가
    /// </summary>
    /// <param name="amount">점수 증가량</param>
    public void AddScore(float amount)
    {
        if(isGameOver) return;

        currentScore += amount;
    }

    /// <summary>
    /// 게임 오버 처리
    /// </summary>
    /// <param name="isClear">클리어 여부</param>
    private void GameOver(bool isClear = false)
    {
        isGameOver = true;
        // 플레이어 이동 정지
        if(player != null)
        {
            player.StopMovement();
        }

        // 최고 점수 갱신
        if(currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetFloat("HighScore", highScore);
        }

        // 결과 화면 표시
        if(uiManager != null)
        {
            uiManager.ShowResultPanel(currentScore, highScore, isClear);
        }
    }

    /// <summary>
    /// 다시 시작
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// 게임 종료(에디터 상에서는 플레이 멈춤)
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
