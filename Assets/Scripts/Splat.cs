
/*
 * ------------------------------------------
 * -- Project: Helix Jump -------------------
 * -- Author: Rubén Rodríguez Estebban ------
 * -- Date: 31/10/2021 ----------------------
 * ------------------------------------------
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Script that controls the draw of the splat in the
 * helix platform when the ball collides
 */

public class Splat : MonoBehaviour
{
    // List with all the splats drawn in the level
    private List<GameObject> spawnedSplats = new List<GameObject>();

    // Static instance 
    public static Splat Instance;

    // Reference to the prefab of the splat
    public GameObject splatPrefab;

    // Reference to the ball
    public Ball ball;

    // Awake is called one time when the scene is loaded
    void Awake()
    {
        // Initialization
        Instance = this;
    }

    // Set the color of the splat with the color of the ball
    public void SetSplatColor()
    {
        // Start coroutine to set the color of the splat
        StartCoroutine(SplatSetColorCoroutine());
    }

    // Coroutine that sets the color of the splat with the color of the ball
    private IEnumerator SplatSetColorCoroutine()
    {
        // Wait an a half second and set the color of the splat with the color of the ball
        yield return new WaitForSeconds(0.1f);
        splatPrefab.GetComponent<SpriteRenderer>().color = ball.GetComponent<Renderer>().material.color;
    }

    // Draw the splat when the ball collides with the helix platform
    public void MakeSplat(GameObject helix)
    {
        // Instantiate the splat in the helix platform when the ball collides
        GameObject splat = Instantiate(splatPrefab, transform.position, Quaternion.Euler(90, 0, 0));
        splat.transform.SetParent(helix.transform);

        // Add the splat to the list of splats drawn
        spawnedSplats.Add(splat);
    }

    // Clear the splats drawn in the current level
    public void ClearSplats()
    {
        // Check if any splat has been drawn before
        if (spawnedSplats.Count > 0)
        {
            // Loop that clear the splats
            foreach (GameObject go in spawnedSplats)
            {
                // Delete the splat
                Destroy(go);
            }
        }
    }
}
