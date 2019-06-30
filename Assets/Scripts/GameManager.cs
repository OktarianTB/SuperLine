using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pausePanel;
    public bool playerHasWon = false;
    public int numberOfStars;

    bool gameIsPaused = false;
    float timeToReset = 1.2f;

    GameObject pausePanelInstance;
    LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        Time.timeScale = 1f;

        if (!levelManager)
        {
            Debug.LogWarning("Level Manager can't be found by Game Manager");
        }

        if (!pausePanel)
        {
            Debug.LogWarning("Pause Panel is missing");
        }
        else
        {
            pausePanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ManagePause();
        }
    }

    public void ManageVictory()
    {
        if(numberOfStars <= 0)
        {
            playerHasWon = true;
            print("Win");
        }
    }

    public void PlayerDeath()
    {
        if (!playerHasWon)
        {
            StartCoroutine(StartAgain());
        }
        
    }

    IEnumerator StartAgain()
    {
        yield return new WaitForSeconds(timeToReset);

        if (levelManager)
        {
            levelManager.LoadSameLevel();
        }
    }

    public void ManagePause()
    {
        gameIsPaused = !gameIsPaused;

        Time.timeScale = gameIsPaused ? 0f : 1f;

        if (gameIsPaused)
        {
            pausePanel.SetActive(true);
        }
        else
        {
            pausePanel.SetActive(false);
        }

    }

}
