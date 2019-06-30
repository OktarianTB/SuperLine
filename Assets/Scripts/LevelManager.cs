using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
 
    public void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int numberOfScenes = SceneManager.sceneCountInBuildSettings;
        
        if(currentIndex + 1 >= numberOfScenes){
            LoadMenu();
            return;
        }

        SceneManager.LoadScene(currentIndex + 1);
    }

    public void LoadSameLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevel(int level)
    {
        int numberOfScenes = SceneManager.sceneCountInBuildSettings;

        if(level < numberOfScenes)
        {
            SceneManager.LoadScene(level);
        }
    }

}
