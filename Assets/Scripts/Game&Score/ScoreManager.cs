using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("점수 설정")]
    [SerializeField] private float finishScore = 1000f;      // 게임 클리어에 필요한 점수
    [SerializeField] private float scoreIncreaseRate = 10f;  // 초당 점수 상승량

    private float currentScore = 0f;
    private float highScore = 0f;

    public float CurrentScore => currentScore;
    public float HighScore => highScore;

    private void Awake()
    {
        // 이전에 저장된 하이스코어 불러오기
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
    }

    private void Update()
    {
        // 게임오버 시 더 이상 점수를 올리지 않음
        if (GameManager.Instance.IsGameOver) return;

        // 초당 점수 증가
        currentScore += scoreIncreaseRate * Time.deltaTime;

        // finishScore 도달 시 게임 클리어 처리
        if (currentScore >= finishScore)
        {
            GameManager.Instance.GameOver(true);
        }
    }

    /// <summary>
    /// 외부(아이템 획득 등)에서 점수를 추가할 때 호출
    /// </summary>
    public void AddScore(float amount)
    {
        if (GameManager.Instance.IsGameOver) return;
        currentScore += amount;
    }

    /// <summary>
    /// 현재 점수를 기준으로 하이스코어 갱신
    /// </summary>
    public void SaveHighScore()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetFloat("HighScore", highScore);
        }
    }
}
