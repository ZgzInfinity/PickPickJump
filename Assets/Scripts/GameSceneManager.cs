
/*
 * ----------------------------------------
 * -- Project: Pick-Pick Jump -------------
 * -- Author: Rubén Rodríguez Estebban ----
 * -- Date: 31/10/2021 --------------------
 * ----------------------------------------
 */

using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Script that controls the change of a scene
 */

public class GameSceneManager : MonoBehaviour
{
    // Static instance
    public static GameSceneManager Instance;

    // Awake is called one time when the scene is loaded
    private void Awake()
    {
        // Initialization
        Instance = this;
    }

    // Change the scene to a new one
    public void ChangeScene(string sceneToLoad)
    {
        // Change the scene
        SceneManager.LoadScene(sceneToLoad);
    }

    // Get the name of the current scene
    public string GetSceneName()
    {
        // Get the name of the current scene
        return SceneManager.GetActiveScene().name;
    }
}
