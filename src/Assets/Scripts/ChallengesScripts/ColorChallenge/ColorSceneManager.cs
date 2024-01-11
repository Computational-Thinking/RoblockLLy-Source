/**
* Universidad de La Laguna
* Project: Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* Date: 20/08/2021
* File: ColorSceneManager.cs : This file contains the 
*       "ColorSceneManager" class implementation which allows the 
*        configuration of some stuff of the scene as:
*           - Set the position of the selectedRobot gameobject.
*           - Manage the size of the chellenge viewer.
*           - Run the logic of the buttos to go backward or reset robot 
*             position.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class used to manage the scene configuration.
/// </summary>
public class ColorSceneManager : MonoBehaviour
{
    private GameObject selectedRobot;
    [SerializeField] private Transform startPoint; // Is the point from which the selected robot will take its position and rotation.

    public GameObject maximizeButtonSmall;
    public GameObject maximizeButtonBig;
    public GameObject smallChallengeViewer;
    public GameObject bigChallengeViewer;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        SetUpSelectedRobot();
    }

    /// <summary>
    /// This method sets up the robot position and rotation at the start 
    /// method of the scene
    /// </summary>
    public void SetUpSelectedRobot()
    {
        UBlockly.CSharp.Runner.Stop();
        float XPosCoord;
        float YPosCoord;
        float ZPosCoord;
        if (selectedRobot == null)
        {
            selectedRobot = GameObject.FindWithTag("SelectedRobot");            
        }
        selectedRobot.transform.position = startPoint.position;
        selectedRobot.transform.rotation = startPoint.rotation;
        selectedRobot.GetComponent<RobotManager>().Kinematic(true); // Enables robot physics again.
        selectedRobot.GetComponent<RobotMotionController>().ResetAngleRotated();
        if (selectedRobot.name.Contains("Hunter"))
        {
            XPosCoord = selectedRobot.transform.position.x;
            YPosCoord = selectedRobot.transform.position.y;
            ZPosCoord = selectedRobot.transform.position.z;   
            selectedRobot.transform.position = new Vector3(XPosCoord, YPosCoord + 22, ZPosCoord);
        } else if (selectedRobot.name.Contains("Kaiju")) {
            XPosCoord = selectedRobot.transform.position.x;
            YPosCoord = selectedRobot.transform.position.y;
            ZPosCoord = selectedRobot.transform.position.z;   
            selectedRobot.transform.position = new Vector3(XPosCoord, YPosCoord + 10, ZPosCoord);
        } else {
            XPosCoord = selectedRobot.transform.position.x;
            YPosCoord = selectedRobot.transform.position.y;
            ZPosCoord = selectedRobot.transform.position.z;   
            selectedRobot.transform.position = new Vector3(XPosCoord, YPosCoord + 2, ZPosCoord);
        }
    }

    /// <summary>
    /// Method used to manage the transition between the 
    /// "IndividualSelectionScene" to the "MainMenu" scene.
    /// </summary>
    public void GoBack ()
    {
        int sceneNumber = PlayerPrefs.GetInt("SelectedChallenge");
        selectedRobot.GetComponent<RobotManager>().Kinematic(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - sceneNumber);
    }

    /// <summary>
    /// Method used to disable the small challenge viewer and activate the big 
    /// one.
    /// </summary>
    public void ChangeToBig()
    {
        if ((!bigChallengeViewer.activeSelf) && (smallChallengeViewer.activeSelf))
        {
            smallChallengeViewer.SetActive(false);
            bigChallengeViewer.SetActive(true);
        }
    }

    /// <summary>
    /// Method used to disable the big challenge viewer and activate the samll 
    /// one.
    /// </summary>
    public void ChangeToSmall()
    {
        if ((bigChallengeViewer.activeSelf) && (!smallChallengeViewer.activeSelf))
        {
            bigChallengeViewer.SetActive(false);
            smallChallengeViewer.SetActive(true);
        }
    }
}
