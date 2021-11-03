
/*
 * ----------------------------------------
 * -- Project: Pick-Pick Jump -------------
 * -- Author: Rubén Rodríguez Estebban ----
 * -- Date: 31/10/2021 --------------------
 * ----------------------------------------
 */

using UnityEngine;
using System.Collections;

/**
 * Script that controls the transition between the scenes
 */

public class MenuManager : MonoBehaviour
{
    // Reference to the button sound
    public Sound buttonSound;

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
