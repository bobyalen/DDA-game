using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using static DDAControl;

public class finalScreen : MonoBehaviour
{
    public static finalScreen Instance { get; private set; }
    public TMP_Text final;
    public TMP_Text time;
    void Start()
    {
        int score = PlayerPrefs.GetInt("Score"); ;
        int finalTime = PlayerPrefs.GetInt("Time");
        final.text = "Game Over final Score: " + score.ToString();
        time.text = "Time survivered: " + finalTime.ToString();
    }

    // Update is called once per frame
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
