using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTimer : MonoBehaviour
{
    private float timer = 0;
    public TMP_Text Highscore;

    private bool timing = false;

    /// <summary>
    ///  starts timer
    /// </summary>
    private void Start()
    {
        StartCoroutine(TutorialWait());
    }
    
    /// <summary>
    /// updates timer every second so it can cound up
    /// </summary>
    void Update()
    {
        if (timing)
        {
            timer += Time.deltaTime;

            Highscore.text = string.Format("{0:00}", timer);
            //Highscore.text = timer.ToString();

        }
    }
    /// <summary>
    /// waits for game to start moving before timer is able to count
    /// </summary>
    /// <returns></returns>
    IEnumerator TutorialWait()
    {
        yield return new WaitForSeconds(3);
        timing = true;

    }
    /// <summary>
    /// pauses the timer
    /// </summary>
    public void PauseTimer()
    {
        timing = false;
    }

}


