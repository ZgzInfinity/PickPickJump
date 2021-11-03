
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
 * Script that controls the draw of the trail that follows the ball
 */

public class Trail : MonoBehaviour
{
    // Static instance 
    public static Trail Instance;

    // Reference to the ball
    public Ball ball;

    // Reference to the trail of the ball
    public TrailRenderer trail;

    // Awake is called one time when the scene is loaded
    void Awake()
    {
        // Initialization
        Instance = this;
    }


    // Set the color of the trail with the color of the ball
    public void SetTrailColor()
    {
        // Start coroutine to set the color of the trail
        StartCoroutine(TrailSetColorCoroutine());
    }

    // Coroutine that sets the color of the trail with the color of the ball
    private IEnumerator TrailSetColorCoroutine()
    {
        // Wait an a half second and set the start and end colors of the trail with the color of the ball
        yield return new WaitForSeconds(0.05f);
        Color ballColor = ball.GetComponent<Renderer>().material.color;
        trail.startColor = ballColor;
        trail.endColor = ballColor;
    }
}
