
/*
 * ----------------------------------------
 * -- Project: Pick-Pick Jump -------------
 * -- Author: Rubén Rodríguez Estebban ----
 * -- Date: 31/10/2021 --------------------
 * ----------------------------------------
 */

using TMPro;
using UnityEngine;
using System.Collections;

/**
 * Scrip that controls the behaviour of the game in the options menu
 */

public class OptionsManager : MonoBehaviour
{
    // Reference to the button sound
    public Sound buttonSound;

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
        // Play the button sound
        AudioManager.Instance.PlaySound(buttonSound, false);

        // Set the top score to zero and store in the text mesh pro indicator
        PlayerPrefs.SetInt("TopScore", 0);
        textBestScore.text = PlayerPrefs.GetInt("TopScore").ToString();
    }

    // Display the options panel 
    private IEnumerator LoadOptionsPanelCoroutine()
    {
        // Wait until the sound has finished and changes the panel
        yield return new WaitForSeconds(buttonSound.clip.length);

        // Hide the credits panel and display the options panel
        creditsPanel.gameObject.SetActive(false);
        optionsPanel.gameObject.SetActive(true);
    }

    // Display the credits panel 
    private IEnumerator LoadCreditsPanelCoroutine()
    {
        // Wait until the sound has finished and changes the panel
        yield return new WaitForSeconds(buttonSound.clip.length);

        // Hide the options panel and display the credits panel
        optionsPanel.gameObject.SetActive(false);
        creditsPanel.gameObject.SetActive(true);
    }

    // Display the options panel 
    public void LoadOptionsPanel()
    {
        // Coroutine that displays the options panel
        AudioManager.Instance.PlaySound(buttonSound, false);
        StartCoroutine(LoadOptionsPanelCoroutine());
    }

    // Display the credits panel 
    public void LoadCreditsPanel()
    {
        // Coroutine that displays the credits panel
        AudioManager.Instance.PlaySound(buttonSound, false);
        StartCoroutine(LoadCreditsPanelCoroutine());
    }
}
