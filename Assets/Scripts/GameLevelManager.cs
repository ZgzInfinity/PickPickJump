
/*
 * ----------------------------------------
 * -- Project: Pick-Pick Jump -------------
 * -- Author: Rubén Rodríguez Estebban ----
 * -- Date: 11/11/2021 --------------------
 * ----------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/**
 * Script that controls the playable levels of the game
 */

public class GameLevelManager : MonoBehaviour
{
    // Controls if a level has been completed
    private bool levelCompleted;

    // Controls if the game has been completed
    private bool gameCompleted;

    // Current level to be played
    private int currentLevel;

    // Static instance
    public static GameLevelManager Instance;

    // Reference to the helix structure
    public Helix helix;

    // Reference to the ball
    public Ball ball;

    // Reference to button sound
    public Sound buttonSound;

    // Reference to game completed sound
    public Sound gameFinished;

    // Reference to the camera
    public Camera cameraLevel;

    // Awake is called one time when the scene is loaded
    private void Awake()
    { 
        // Initialization
        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Set the initial level and loads it
        currentLevel = PlayerPrefs.GetInt("Level");

        gameCompleted = false;
        LoadLevel();

        // Set the score to zero
        UiManager.Instance.ResetScore();

        // Set the game panel as incompleted
        UiManager.Instance.GameUnCompleted();
    }


    // Restart the game
    private IEnumerator RestartGameAfterGameCompletedSoundCoroutine()
    {
        // Wait until the sound has finished and restart the game
        yield return new WaitForSeconds(gameFinished.clip.length * 2);
        GameSceneManager.Instance.ChangeScene(GameScenes.Intro);
    }

    // Restart the game
    private IEnumerator LoadNextLevelAfterOneSecondCoroutine()
    {
        // Wait until the sound has finished and restart the game
        yield return new WaitForSeconds(1f);

        // Reset the ball and loads the next level
        ball.Reset();
        LoadLevel();
    }

    // Load next level after button sound finish
    private void LoadNextLevelAfterOneSeconds()
    {
        // Coroutine that load next level after the button sound has finished
        StartCoroutine(LoadNextLevelAfterOneSecondCoroutine());
    }

    // Load level
    private void RestartGameAfterGameCompletedSound()
    {
        StartCoroutine(RestartGameAfterGameCompletedSoundCoroutine());
    }

    // Update is called once per frame
    private void Update()
    {
        // Check if the level has been completed
        if (gameCompleted)
        {
            // Establish camera background as black
            cameraLevel.backgroundColor = Color.black;

            // Hide the helix and the ball
            helix.gameObject.SetActive(false);

            // Destroy the parent of the ball
            cameraLevel.transform.parent = null;

            // Hide the ball and the ball
            ball.gameObject.SetActive(false);

            // Store the new level to play
            PlayerPrefs.SetInt("Level", 0);

            // Change the scene to the main menu one
            RestartGameAfterGameCompletedSound();
        }
        else if (levelCompleted)
        {
            // Check if there is next level
            bool nextLevel = IncrementLevel();
            if (nextLevel)
            {
                // Load the next level
                LoadNextLevelAfterOneSeconds();
            }

            // Only loads the level one time
            levelCompleted = false;
        }
    }

    // Get the current level
    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    // Set the current level as completed or not
    public void SetLevelCompleted(bool completed)
    {
        levelCompleted = completed;
    }


    // Set the game as completed
    public void SetGameCompleted(bool completed)
    {
        gameCompleted = completed;
    }


    // Load the current level
    public void LoadLevel()
    {
        // Check if the ball is in game over
        if (ball.GetInGameOver())
        {
            // Set the score to zero
            UiManager.Instance.ResetScore();
        }
        else
        {
            // Set the level to play
            UiManager.Instance.SetLevel();
        }

        // Load the level
        helix.LoadLevel(currentLevel);
    }

    // Increment the level to access to the next one
    public bool IncrementLevel()
    {
        // Check if all the levels have been passed
        bool nextLevel = (currentLevel < helix.GetNumLevels() - 1) ? true : false;

        // Increment the level because there are levels to complete
        if (nextLevel){

            // Increment the level
            currentLevel++;

            // Store the new level to play
            PlayerPrefs.SetInt("Level", currentLevel);
        }
        return nextLevel;
    }
}
