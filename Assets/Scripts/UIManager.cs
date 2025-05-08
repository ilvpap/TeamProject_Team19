using UnityEngine;
using UnityEngine.UI;
using TMPro; // 텍스트메시프로 사용 시

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider healthSlider;      // 체력 바 슬라이더
    [SerializeField] private TextMeshProUGUI scoreText;    // 현재 점수 표기
    [SerializeField] private TextMeshProUGUI highScoreText; // 최고 점수 표기
    [SerializeField] private GameObject resultPanel;   // 결과 화면
    [SerializeField] private TextMeshProUGUI resultScoreText; // 결과 화면 점수
    [SerializeField] private TextMeshProUGUI resultHighScoreText; // 결과 화면 최고 점수
    [SerializeField] private TextMeshProUGUI resultTitleText; // "게임 클리어" or "게임 오버" 타이틀

    // 버튼 연결 (재시작, 종료)
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        if(resultPanel != null)
        {
            resultPanel.SetActive(false);
        }

        if(restartButton != null)
            restartButton.onClick.AddListener(() => GameManager.Instance.RestartGame());

        if(quitButton != null)
            quitButton.onClick.AddListener(() => GameManager.Instance.QuitGame());
    }

    /// <summary>
    /// 체력 바 갱신
    /// </summary>
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if(healthSlider == null) return;

        healthSlider.value = currentHealth / maxHealth;
    }

    /// <summary>
    /// 점수 텍스트 갱신
    /// </summary>
    public void UpdateScoreText(float score)
    {
        if(scoreText == null) return;
        scoreText.text = $"Score: {Mathf.FloorToInt(score)}";
    }

    /// <summary>
    /// 최고 점수 텍스트 갱신
    /// </summary>
    public void UpdateHighScoreText(float highScore)
    {
        if(highScoreText == null) return;
        highScoreText.text = $"HighScore: {Mathf.FloorToInt(highScore)}";
    }

    /// <summary>
    /// 결과 화면 출력
    /// </summary>
    /// <param name="score"></param>
    /// <param name="highScore"></param>
    /// <param name="isClear"></param>
    public void ShowResultPanel(float score, float highScore, bool isClear = false)
    {
        if(resultPanel != null)
        {
            resultPanel.SetActive(true);
        }

        if(resultScoreText != null)
        {
            resultScoreText.text = $"Score: {Mathf.FloorToInt(score)}";
        }

        if(resultHighScoreText != null)
        {
            resultHighScoreText.text = $"HighScore: {Mathf.FloorToInt(highScore)}";
        }

        if(resultTitleText != null)
        {
            resultTitleText.text = isClear ? "GAME CLEAR!" : "GAME OVER";
        }
    }
}
