
/*
 * ----------------------------------------
 * -- Project: Pick-Pick Jump -------------
 * -- Author: Rubén Rodríguez Estebban ----
 * -- Date: 11/11/2021 --------------------
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

    // Reference to the ball
    public CharacterSelector characterSelector;

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

        // Set the color of the trail
        SetColor();
    }

    // Set the color of the trail
    private void SetColor() {

        // Get the index of the character selector texture
        int indexCharacterSelector = characterSelector.GetIndexCharacterSelector();

        // Check the index of the texture
        switch (indexCharacterSelector)
        {
            case 0:

                // Set the trail with the color material of the ball
                trail.startColor = characterSelector.transform.GetChild(0).gameObject.
                    GetComponent<Renderer>().material.color;

                trail.endColor = characterSelector.transform.GetChild(0).gameObject.
                    GetComponent<Renderer>().material.color;
                break;
            case 1:
                // Set the color of the trail with the emoji color
                trail.startColor = new Color(0.95f, 0.85f, 0.14f);
                trail.endColor = new Color(0.95f, 0.85f, 0.14f);
                break;
            case 2:
                // Set the color of the ninja with the emoji color
                trail.startColor = new Color(0.95f, 0.54f, 0.14f);
                trail.endColor = new Color(0.95f, 0.54f, 0.14f);
                break;
            case 3:
                // Set the color of the soccer with the emoji color
                trail.startColor = new Color(0.97f, 0.27f, 0.26f);
                trail.endColor = new Color(0.97f, 0.27f, 0.26f);
                break;
        }
    }
}
