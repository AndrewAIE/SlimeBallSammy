using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Security.Cryptography;

public class GameManager : MonoBehaviour
{
    public GameObject Samuel;
    public GameObject timer;
    public GameObject DeathScreen;
    public TMP_Text DeathText;

    private int randomNumber;


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
    public void Start()
    {
        // get ranbdom number for deat text
        randomNumber = Random.Range(0, 100);
    }

    private void EndGame()
    { 
        
        DeathScreen.SetActive(true);
        Time.timeScale = 0f;
        
        // set text depending on the random number value
        if (randomNumber >= 70)
            DeathText.text = "Well this is unfortunate.";

        if (randomNumber < 70 &  randomNumber >= 60)
            DeathText.text = "We all jump, we all fall.";

        if (randomNumber < 60 & randomNumber >= 40)
            DeathText.text = "Slimeball go splat!";

        if (randomNumber < 40 & randomNumber >= 30)
            DeathText.text = "Did that hurt?";

        if (randomNumber < 30 & randomNumber >= 2)
            DeathText.text = "Death is an inevitable part of life.";

        if (randomNumber <= 1)
            DeathText.text = "you died, but im never gonna give you up...";


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
