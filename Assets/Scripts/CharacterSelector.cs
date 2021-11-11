
/*
 * ----------------------------------------
 * -- Project: Pick-Pick Jump -------------
 * -- Author: Rubén Rodríguez Estebban ----
 * -- Date: 11/11/2021 --------------------
 * ----------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Script that controls the texture of the ball
 */

public class CharacterSelector : MonoBehaviour
{

    // Static reference
    public static CharacterSelector Instance;

    // Vector with the textures of the bball
    public GameObject[] characters;

    // Index of the texture to be displayed
    private int indexCharacterSelector;

    // Reference to the button sound
    public Sound buttonSound;

    // Awake is called one time when the scene is loaded
    private void Awake()
    {
        // Initialization
        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Set a default value to the ball texture
        indexCharacterSelector = 0;
        foreach (GameObject character in characters)
        {
            // Assign layer to the textures and deactive them
            character.SetActive(false);
            character.layer = 8;
        }

        // Active the texture to be displayed
        characters[indexCharacterSelector].SetActive(true);
    }

    // Set the texture of the ball depending on the index
    public void SetIndexCharacterSelector(int newCharacterIndex)
    {
        // Reproduce the sound of victory
        AudioManager.Instance.PlaySound(buttonSound, false);

        // Hide the current texture and display the new one
        characters[indexCharacterSelector].SetActive(false);
        characters[newCharacterIndex].SetActive(true);
        indexCharacterSelector = newCharacterIndex;

        // Set the colors of the splat and trail according to the texture
        Splat.Instance.SetSplatColor();
        Trail.Instance.SetTrailColor();
    }

    // Get the index of the texture to be displayed
    public int GetIndexCharacterSelector()
    {
        return indexCharacterSelector;
    } 
}
