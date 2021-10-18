using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager Instance;

    public Helix helix;

    private void Awake()
    {
        Instance = this;
    }

    public void LoadNextLevel()
    {
        helix.LoadNextStage();
    }
}
