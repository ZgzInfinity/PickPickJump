
/*
 * ----------------------------------------
 * -- Project: Pick-Pick Jump -------------
 * -- Author: Rubén Rodríguez Estebban ----
 * -- Date: 31/10/2021 --------------------
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

    // Current level to be played
    private int currentLevel;

    // Static instance
    public static GameLevelManager Instance;

    // Reference to the helix structure
    public Helix helix;

    // Reference to the ball
    public Ball ball;

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
        currentLevel = 0;
        LoadLevel();

        // Set the score to zero
        UiManager.Instance.ResetScore();
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

    // Load the current level
    public void LoadLevel()
    {
        // Check if the ball is in game over
        if (ball.GetInGameOver())
        {
            // Set the score to zero
            UiManager.Instance.ResetScore();
        }

        // Load the level
        helix.LoadLevel(currentLevel);
    }

    // Increment the level to access to the next one
    public bool IncrementLevel()
    {
        // Check if all the levels have been passed
        bool nextLevel = (currentLevel < helix.GetNumLevels()) ? true : false;

        // Increment the level because there are levels to complete
        if (nextLevel){
            currentLevel++;
        }
        return nextLevel;
    }

    // Update is called once per frame
    public void Update()
    {
        // Check if the level has been completed
        if (levelCompleted)
        {
            // Check if the screen has been touched
            if (Input.GetMouseButton(0))
            {
                // Check if there is next level
                bool nextLevel = IncrementLevel();
                if (nextLevel)
                {
                    // Reset the ball and loads the next level
                    ball.Reset();
                    LoadLevel();
                }
                else
                {
                    // Change the scene to the main menu one
                    GameSceneManager.Instance.ChangeScene(GameScenes.Intro);
                }
            }
        }
    }
}
