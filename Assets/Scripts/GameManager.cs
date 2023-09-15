using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Samuel;
    public GameObject timer;
    public GameObject DeathScreen;

    public ButtonEvents ButtonEvents;

    public GameObject pauseScreen;

    private HighscoreTimer HighscoreTimer;

    public void Awake()
    {
        HighscoreTimer = timer.GetComponent<HighscoreTimer>();        
    }

    public void Update()
    {
        if(!Samuel)
        {
            HighscoreTimer.PauseTimer();
            EndGame();
        }
    }

    private void EndGame()
    {        
        DeathScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public bool ClickerActive()
    {
        if(pauseScreen.gameObject.activeSelf || ButtonEvents.IsOverButton())
        {
            return false;
        }
        return true;
    }
}
