using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{

    public TMP_Text textBestScore;

    public RectTransform optionsPanel;

    public RectTransform creditsPanel;

    private void Start()
    {
        textBestScore.text = PlayerPrefs.GetInt("TopScore").ToString();
    }

    public void ResetTopScore()
    {
        PlayerPrefs.SetInt("TopScore", 0);
        textBestScore.text = PlayerPrefs.GetInt("TopScore").ToString();
    }

    public void loadOptionsPanel()
    {
        creditsPanel.gameObject.SetActive(false);
        optionsPanel.gameObject.SetActive(true);
    }

    public void loadCreditsPanel()
    {
        optionsPanel.gameObject.SetActive(false);
        creditsPanel.gameObject.SetActive(true);
    }
}
