using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    // Static instance
    public static GameSceneManager Instance;

    // Constructor
    private void Awake()
    {
        Instance = this;
    }

    public void ChangeScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
