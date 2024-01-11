/**
* Universidad de La Laguna"
* Project:  Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* Date: 27/07/2021
* File: TimerBehaviour.cs : This file contains the class used to manage 
*       the behaviour of the timer panel of the GUI of any challenge scene.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the behaviour of the timer panel of the GUI of any challenge scene.
/// </summary>
public class TimerBehaviour : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private bool timeFinished = false;
    private bool runTimer = false;

    // Start is called before the first frame update
    void Start()
    {
        StartTimer();
        CoinBehaviour.StopTimer = TimerFinish;
    }

    // Update is called once per frame
    void Update()
    {
        if (runTimer)
        {
            ConvertTimerToString();
        }
    }

    /// <summary>
    /// This method is used to start the timer.
    /// </summary>
    public void StartTimer()
    {
        ResetTimer();
        startTime = Time.time;
        runTimer = true;
    }

    /// <summary>
    /// Used to seset the timer values.
    /// </summary>
    public void ResetTimer()
    {
        timeFinished = false;
        timerText.text = "00:00:00";
        timerText.color = Color.black;
    }

    /// <summary>
    /// Used to calculate the current timer values in each frame.
    /// </summary>
    public List<float> TimerFunction()
    {
        List<float> timerParts = new List<float>();
        if (!timeFinished)
        {
            float currentTime = Time.time - startTime;
            float minutes = ((int) currentTime / 60);
            float seconds = (currentTime % 60);
            timerParts.Add(minutes);
            timerParts.Add(seconds);
        }
        return timerParts;
    }

    /// <summary>
    /// Used to convert the timer number values to strings, in order to display 
    /// them in the panel.
    /// </summary>
    private void ConvertTimerToString()
    {
        List<float> currentTime = TimerFunction();
        string minutes = currentTime[0].ToString("f0");
        string seconds = currentTime[1].ToString("f2");
        string[] secondsParts = seconds.Split(',');
        timerText.text = minutes + ":" + secondsParts[0] + ":" + secondsParts[1];
    }

    public string GetTimeString()
    {
        return timerText.text;
    }

    /// <summary>
    /// This method is called when the user finishes the current challenge.
    /// </summary>
    public void TimerFinish()
    {
        timeFinished = true;
        runTimer = false;
        timerText.color = Color.red;
    }
}
