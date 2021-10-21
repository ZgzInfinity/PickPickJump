using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class Level
{
    [Range(1, 11)]
    public int partCount = 11;

    [Range(0, 11)]
    public int deathPartCount = 1;
}

[CreateAssetMenu(fileName = "New Stage")]
public class Stage : ScriptableObject
{
    public Color stageBackgroundColor = Color.white;
    public Color stageLevelPartNotDeathColor = Color.white;
    public Color stageLevelPartDeath = Color.white;
    public Color stageBallColor = Color.white;
    public Color stageHelixCylinder = Color.white;

    public List<Level> levels = new List<Level>();

}
