
/*
 * ------------------------------------------
 * -- Project: Helix Jump -------------------
 * -- Author: Rubén Rodríguez Estebban ------
 * -- Date: 31/10/2021 ----------------------
 * ------------------------------------------
 */

using UnityEngine;

/**
 * Script that controls the transition between the scenes
 */

public class MenuManager : MonoBehaviour
{
    // Change the scene to a new one
    public void ChangeScene(string sceneToLoad)
    {
        // Change the current scene
        GameSceneManager.Instance.ChangeScene(sceneToLoad);
    }

    // Quit the application
    public void Quit()
    {
        // Quit
        Application.Quit(); 
    }
}
