using UnityEngine;
using TMPro;
using System.Collections;

/**
 * Script that controls the transition between the scenes
 */

public class MenuManager : MonoBehaviour
{
    public void ChangeScene(string sceneToLoad)
    {
        GameSceneManager.Instance.ChangeScene(sceneToLoad);
    }

    public void Quit()
    {
        Application.Quit(); 
    }
}
