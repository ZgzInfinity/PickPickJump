
/*
 * ------------------------------------------
 * -- Project: Helix Jump -------------------
 * -- Author: Rubén Rodríguez Estebban ------
 * -- Date: 31/10/2021 ----------------------
 * ------------------------------------------
 */

using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
 * Script that controls the HUD of the game
 */

public class UiManager : MonoBehaviour
{

    // Score of the player
    private int score;

    // Best score registered in game
    private int bestScore;

    // Static instance
    public static UiManager Instance;

    // Reference to the score text indicator
    public TMP_Text textScore;

    // Reference to the best score text indicator
    public TMP_Text textBestScore;

    // Reference to the slider 
    public Slider slider;

    // Reference to the current text indicator
    public TMP_Text currentLevel;

    // Reference to the next level text indicator
    public TMP_Text nextLevel;

    // Reference to the helix top transform
    public Transform helixTopTransform;

    // Reference to the helix goal transform
    public Transform helixGoalTransform;

    // Reference to the ball transform
    public Transform ballTransform;

    // Reference to the level completed panel
    public RectTransform levelCompletedPanel;

    // Reference to the game over panel
    public RectTransform levelGameOverPanel;

    // Awake is called one time when the scene is loaded
    private void Awake()
    {
        // Initialization
        Instance = this;
    }

    // Add new score
    private void AddScore(int scorePoints)
    {
        // Add the score
        score += scorePoints;
    }

    // Update the best score of the game
    private void UpdateBestScore()
    {
        // Check if the best score has been improved
        if (score > PlayerPrefs.GetInt("TopScore"))
        {
            // Set the best score value with the new best score
            PlayerPrefs.SetInt("TopScore", score);
            textBestScore.text = score.ToString();
        }
    }

    // Reset the score to zero and updates the current and next level indicators
    public void ResetScore()
    {
        // Set the score to zero and assign it to the score text indicator
        score = 0;
        textScore.text = score.ToString();

        // Get the current level where the player plays
        int level = GameLevelManager.Instance.GetCurrentLevel();

        // Set the score to zero and assign it to the best score text indicator
        textBestScore.text = PlayerPrefs.GetInt("TopScore").ToString();

        // Update the current and next level indicators
        currentLevel.text = (level + 1).ToString();
        nextLevel.text = (level + 2).ToString();
    }

    // Display the level completed panel
    public void LevelCompleted()
    {
        // Display the panel
        levelCompletedPanel.gameObject.SetActive(true);
    }

    // Hide the level completed panel
    public void LevelUnCompleted()
    {
        // Hide the panel
        levelCompletedPanel.gameObject.SetActive(false);
    }

    // Display the game over panel
    public void LevelGameOver()
    {
        // Display the panel
        levelGameOverPanel.gameObject.SetActive(true);
    }

    // Hide the game over panel
    public void LevelNotGameOver()
    {
        // Hide the panel
        levelGameOverPanel.gameObject.SetActive(false);
    }

    // Update the score indicator
    public void UpdateScore(int scorePoints)
    {
        // Add the new score and update the score text indicator
        AddScore(scorePoints);
        textScore.text = score.ToString();
        UpdateBestScore();
    }

    // Set the slider levels of the game
    public void SetSliderLevels()
    {
        // Get the current level played
        int level = GameLevelManager.Instance.GetCurrentLevel();

        // Set the current and next level in the slider
        currentLevel.text = (level + 1).ToString();
        nextLevel.text = (level + 2).ToString();
    }

    // Compute the distance travelled by the ball in the level and is displayed in the slider
    public void ChangeSliderLevelAndProgress()
    {
        // Compute the distance travelled by the ball in the level
        float totalDistance = (helixTopTransform.position.y - helixGoalTransform.position.y);
        float distanceLeft = totalDistance - (ballTransform.position.y - helixGoalTransform.position.y);
        float distanceCovered = (distanceLeft / totalDistance);
        slider.value = Mathf.Lerp(slider.value, distanceCovered, 5);
    }
}
