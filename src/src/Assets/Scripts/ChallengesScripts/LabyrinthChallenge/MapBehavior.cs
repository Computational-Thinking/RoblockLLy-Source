/**
* Universidad de La Laguna"
* Project: Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* Date: 27/07/2021
* File: MapBehaviour.cs: This file contains the class used to manage 
*       the behaviour of the camera that follows the robot during the 
*       different challenges.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to manage the behaviour of the camera that follow the robot 
/// duing a challenges.
/// </summary>
public class MapBehavior : MonoBehaviour
{
    private Transform robotTransform;
    // Start is called before the first frame update
    void Start()
    {
        robotTransform = GameObject.FindWithTag("SelectedRobot").transform;
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        Vector3 newMapPosition = robotTransform.position;
        newMapPosition.y = robotTransform.position.y + 80;
        transform.position = newMapPosition;
    }
}
