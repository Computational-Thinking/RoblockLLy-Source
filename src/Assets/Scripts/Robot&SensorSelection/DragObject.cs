/**
* Universidad de La Laguna"
* Project: Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* File: DragObject.cs : This file contains a class that allows each sensor to 
*       be dragged and dropped
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    // Delegate to manage the drag of the objects. Is used in AddNewSensor method of the class SanpController.
    public delegate void DraggEnded(DragObject sensorToDrag);
    public DraggEnded DragFinished;
    
    // Delegate for managing the color change of snap points when the user 
    // selects a sensor, in such a way that the available snap points are 
    // displayed. Is used with SnapController class.
    public delegate void ChangeSnapPointsColor(bool activation);
    public static ChangeSnapPointsColor ActiveSnapPointsColor;
    
    private float MouseZCoord; // Stores the Z coordinate of the mouse in a screen point.
 
    private bool dragged = false; // True if the sensor is dragged.

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (!Input.GetMouseButton(0) && dragged) // If drag ends.
        {
            dragged = false;
            ActiveSnapPointsColor(false); // If the drag ends, snap points stop displaying.
            DragFinished(this); // Call delegate for snap the sensor.
        } else if (dragged) // If it stills being dragged
        {
            ActiveSnapPointsColor(true); // If the drag start, snap points are displayed.
            transform.position = GetMouseWorldPos();
        }
    }

    /// <summary>
    /// ManageDrag is called when the sensor is instantiated in the 
    /// "UltrasoundBox script" to allow the spawn and drag of the sensor.
    /// </summary>
    public void ManageDrag()
     {
        // Takes the Z coordinate of the sensor position and convert it to screen point.
        MouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        //MouseOffset = gameObject.transform.position - GetMouseWorldPos();
        transform.position = GetMouseWorldPos(); // Move the object to the same 
                                                 // position of the mouse.
        dragged = true;
     }

    /// <summary>
    /// GetMouseWorldPos is used to give the mouse position on World 
    /// representation.
    /// </summary>
    private Vector3 GetMouseWorldPos()
    {
        Vector3 MousePoint = Input.mousePosition;
        MousePoint.z = MouseZCoord;
        return Camera.main.ScreenToWorldPoint(MousePoint);
    }
}
