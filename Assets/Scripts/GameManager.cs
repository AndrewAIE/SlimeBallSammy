using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Samuel;
    public GameObject timer;

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

        IEnumerator EndGame()
        {
            yield return new WaitForSeconds(6f);
            SceneManager.LoadScene("MainMenu");


        }
    }

}
