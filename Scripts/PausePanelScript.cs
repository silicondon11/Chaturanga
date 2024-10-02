using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PausePanelScript : MonoBehaviour
{
    public Button pauseButton;
    public GameObject pausePanel;
    public Button xButton;

    private bool pauseOpen;

    void Start()
    {
        pauseButton.enabled = true;
        pauseOpen = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pauseOpen)
            {
                HidePausePanel();
            }
            else
            {
                ShowPausePanel();

            }
        }
    }

    public void ShowPausePanel()
    {
        pauseButton.enabled = false;
        pausePanel.SetActive(true);
        xButton.enabled = true;
        pauseOpen = true;
        Time.timeScale = 0f;
    }

    public void HidePausePanel()
    {
        pauseButton.enabled = true;
        pausePanel.SetActive(false);
        xButton.enabled = false;
        pauseOpen = false;
        Time.timeScale = 1f;
    }

    public void GoToBattleStats()
    {
        SceneManager.LoadScene(6);
    }

    public void GoToSettings()
    {
        SceneManager.LoadScene(3);
    }

    public void GoToExit()
    {
        SceneManager.LoadScene(0);
    }
}
