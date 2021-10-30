using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager Instance;

    public Helix helix;

    public int currentLevel;

    public bool levelCompleted;

    public Ball ball;

    private void Awake()
    { 
        Instance = this;
        currentLevel = 0;
    }

    public void Start()
    {
        LoadLevel();
    }

    public void LoadLevel()
    {
        helix.LoadLevel(currentLevel);
    }

    public bool IncrementLevel()
    {
        if (currentLevel < helix.GetNumLevels()){
            currentLevel++;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Update()
    {
        if (levelCompleted)
        {
            if (Input.GetMouseButton(0))
            {
                if (IncrementLevel())
                {
                    ball.Reset();
                    LoadLevel();
                }
                else
                {
                    GameSceneManager.Instance.ChangeScene("Intro");
                }
            }
        }
    }
}
