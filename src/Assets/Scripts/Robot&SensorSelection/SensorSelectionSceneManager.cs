/**
* Universidad de La Laguna
* Project: Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* File: SensorSelectionSceneManager.cs : This file contains the 
*       "SensorSelectionSceneManager" class implementation. This class is
*       used to manage the buttons to move from this scene to another.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Class used to manage the "Individual Sensor Selection" scene.
/// </summary>
public class SensorSelectionSceneManager : MonoBehaviour
{
    private GameObject selectedRobot;
    private GameObject rotatingPlatform;
    private bool settedUp = false;
    public GameObject gyroscopeCheck;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (selectedRobot == null) // Late start
        {
            selectedRobot = GameObject.FindWithTag("SelectedRobot");
            SetUpSelectedRobot();
            gyroscopeCheck.GetComponent<CheckGyroscope>().CheckActivated();
        }
    }

    /// <summary>
    /// This method sets up the robot position and rotation at the start 
    /// method of the scene
    /// </summary>
    void SetUpSelectedRobot()
    {
        float XPosCoord;
        float YPosCoord;
        float ZPosCoord;
        rotatingPlatform = GameObject.Find("RotatingPlatform");
        selectedRobot.transform.position = rotatingPlatform.transform.position;
        selectedRobot.transform.rotation = rotatingPlatform.transform.rotation;
        if (selectedRobot.name.Contains("Hunter"))
        {
            XPosCoord = selectedRobot.transform.position.x;
            YPosCoord = selectedRobot.transform.position.y;
            ZPosCoord = selectedRobot.transform.position.z;   
            selectedRobot.transform.position = new Vector3(XPosCoord - 10, YPosCoord + 25, ZPosCoord + 10);
        } else 
        {
            XPosCoord = selectedRobot.transform.position.x;
            YPosCoord = selectedRobot.transform.position.y;
            ZPosCoord = selectedRobot.transform.position.z;   
            selectedRobot.transform.position = new Vector3(XPosCoord - 10, YPosCoord + 10, ZPosCoord + 10);
        }
    }

    /// <summary>
    /// Method used to manage the transition between the 
    /// "IndividualSensorSelection" to the "IndividualSelectionMenu" scene.
    /// </summary>
    public void GoBack ()
    {   
        Finder.RemoveElementByTag("SelectedRobot");
        DestroySelectedRobot();
        // Load the previous level in the queue (in this case, is the scene 1, called "IndividualSelectionMenu").
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    /// <summary>
    /// Method used to manage the transition between the 
    /// "IndividualSensorSelection" to the "IndividualSensorSelection" scene.
    /// </summary>
    public void GoForward ()
    {
        // Load the selected challenge scene getting the selected challenge 
        // scene index in the player preferences.
        int sceneIndex = PlayerPrefs.GetInt("SelectedChallenge");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneIndex);
    }


    /// <summary>
    /// Method used to delete de selected robot gameobject to allow a new 
    /// in selection "IndividualSelectionMenu".
    /// </summary>
    private void DestroySelectedRobot()
    {
        GameObject auxRobot = GameObject.FindWithTag("SelectedRobot");
        if (auxRobot != null)
        {
            Destroy(auxRobot);
        } else {
            Debug.LogError("There is not any \"SelectedRobot\" currently");
        }
    }
}
