using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text scoreText;
    public Text bestScoreText;

    private int score;
    private int bestScore;

    void Start()
    {
        score = 0;
        bestScore = PlayerPrefs.GetInt("BestScore", 0); // Загружаем лучший результат
        UpdateScore();
        UpdateBestScore();
    }

    void Update()
    {
        // Увеличиваем счёт только если ещё не столкнулись
        if (!GameManager.isGameOver)
        {
            score++;
            UpdateScore();
        }
    }

    public void GameOver()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore); // Сохраняем лучший результат
        }
        UpdateBestScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateBestScore()
    {
        bestScoreText.text = "Best Score: " + bestScore;
    }
}
