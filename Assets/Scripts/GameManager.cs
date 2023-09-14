using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Samuel;
    public GameObject timer;
    public GameObject DeathScreen;

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
            StartCoroutine(EndGame());
        }

    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2.5f);
        DeathScreen.SetActive(true);        
    }
}
