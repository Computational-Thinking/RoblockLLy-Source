/**
* Universidad de La Laguna
* Project: Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* File: RobotSelection.cs : This file contains the 
*       "RobotSelection" class implementation. This class is
*       used to manage the change of different robots on the rotating platform.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to manage the change of different robots on the rotating 
/// platform.
/// </summary>
public class RobotSelection : MonoBehaviour
{
    private int CurrentRobot = 0; // Is the index of the current child of the 
                                  // RobotHolder GameObject.

    private int RobotIndexSelected = 0;

    /// <summary>
    /// Method to change the robots on the rotating platform.
    /// </summary>
    private void SelectRobot (int robotIndex)
    {
        // If robotIndex is greater or equal to the number of RobotHolder 
        // children, recalculate de index again to allow the user can click on 
        // the arrow buttons like it hasn't got an end.
        if ((robotIndex >= transform.childCount) || (robotIndex < 0))
        {
            robotIndex = Mathf.Abs(robotIndex); // If is negative (<0)
            robotIndex = robotIndex % transform.childCount;
        }

        // The for loop below activates and deactivates the current robot.
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.tag = "notCurrent";
            if (i == robotIndex)
            {
                transform.GetChild(i).gameObject.tag = "CurrentRobot";
            }   
            transform.GetChild(i).gameObject.SetActive(i == robotIndex);
        }
    }

    /// <summary>
    /// Is the public method to change the robots on the rotating platform.
    /// </summary>
    public void ChangeRobot (int valueToAdd) 
    {
        CurrentRobot += valueToAdd;
        SelectRobot(CurrentRobot);
    }

    /// <summary>
    ///  This method marks the current robot as the selected one to move to the 
    ///  next scene.
    /// </summary>
    public void ConfirmRobot()
    {
        for (int i = 0; i < transform.childCount; i++){
            if (transform.GetChild(i).gameObject.tag == "CurrentRobot")
            {
                RobotIndexSelected = i;
            }
        }
        PlayerPrefs.SetInt("RobotSelected", RobotIndexSelected);
    }
}
