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
    public GameObject SAM;

    public Canvas GameplayCanvas;

    public GameObject deathScene;

    public bool m_overButton = false;

    //private Canvas _canvasGame;
    //private Canvas _canvasPause;

    /// <summary>
    /// This fuction sets screen resolution baced on the users platform, and get the current scene on Start.
    /// </summary>
    public void Start()
    {
        if(Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Screen.SetResolution(346, 615, false);
        }
        
        if(Application.platform == RuntimePlatform.Android)
        {
            Debug.Log(Screen.currentResolution);
        }

        Time.timeScale = 1f;

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

    /// <summary>
    /// checks every fram is esc was presed and if the requirements met then opens the pause menu
    /// </summary>

    private void Update()
    {
        if(Active == "PROTOTYPE1" && !deathScene.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(_pauseMenu.gameObject.activeSelf)
                {
                    BackToPlay();
                }
                else
                {
                    GoToPauseMenu();
                }
            }
        }

    }


    /// <summary>
    /// Quits game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

 /// <summary>
 /// finds a an object using a its name and makes it as a GameObject.
 /// (used for finding menus)
 /// </summary>
 /// <param name="Menu"></param>
 /// <param name="MenuName"></param>

    void FindSomething(GameObject Menu, string MenuName)
    {
        Menu = GameObject.Find(MenuName);
    }

    /// <summary>
    /// change scene to pause menu
    /// </summary>
    public void GoToPauseMenu()
    {
        //FindSomething(_pauseMenu, "PauseMenu");
        _pauseMenu.SetActive(true);

        Time.timeScale = 0f;

    }
    /// <summary>
    /// change scene to block guide
    /// </summary>

    public void GoToGuide()
    {
        // dont destroy on load so then the music continues

         PreviousScene = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene("BlockGuide");
        Time.timeScale = 1f;
    }

    /// <summary>
    /// change scene to the game scene (prototype1)
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene("PROTOTYPE1");
        Time.timeScale = 1f;
        Start();
    }

    /// <summary>
    /// de-activates pause panel and un-pauses game.
    /// </summary>

    public void BackToPlay()
    {
        _pauseMenu.SetActive(false);
       //for (int i = 0; i < GameFunctions.Length; i++)
       //{
       //    GameFunctions[i].SetActive(true);
       //}
        Time.timeScale = 1f;
    }

    /// <summary>
    /// goes back to previous scene
    /// 
    /// </summary>

    public void BackFromGuide()
    {
       
        SceneManager.LoadScene(PreviousScene);
    }

    /// <summary>
    /// change scene to main menu
    /// </summary>

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

}
