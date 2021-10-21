using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager Instance;

    public Helix helix;

    public int currentLevel;

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
}
