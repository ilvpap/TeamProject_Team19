using UnityEngine;
using UnityEngine.SceneManagement;

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
    }

    /// <summary>
    /// 게임 오버 처리 (isClear가 true면 클리어로 간주)
    /// </summary>
    public void GameOver(bool isClear = false)
    {
        isGameOver = true;

        // 점수 저장
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.SaveHighScore();
        }

        // 추가적인 게임 오버 처리(연출, 사운드 등)는 여기서 구현
    }

    /// <summary>
    /// 다시 시작
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// 게임 종료(에디터 상에서는 플레이 중지)
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
