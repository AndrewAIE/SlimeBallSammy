using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTimer : MonoBehaviour
{
    private float timer = 10;
    public TMP_Text Highscore;

    void Update()
    {
        if (timer < 999999)
        {
            timer += Time.deltaTime;
            Highscore.text = timer.ToString();

        }
    }
}


