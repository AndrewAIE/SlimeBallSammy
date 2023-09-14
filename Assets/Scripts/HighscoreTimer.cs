using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTimer : MonoBehaviour
{
    private float timer = 0;
    public TMP_Text Highscore;

    public bool timing = true;

    void Update()
    {
        if (timing)
        {
            timer += Time.deltaTime;

            Highscore.text = string.Format("{0:00}", timer);
           // Highscore.text = timer.ToString();

        }
    }

    public void PauseTimer()
    {
        timing = false;
    }

}


