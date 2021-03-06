
/*
 * ----------------------------------------
 * -- Project: Pick-Pick Jump -------------
 * -- Author: Rubén Rodríguez Estebban ----
 * -- Date: 11/11/2021 --------------------
 * ----------------------------------------
 */

using UnityEngine;
using System.Collections.Generic;

/**
 * Script that controls the creation of the different game staes
 */

public class Helix : MonoBehaviour
{
    // Force to rotate the helix structure
    private float rotationForce = 20f;

    // Distance of the level to be travelled by the ball
    private float levelDistance;

    // Position in axis y to spawn the helix platform
    private float spawnPosY;

    // Distance travelled by the ball in the level
    private float helixDistance;

    // Starting position of the helix structure
    private Vector3 startRotation;

    // Stage to be played
    private Stage stage;

    // List with the disabled parts of the helix platforms
    private List<GameObject> disabledParts = new List<GameObject>();

    // List with the not disabled parts of the helix platforms that are not death parts
    private List<GameObject> leftParts = new List<GameObject>();

    // List with the not disabled parts of the helix platforms that are death parts
    private List<GameObject> deathParts = new List<GameObject>();

    // List with all the spawned helix intermediate platforms
    public List<GameObject> spawnedHelixPlatforms = new List<GameObject>();

    // Reference to the helix top platform
    public Transform helixTopTransform;

    // Reference to the goal top platform
    public Transform helixGoalTransform;

    // Reference to the helix intermediate platform
    public GameObject helixLevelPrefab;

    // List with all the stages of the game
    public List<Stage> stages = new List<Stage>();

    // Reference to the camera
    public Camera cameraLevel;

    // Reference to the ball
    public Ball ball;

    // Reference to the ball
    public CharacterSelector characterSelector;

    // Current level
    public int currentLevel;

    // Reference to the music button
    public RectTransform musicButton;

    // Reference to the helix start platform
    public GameObject helixStartPlatform;

    // Reference to the helix start platform
    public GameObject helixGoalPlatform;

    // Awake is called one time when the scene is loaded
    private void Awake()
    {
        // Set the start rotation of the helix structure and the ditance travelled by the ball
        startRotation = transform.localEulerAngles;
        helixDistance = helixTopTransform.localPosition.y - (helixGoalTransform.localPosition.y + 0.17f);
    }

    // Update is called once per frame
    private void Update()
    {

        if (Input.touchCount > 0 && !ball.GetInGameOver())
        {
            // GET TOUCH 0
            Touch touch0 = Input.GetTouch(0);

            // APPLY ROTATION
            if (touch0.phase == TouchPhase.Moved)
            {
                Quaternion rotationY = Quaternion.Euler(0f, -touch0.deltaPosition.x * rotationForce * Time.deltaTime, 0f);
                transform.rotation *= rotationY;
            }

        }

        // Calculate the progress of the ball in the level
        UiManager.Instance.ChangeSliderLevelAndProgress();
    }

    // Reset the helix configuration
    private void Reset()
    {
        // Set the helix position
        transform.localEulerAngles = startRotation;

        // Hide the completed game panel
        UiManager.Instance.LevelUnCompleted();

        // Hide the game over panel
        UiManager.Instance.LevelNotGameOver();
    }

    // Set the color of the start and goal helix platforms
    private void SetStartGoalColorPlatforms(GameObject helixPlatform)
    {
        // Loop that iterates of the parts of the platform
        foreach (Transform child in helixPlatform.transform)
        {
            // Change the color of the platform
            child.gameObject.GetComponent<Renderer>().material.color = stage.stageLevelPartNotDeathColor;
        }
    }

    // Set the configuration of the stage
    private void SetStageConf(int stageNumber)
    {
        // Get the stage to be loaded
        stage = stages[stageNumber];

        // Set the background color of the level
        cameraLevel.backgroundColor = stage.stageBackgroundColor;

        // Set the color of the helix structure of the level
        gameObject.GetComponent<Renderer>().material.color = stage.stageHelixCylinder;

        // Get the index character selector of the ball
        int indexCharacterSelector = characterSelector.GetIndexCharacterSelector();

        characterSelector.transform.GetChild(0).gameObject.
            GetComponent<Renderer>().material.color = stage.stageBallColor;
 
        // Set the initial rotation
        startRotation = transform.localEulerAngles;

        // Set the color of the helix start platform
        SetStartGoalColorPlatforms(helixStartPlatform);

        // Set the color of the helix goal platform
        SetStartGoalColorPlatforms(helixGoalPlatform);

    }

