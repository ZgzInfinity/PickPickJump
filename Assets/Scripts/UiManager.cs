using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    public static UiManager Instance;

    public int score;

    public int bestScore;

    public TMP_Text textScore;

    public TMP_Text textBestScore;

    public Slider slider;

    public TMP_Text currentLevel;

    public TMP_Text nextLevel;

    public Transform helixTopTransform;

    public Transform helixGoalTransform;

    public Transform ballTransform;

    public RectTransform levelCompletedPanel;

    public RectTransform levelGameOverPanel;

    void Awake()
    {
        Instance = this;
    }

    public void resetScore()
    {
        score = 0;
        textScore.text = score.ToString();

        int level = GameLevelManager.Instance.GetCurrentLevel();

        textBestScore.text = PlayerPrefs.GetInt("TopScore").ToString();
        currentLevel.text = (level + 1).ToString();
        nextLevel.text = (level + 2).ToString();
    }

    public void LevelCompleted()
    {
        levelCompletedPanel.gameObject.SetActive(true);
    }

    public void LevelUnCompleted()
    {
        levelCompletedPanel.gameObject.SetActive(false);
    }

    public void LevelGameOver()
    {
        levelGameOverPanel.gameObject.SetActive(true);
    }

    public void LevelNotGameOver()
    {
        levelGameOverPanel.gameObject.SetActive(false);
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

    public void changeSliderLevelAndProgress()
    {
        int level = GameLevelManager.Instance.GetCurrentLevel();

        currentLevel.text = (level + 1).ToString();
        nextLevel.text = (level + 2).ToString();

        float totalDistance = (helixTopTransform.position.y - helixGoalTransform.position.y);
        float distanceLeft = totalDistance - (ballTransform.position.y - helixGoalTransform.position.y);
        float distanceCovered = (distanceLeft / totalDistance);
        slider.value = Mathf.Lerp(slider.value, distanceCovered, 5);
    }
}
