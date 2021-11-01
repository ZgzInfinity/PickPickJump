
/*
 * ------------------------------------------
 * -- Project: Helix Jump -------------------
 * -- Author: Rubén Rodríguez Estebban ------
 * -- Date: 31/10/2021 ----------------------
 * ------------------------------------------
 */

using System;
using UnityEngine;
using System.Collections.Generic;

/**
 * Script that controls the representation of the stages like a scriptable object
 */

[Serializable]
public class Level
{
    // Number of parts that has an helix platform
    [Range(1, 11)]
    public int partCount = 11;

    // Number of death parts that has an helix platform
    [Range(0, 11)]
    public int deathPartCount = 0;
}

[CreateAssetMenu(menuName = "HelixJump/Stage", fileName = "Stage.asset")]
public class Stage : ScriptableObject
{
    // Color of the stage's background
    public Color stageBackgroundColor = Color.white;

    // Color of the helix platform not death parts
    public Color stageLevelPartNotDeathColor = Color.white;

    // Color of the helix platform death parts
    public Color stageLevelPartDeath = Color.white;

    // Color of the ball
    public Color stageBallColor = Color.white;

    // Color of the helix cylinder
    public Color stageHelixCylinder = Color.white;

    // List with all the stages of the game
    public List<Level> levels = new List<Level>();

}
