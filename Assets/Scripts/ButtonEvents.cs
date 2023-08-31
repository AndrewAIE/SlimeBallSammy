using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonEvents : MonoBehaviour
{

    private string _activeSceneName;
    static string Active;

    private GameObject _activeGameCanvas;
    private GameObject _pauseGameCanvas;

    static string PreviousScene;

    // fix this later
    private void Start()
    {
        DontDestroyOnLoad(this);
        // find active scene
        Active = SceneManager.GetActiveScene().name;
        _activeSceneName = Active;

        // if active scene is gamplay then find the UI canvas
        if (Active == "TESTGamePlayUI")
        {
            _activeGameCanvas = GameObject.FindGameObjectWithTag("ActiveGameUI");
            _pauseGameCanvas = GameObject.FindGameObjectWithTag("PauseMenu");

            _pauseGameCanvas.SetActive(false);
        }
    }

    public void GoToPauseMenu()
    {
        _activeGameCanvas.SetActive(false);
        _pauseGameCanvas.SetActive(true);

        // when gameloop script is finished implement the a pause the the funcvtion


        // rember on the " back to game menu to set each canvas bac to defult
    }

    public void GoToOptions()
    {
         PreviousScene = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene("OptionsMenu");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("TESTGameplayUI");
    }

    public void BackToPlay()
    {
        // when game loop finsiedh impliment code where the gameplay 'unpauses'
        // so then the game isnt frozen whgen the UIgoes away

        _activeGameCanvas.SetActive(true);
        _pauseGameCanvas.SetActive(false);
    }

    public void BackFromOptions()
    {
        //string _prevName;
        //_prevName = PreviousScene.name;
        SceneManager.LoadScene(PreviousScene);

    }

}
