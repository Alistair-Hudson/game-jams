using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    int level_number= 1;

    private void Awake()
    {
        LevelController[] levelControllers = FindObjectsOfType<LevelController>();
        if (1 < levelControllers.Length)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public int GetLevelNumber()
    {
        return level_number;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
    }

    public void StartGame()
    {
        level_number = 1;
        SceneManager.LoadScene(1);
    }

    public void NextLevel()
    {
        ++level_number;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        SceneManager.LoadScene(2);
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene(3);
    }
}