    // Clear the helix platforms of the cylinder
    private void ClearHelixPlatforms()
    {
        // Check if there are helix platforms created
        if (spawnedHelixPlatforms.Count > 0)
        {
            // Loop to clear the helix platforms
            foreach (GameObject go in spawnedHelixPlatforms)
            {
                // Clear the helix platform
                Destroy(go);
            }
        }
    }

    // Set the parts of the helix plaforms that are active
    private void SetHelixPlatformNotDisabledParts(GameObject level, int i)
    {
        // Set the parts of the helix to be disable
        int partsToDisable = 12 - stage.levels[i].partCount;

        // Clear the list
        disabledParts.Clear();

        // Loop that clear all the parts
        while (disabledParts.Count < partsToDisable)
        {
            // Select a random part of the helix
            GameObject randomPart = level.transform.GetChild(Random.Range(0, level.transform.childCount)).gameObject;

            // Check if it's on the list in order to avoid to add it more than one
            if (!disabledParts.Contains(randomPart))
            {
                // Deactivate the helix part and add it to the list
                randomPart.SetActive(false);
                disabledParts.Add(randomPart);
            }
        }
    }

    // Set the not death parts of the helix
    private void SetHelixPlatformNotDeathParts(GameObject level)
    {
        // Clear the list
        leftParts.Clear();

        // Loop to set all the activate parts
        foreach (Transform transform in level.transform)
        {
            // Assign the correct color to the part
            transform.GetComponent<Renderer>().material.color = stage.stageLevelPartNotDeathColor;

            // Check if the part is not a deactive part
            if (transform.gameObject.activeInHierarchy)
            {
                // Assign the correct tag to control the collision and add it to the list
                transform.gameObject.tag = GameTags.HelixLevelNotDeath;
                leftParts.Add(transform.gameObject);
            }
        }
    }

    // Set the deaths parts of the helix platform
    private void SetHelixPlatformDeathParts(int i)
    {
        // Clear the list
        deathParts.Clear();

        // Loop that creates the death parts of a helix platform
        while (deathParts.Count < stage.levels[i].deathPartCount)
        {
            // Get a random part
            GameObject randomPart = leftParts[(Random.Range(0, leftParts.Count))];

            // Check if it was selected before to avoid be selected more than once
            if (!deathParts.Contains(randomPart))
            {
                // Set the color of the death part
                randomPart.gameObject.GetComponent<Renderer>().material.color = stage.stageLevelPartDeath;

                // Assign the correct tag to control the collision and add it to the list
                randomPart.gameObject.tag = GameTags.HelixLevelDeath;
                deathParts.Add(randomPart);
            }
        }
    }

    // Spawn the intermediate helix platforms
    private void SpawnHelixPlatforms()
    {
        // Calculate the total distance of the level and the position of the first platform
        levelDistance = helixDistance / stage.levels.Count;
        spawnPosY = helixTopTransform.localPosition.y;

        // Loop that creates the helix platforms
        for (int i = 0; i < stage.levels.Count; i++)
        {
            // Calculate the next position to instance the next platform
            spawnPosY -= levelDistance;

            // Create the plaform and assign its position
            GameObject level = Instantiate(helixLevelPrefab, transform);
            level.transform.localPosition = new Vector3(0, spawnPosY, 0);
            spawnedHelixPlatforms.Add(level);

            // Set the not disabled parts of the helix platform
            SetHelixPlatformNotDisabledParts(level, i);

            // Set the not disabled parts of the helix platform
            SetHelixPlatformNotDeathParts(level);

            // Set the death parts of the helix platform
            SetHelixPlatformDeathParts(i);
        }
    }

    // Get the number of levels playable in the game
    public int GetNumLevels()
    {
        // Get the number of levels
        return stages.Count;
    }

    // Load the current playable scene
    public void LoadStage(int stageNumber)
    {
        // Reset the helix structure
        Reset();

        // Set the configuration of the stage
        SetStageConf(stageNumber);

        // Clear the helix platorms
        ClearHelixPlatforms();

        // Spawn the helix platforms of the level
        SpawnHelixPlatforms();

        // Set the game as not completed
        GameLevelManager.Instance.SetGameCompleted(false);

        // Set the level as not completed
        GameLevelManager.Instance.SetLevelCompleted(false);

        // Reproduce the current soundtrack
        Sound soundtrack = AudioManager.Instance.GetCurrentSoundtrack();
        AudioManager.Instance.PlaySound(soundtrack, true);
    }

    // Load the current level
    public void LoadLevel(int currentLevel)
    {
        // Reset the helix and the ball
        Reset();
        ball.Reset();

        // Load the stage
        LoadStage(currentLevel);
    }
}
