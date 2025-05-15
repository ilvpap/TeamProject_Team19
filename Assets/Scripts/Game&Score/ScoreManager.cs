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
        SaveHighScore();
        if (GameManager.Instance.IsGameOver) return;

        currentScore += scoreIncreaseRate * Time.deltaTime;

        if (currentScoreText != null)
        {
            currentScoreText.text = $"{currentScore:F0}";
        }
        if (highScoreText != null)
        {
            highScoreText.text = $"High Score \n {highScore:F0}";
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
