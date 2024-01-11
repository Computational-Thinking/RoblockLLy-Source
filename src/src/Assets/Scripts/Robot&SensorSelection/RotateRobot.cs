/**
* Universidad de La Laguna
* Project: Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* File: RotateRobot.cs : This file contains the code to allow the user to 
*                        rotate the robot by dragging it with the mouse.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to manage the rotation of the robot dragging it with the mouse.
/// </summary>
public class RotateRobot : MonoBehaviour
{
    public float RotationSpeed = 150;

    /// <summary>
    /// OnMouseDrag is called when the user has clicked on a GUIElement or 
    /// Collider and is still holding down the mouse.
    /// </summary>
    void OnMouseDrag()
    {
        RotateWithMouse();
    }

    /// <summary>
    /// Allows the user rotate the robot when drag ir with the mouse.
    /// </summary>
    private void RotateWithMouse() 
    {
        float rotX = Input.GetAxis("Mouse X") * RotationSpeed * Mathf.Deg2Rad;
        transform.Rotate(Vector3.up, -rotX);
    }
}
