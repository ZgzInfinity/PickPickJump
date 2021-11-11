
/*
 * ----------------------------------------
 * -- Project: Pick-Pick Jump -------------
 * -- Author: Rubén Rodríguez Estebban ----
 * -- Date: 11/11/2021 --------------------
 * ----------------------------------------
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Script that controls the transition between the scenes
 */

public class MenuManager : MonoBehaviour
{

    // Control if the game is in pause
    private bool inPauseMode;

    // Reference to the button sound
    public Sound buttonSound;

    // List of the game panel menu
    public List<GameObject> buttons = new List<GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        // Game not paused by default
        inPauseMode = false;
    }

    // Change the scene
    private IEnumerator ChangeSceneAfterButtonSoundCoRoutine(string sceneToLoad)
    {
        // Wait until the sound has finished and change the scene
        yield return new WaitForSeconds(buttonSound.clip.length);
        ChangeScene(sceneToLoad);
    }

    // Close the application
    private IEnumerator QuitApplicationAfterButtonSoundCoRoutine()
    {
        // Wait until the sound has finished and change the scene
        yield return new WaitForSeconds(buttonSound.clip.length);
        Application.Quit();
    }

    // Load level
    private IEnumerator LoadLevelAfterButtonSoundCoroutine()
    {
        // Wait until the sound has finished and load level
        yield return new WaitForSeconds(buttonSound.clip.length);
        GameLevelManager.Instance.LoadLevel();
    }

    // Pause the game when a button is pressed
    public void PauseGame()
    {
        // Pause the game and change the scene after it has finished
        AudioManager.Instance.PlaySound(buttonSound, false);

        // Check if the game is in pause
        if (!inPauseMode)
        {
            Sound currentSoundtrack = AudioManager.Instance.GetCurrentSoundtrack();
            AudioManager.Instance.StopSound(currentSoundtrack, true);

            // Pause the game
            Time.timeScale = 0f;
            inPauseMode = true;

            // Deactivate all the buttons except the pause button
            foreach (GameObject button in buttons)
            {
                button.gameObject.SetActive(false);
            }
        }
        else
        {
            Sound currentSoundtrack = AudioManager.Instance.GetCurrentSoundtrack();
            AudioManager.Instance.PlaySound(currentSoundtrack, true);

            // Continue the game
            Time.timeScale = 1f;
            inPauseMode = false;

            // Activate all the buttons 
            foreach (GameObject button in buttons)
            {
                button.gameObject.SetActive(true);
            }
        }
    }

    // Changes of scene when a button is pressed
    public void ChangeSceneAfterButtonSound(string sceneToLoad)
    {
        // Reproduce the sound and change the scene after it has finished
        AudioManager.Instance.PlaySound(buttonSound, false);
        StartCoroutine(ChangeSceneAfterButtonSoundCoRoutine(sceneToLoad));
    }

    // Close the application when the button is pressed
    public void QuitApplicationAfterButtonSound()
    {
        // Reproduce the sound and change and close the application
        AudioManager.Instance.PlaySound(buttonSound, false);
        StartCoroutine(QuitApplicationAfterButtonSoundCoRoutine());
    }


    // Load level when the button is pressed
    public void LoadLevelAfterButtonSound()
    {
        // Reproduce the sound and load the level
        AudioManager.Instance.PlaySound(buttonSound, false);
        StartCoroutine(LoadLevelAfterButtonSoundCoroutine());
    }

    // Change the scene to a new one
    public void ChangeScene(string sceneToLoad)
    {
        // Change the current scene
        GameSceneManager.Instance.ChangeScene(sceneToLoad);
    }
}
