/**
* Universidad de La Laguna
* Project: Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* File: LabyrinthSeceneManager.cs : This file contains the 
*       "LabyrinthSceneManager" class implementation which allows the 
*        configuration of some stuff of the scene as:
*           - Set the position of the selectedRobot gameobject.
*           - Manage the size of the chellenge viewer.
*           - Run the logic of the buttos to go backward or reset robot 
*             and platform position.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class used to manage the scene configuration.
/// </summary>
public class GyroscopeSceneManager : MonoBehaviour
{
    private GameObject selectedRobot;
    [SerializeField] private Transform startPoint; // Is the point from which the selected robot will take its position and rotation.

    public GameObject maximizeButtonSmall;
    public GameObject maximizeButtonBig;
    public GameObject smallChallengeViewer;
    public GameObject bigChallengeViewer;
    public GameObject errorPanel;
    
    private GameObject platform;
    private Vector3 platformInitialPos; // Is the platform where the robot have to stay.

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        platform = GameObject.Find("Platform").gameObject;
        platformInitialPos = platform.transform.position;
        SetUpSelectedRobot();
        CheckError();
    }

    /// <summary>
    /// This method sets up the robot position and rotation at the start 
    /// method of the scene
    /// </summary>
    public void SetUpSelectedRobot()
    {
        UBlockly.CSharp.Runner.Stop();
        if (selectedRobot == null)
        {
            selectedRobot = GameObject.FindWithTag("SelectedRobot");            
        }
        selectedRobot.transform.position = startPoint.position;
        float XPosCoord = selectedRobot.transform.position.x;
        float YPosCoord = selectedRobot.transform.position.y;
        float ZPosCoord = selectedRobot.transform.position.z;   
        selectedRobot.transform.position = new Vector3(XPosCoord, YPosCoord + 10, ZPosCoord);
        selectedRobot.transform.rotation = startPoint.rotation;
        selectedRobot.GetComponent<RobotManager>().Kinematic(true);
        selectedRobot.GetComponent<RobotMotionController>().ResetAngleRotated();
        platform.transform.position = platformInitialPos;
        platform.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void CheckError()
    {
        if (CheckSensors())
        {
            errorPanel.GetComponent<ErrorPanel>().ShowErrorGyroscope();
        }
    }

    private bool CheckSensors()
    {
        bool error = false;
        string currentName;
        for (int i = 0; i < selectedRobot.transform.childCount; i++)
        {
            currentName = selectedRobot.transform.GetChild(i).name;
            if (currentName.Contains("Color") || currentName.Contains("Infrarrojo") || currentName.Contains("Ultrasonido") || currentName.Contains("Contacto"))
            {
                error = true;
            }
        }
        return error;
    }

    public void DisableRobotKinematic()
    {
        selectedRobot.GetComponent<RobotManager>().Kinematic(false);
        platform.GetComponent<Rigidbody>().isKinematic = false;
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
