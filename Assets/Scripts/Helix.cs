using UnityEngine;
using System.Collections.Generic;


public class Helix : MonoBehaviour
{
    private Vector2 lastPosition;

    private Vector3 startRotation;

    public Transform helixTopTransform;
    public Transform helixGoalTransform;

    public GameObject helixLevelPrefab;

    public List<Stage> allStages = new List<Stage>();

    public float helixDistance;

    public List<GameObject> spawnedLevels = new List<GameObject>();

    public Camera cameraLevel;

    public Ball ball;

    public int currentLevel;

    private void Awake()
    {
        startRotation = transform.localEulerAngles;
        helixDistance = helixTopTransform.localPosition.y - (helixGoalTransform.localPosition.y + 0.17f);
        currentLevel = 0;
    }

    private void Start()
    {
        LoadStage(currentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 currentTapPosition = Input.mousePosition;
            float distance = (lastPosition.x - currentTapPosition.x);
            lastPosition = currentTapPosition;
            transform.Rotate(Vector3.up * distance);
        }
    }

    public void LoadStage(int stageNumber)
    {
        Stage stage = allStages[Mathf.Clamp(stageNumber, 0, allStages.Count - 1)];

        cameraLevel.backgroundColor = stage.stageBackgroundColor;
        ball.GetComponent<Renderer>().material.color = stage.stageBallColor;
        startRotation = transform.localEulerAngles;

        foreach (GameObject go in spawnedLevels)
        {
            Destroy(go);
        }

        float levelDistance = helixDistance / stage.levels.Count;
        float spawnPosY = helixTopTransform.localPosition.y;

        for (int i = 0; i < stage.levels.Count; i++)
        {
            spawnPosY -= levelDistance;
            GameObject level = Instantiate(helixLevelPrefab, transform);
            level.transform.localPosition = new Vector3(0, spawnPosY, 0);
            spawnedLevels.Add(level);

            int partsToDisable = 12 - stage.levels[i].partCount;

            List<GameObject> disabledParts = new List<GameObject>();

            while (disabledParts.Count < partsToDisable)
            {
                GameObject randomPart = level.transform.GetChild(Random.Range(0, level.transform.childCount)).gameObject;
                if (!disabledParts.Contains(randomPart))
                {
                    randomPart.SetActive(false);
                    disabledParts.Add(randomPart);
                }
            }

            List<GameObject> leftParts = new List<GameObject>();

            foreach (Transform transform in level.transform)
            {
                transform.GetComponent<Renderer>().material.color = stage.stageLevelPartNotDeathColor;
                
                if (transform.gameObject.activeInHierarchy)
                {
                    transform.gameObject.tag = GameTags.HelixLevel;
                    leftParts.Add(transform.gameObject);
                }
            }

            List<GameObject> deathParts = new List<GameObject>();

            while (deathParts.Count < stage.levels[i].deathPartCount)
            {
                GameObject randomPart = leftParts[(Random.Range(0, leftParts.Count))];

                if (!deathParts.Contains(randomPart))
                {
                    randomPart.gameObject.AddComponent<DeathPart>();
                    randomPart.gameObject.GetComponent<Renderer>().material.color = stage.stageLevelPartDeath;
                    deathParts.Add(randomPart);
                }
            }
        }
    }

    public void LoadNextStage()
    {
        if (currentLevel < allStages.Count)
        {
            currentLevel++;
            LoadStage(currentLevel);
        }
    }
}
