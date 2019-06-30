using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject levelsPanel;
    public GameObject instructionsPanel;

    private void Start()
    {
        if (!levelsPanel)
        {
            Debug.LogWarning("Levels Panel is missing");
        }
        else
        {
            levelsPanel.SetActive(false);
        }

        if (!instructionsPanel)
        {
            Debug.LogWarning("Instructions Panel is missing");
        }
        else
        {
            instructionsPanel.SetActive(false);
        }
    }

    public void OpenLevelPanel()
    {
        levelsPanel.SetActive(true);
    }

    public void CloseLevelPanel()
    {
        levelsPanel.SetActive(false);
    }

    public void OpenInstructions()
    {
        instructionsPanel.SetActive(true);
    }

    public void CloseInstructions()
    {
        instructionsPanel.SetActive(false);
    }

}
