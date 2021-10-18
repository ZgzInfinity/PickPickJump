using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager Instance;

    public int score;

    public int bestScore;

    public TMP_Text textScore;

    public TMP_Text textBestScore;

    void Awake()
    {
        Instance = this;
    }


    public void resetScore()
    {
        score = 0;
        textScore.text = score.ToString();
        textBestScore.text = PlayerPrefs.GetInt("TopScore").ToString();
    }

    public void updateScore(int scorePoints)
    {
        addScore(scorePoints);
        textScore.text = score.ToString();
        updateBestScore();
    }

    private void addScore(int scorePoints)
    {
        score += scorePoints;
    }

    private void updateBestScore()
    {
        if (score > PlayerPrefs.GetInt("TopScore"))
        {
            PlayerPrefs.SetInt("TopScore", score);
            textBestScore.text = score.ToString();
        }
    }
}
