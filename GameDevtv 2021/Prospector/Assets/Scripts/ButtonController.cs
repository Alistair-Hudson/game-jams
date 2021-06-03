using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    LevelController levelController = null;
    // Start is called before the first frame update
    void Start()
    {
        levelController = FindObjectOfType<LevelController>();
    }


    public void StartGame()
    {
        levelController.StartGame();
    }

    public void MainMenu()
    {
        levelController.MainMenu();
    }

    public void Credits()
    {
        levelController.Credits();
    }

    public void HowToPlay()
    {
        levelController.HowToPlay();
    }

    public void QuitGame()
    {
        levelController.QuitGame();
    }
}
