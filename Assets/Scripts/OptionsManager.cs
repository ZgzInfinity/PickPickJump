
/*
 * ------------------------------------------
 * -- Project: Helix Jump -------------------
 * -- Author: Rubén Rodríguez Estebban ------
 * -- Date: 31/10/2021 ----------------------
 * ------------------------------------------
 */

using TMPro;
using UnityEngine;

/**
 * Scrip that controls the behaviour of the game in the options menu
 */

public class OptionsManager : MonoBehaviour
{
    // Reference to the text mesh pro with the best score info
    public TMP_Text textBestScore;

    // Reference to the options panel
    public RectTransform optionsPanel;

    // Reference to the credits panel
    public RectTransform creditsPanel;

    // Start is called before the first frame update
    private void Start()
    {
        // Set the top score info
        textBestScore.text = PlayerPrefs.GetInt("TopScore").ToString();
    }

    // Reset the current top score to zero
    public void ResetTopScore()
    {
        // Set the top score to zero and store in the text mesh pro indicator
        PlayerPrefs.SetInt("TopScore", 0);
        textBestScore.text = PlayerPrefs.GetInt("TopScore").ToString();
    }

    // Display the options panel 
    public void LoadOptionsPanel()
    {
        // Hide the credits panel and display the options panel
        creditsPanel.gameObject.SetActive(false);
        optionsPanel.gameObject.SetActive(true);
    }

    // Display the credits panel 
    public void LoadCreditsPanel()
    {
        // Hide the options panel and display the credits panel
        optionsPanel.gameObject.SetActive(false);
        creditsPanel.gameObject.SetActive(true);
    }
}
