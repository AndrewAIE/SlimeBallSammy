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

    // fix this later
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



    public void QuitGame()
    {
        Application.Quit();
    }

    public void  HoverPauseEnter()
    {
        m_overButton = true;
    }

    public void HoverPauseExit()
    {
        m_overButton = false;
    }

    public bool IsOverButton()
    {
        return m_overButton;
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
        SceneManager.LoadScene("PROTOTYPE1");
        Time.timeScale = 1f;
        Start();
    }

    public void BackToPlay()
    {
        _pauseMenu.SetActive(false);
       //for (int i = 0; i < GameFunctions.Length; i++)
       //{
       //    GameFunctions[i].SetActive(true);
       //}
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
