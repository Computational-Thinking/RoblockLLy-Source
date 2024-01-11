/**
* Universidad de La Laguna
* Project:  Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* File: IndividualSelectionSceneManager.cs : This file contains the 
*       "IndividualSelectionSceneManager" class implementation. This class is
*       used to manage the selection of the robots for the individual 
*       challenges.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class used to manage the "Individual Robot Selection" scene.
/// </summary>
public class IndividualSelectionSceneManager : MonoBehaviour
{
    private GameObject Platform; // Is the rotating platform.
    private GameObject CurrentRobot; // Is the robot that is currently rotating 
                                     // on the platform
    public float RotationSpeed = 5;
    
    void Start()
    {
        Platform = GameObject.FindWithTag("RotatingPlatform");
    }

    void Update()
    {
        RotatePlatform();
    }

    /// <summary>
    /// Method called from the Update function to rotate the plantform where 
    /// the robots are on.
    /// </summary>
    private void RotatePlatform()
    {
        CurrentRobot = GameObject.FindWithTag("CurrentRobot");
        Platform.transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
        CurrentRobot.transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Method used to manage the transition between the 
    /// "IndividualSelectionScene" to the "MainMenu" scene.
    /// </summary>
    public void GoBack ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    /// <summary>
    /// Method used to manage the transition between the 
    /// "IndividualSelectionScene" to the "IndividualSensorSelection" scene.
    /// </summary>
    public void GoForward ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}


