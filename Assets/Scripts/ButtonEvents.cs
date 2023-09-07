using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ButtonEvents : MonoBehaviour
{

    private string _activeSceneName;
    static string Active;
    static string PreviousScene;

    public GameObject _pauseMenu;

    public GameObject [] GameFunctions;
    public GameObject [] SpawnerObjects;

    GameObject Transform_list;

    public Canvas GameplayCanvas;

    //private Canvas _canvasGame;
    //private Canvas _canvasPause;

    // fix this later
    public void Start()
    {

       
        // find active scene
        Active = SceneManager.GetActiveScene().name;
        

       //if (Active == "OptionsMenu")
       //{
       //    DontDestroyOnLoad(this);
       //}
       //// || pines dont work?
       //if (Active == "MainMenu")
       //{
       //    DontDestroyOnLoad(this);
       //}

        // Managment for the Game Screen
        

        //  GameplayCanvas = GameObject.FindObjectOfType<Canvas>();
        // 
        //  if (GameplayCanvas != null)
        //  {
        //      _pauseMenu = GameObject.Find("PauseMenu");
        // 
        //     _pauseMenu.SetActive(false);
        //  }





    }

    void FindSomething(GameObject Menu, string MenuName)
    {
        Menu = GameObject.Find(MenuName);
    }

    public void GoToPauseMenu()
    {
        //FindSomething(_pauseMenu, "PauseMenu");
        _pauseMenu.SetActive(true);

        Time.timeScale = 0f;

    }

    public void GoToOptions()
    {
         PreviousScene = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene("OptionsMenu");
        Time.timeScale = 1f;
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("PROTOTYPE1");
        Start();
    }

    public void BackToPlay()
    {
        _pauseMenu.SetActive(false);
        for (int i = 0; i < GameFunctions.Length; i++)
        {
            GameFunctions[i].SetActive(true);
        }
        Time.timeScale = 1f;


    }

    public void BackFromOptions()
    {
        //string _prevName;
        //_prevName = PreviousScene.name;
        SceneManager.LoadScene(PreviousScene);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

}
