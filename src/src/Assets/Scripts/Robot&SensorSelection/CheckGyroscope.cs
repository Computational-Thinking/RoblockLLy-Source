/**
* Universidad de La Laguna
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* Date: 18/06/2021
* File: CheckGyroscope.cs : This file contains the logic for the 
*       Gyroscope checkbox selection.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGyroscope : MonoBehaviour
{
    public delegate void GyroscopeChecked();
    public static GyroscopeChecked CheckboxGyroscope;

    public delegate void GyroscopeNotChecked();
    public static GyroscopeNotChecked DeactivateGyroscope;

    private GameObject selectedRobot;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (selectedRobot == null) // Late start
        {
            selectedRobot = GameObject.FindWithTag("SelectedRobot").gameObject;
        }
    }

    public void CheckActivated()
    {
        if (selectedRobot == null) // Late start
        {
            selectedRobot = GameObject.FindWithTag("SelectedRobot").gameObject;
        }
        if (selectedRobot.GetComponent<RobotManager>().GetGyrsocopeStatus())
        {
            gameObject.SetActive(true);
        }
    }

    public void Acivation()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            CheckboxGyroscope();
        } else {
            gameObject.SetActive(false);
            DeactivateGyroscope();
        } 
    }
}
