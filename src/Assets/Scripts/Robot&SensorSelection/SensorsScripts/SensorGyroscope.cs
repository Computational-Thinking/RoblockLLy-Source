/**
* Universidad de La Laguna
* Project: Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* File: SensorGyroscope.cs : This file contains the 
*       "SensorGyroscope" class implementation. This class allows to manage the *       gyroscope sensor behaviour.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorGyroscope : MonoBehaviour
{
    private GameObject selectedRobot;
    private GameObject BalanceSphere; // Is the sphere under the platform of the "BalanceChallenge".

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        selectedRobot = GameObject.FindWithTag("SelectedRobot");
    }

    /// <summary>
    /// Returns the direction where the robot is moving.
    /// </summary>
    /// <returns> True if the shere is inside the center red circle of the 
    /// balance platform </returns>
    public string GyroscopeBehavior()
    {
        BalanceSphere = GameObject.Find("BaseSphere");
        if (!CheckParallel())
        {
            if (CheckAngle())
            {
                return "forward";
            } else {
                return "backward";
            }
        }
        return "parallel";
    }

    /// <summary>
    /// Checks that the robot is parallel with the horizontal plane of the base 
    /// sphere
    /// </summary>
    /// <returns> True if the shere is inside the center red circle of the 
    /// balance platform </returns>
    private bool CheckParallel()
    {
        Vector3 SphereRobotVector = (selectedRobot.transform.position - BalanceSphere.transform.position).normalized;
        //Debug.DrawLine(selectedRobot.transform.position, BalanceSphere.transform.position, Color.red, 5.0f);
        Vector3 PointSphereUp = new Vector3(BalanceSphere.transform.position.x, BalanceSphere.transform.position.y + 100, BalanceSphere.transform.position.z);
        Vector3 SphereUp = (PointSphereUp - BalanceSphere.transform.position).normalized;
        //Debug.DrawLine(BalanceSphere.transform.position, PointSphereUp, Color.blue, 5.0f);
        float auxAngle = Vector3.SignedAngle(SphereRobotVector, SphereUp, BalanceSphere.transform.right);
        Debug.Log("Ángulo: " + auxAngle);
        auxAngle = Mathf.Round(auxAngle);
        if (auxAngle < 10.0f && auxAngle > -10.0f)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Checks that the robot is moving forward on the platform.
    /// </summary>
    /// <returns> True if is moving forward or flase if is backwards.</returns>
    private bool CheckAngle()
    {
        Vector3 SphereRobotVector = (selectedRobot.transform.position - BalanceSphere.transform.position).normalized;
        Vector3 PointSphereUp = new Vector3(BalanceSphere.transform.position.x, BalanceSphere.transform.position.y + 100, BalanceSphere.transform.position.z);
        Vector3 SphereUp = (PointSphereUp - BalanceSphere.transform.position).normalized;
        float auxAngle = Vector3.SignedAngle(SphereRobotVector, SphereUp, BalanceSphere.transform.right);
        Debug.Log("Ángulo2: " + auxAngle);
        if (auxAngle < 0.0f)
        {
            return true; // Robot is moving forward
        }
        return false; // Robot is moving backward
    }
}
