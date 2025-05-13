using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Elements")] 
    public TextMeshProUGUI scoreText;         // 현재 점수
    public TextMeshProUGUI highScoreText;     // 최고 점수 표시
    public GameObject gameOverPanel;          // 게임 오버 시 뜨는 패널
    public TextMeshProUGUI gameOverText;      // "Game Over" 문구

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateScore(float score)
    {
        if (scoreText != null)
            scoreText.text = $"Score : {score:F2}";
    }

    public void UpdateHighScore(float highScore)
    {
        if (highScoreText != null)
            highScoreText.text = $"High Score : {highScore:F2}";
    }
    
    public void ShowGameOver(string message)
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (gameOverText != null) gameOverText.text = message;
        
    }
}
