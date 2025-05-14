using UnityEngine;
// [추가]
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    public static ScoreManager Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField] private float finishScore = 1000f;
    [SerializeField] private float scoreIncreaseRate = 10f;

    private float currentScore = 0f;
    private float highScore = 0f;

    public float CurrentScore => currentScore;
    public float HighScore => highScore;

    // [추가] UI 텍스트 연결 (Score, HighScore 표시)
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

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
            return;
        }

        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver) return;

        // 시간 흐름에 따른 점수 증가
        currentScore += scoreIncreaseRate * Time.deltaTime;

        // UI 갱신
        if (currentScoreText != null)
        {
            currentScoreText.text = $"Score : {currentScore:F2}";
        }
        if (highScoreText != null)
        {
            highScoreText.text = $"High Score : {highScore:F2}";
        }

        // 목표 점수 달성 시 게임 종료 처리
        if (currentScore >= finishScore)
        {
            GameManager.Instance.GameOver(true);
        }
    }

    public void AddScore(float amount)
    {
        if (GameManager.Instance.IsGameOver) return;
        currentScore += amount;
    }

    public void SaveHighScore()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetFloat("HighScore", highScore);
        }
    }
}
