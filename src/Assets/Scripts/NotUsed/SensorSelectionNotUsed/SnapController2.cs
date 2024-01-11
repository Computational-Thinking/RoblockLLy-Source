/**
* Universidad de La Laguna
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* Date: 07/06/2021
* File: SnapController.cs : This file contains the 
*       "SnapController" class implementation. This class is
*       used to manage the snap point to fix the selected sensors
*       to each robot.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to manage the different snap points of each robot.
/// </summary>
public class SnapController2 : MonoBehaviour
{
   // public GameObject Sensors = null;
    public List<Transform> SnapPoints; // Contains all the snap points of each 
                                       // robot.
    public float SnapRange = 2.0f;
    // bool HasSnappedSensor = false;

    // Start is called before the first frame update
    void Start()
    {
        /*for (int i = 0; i < Sensors.transform.childCount; i++)
        {
           Debug.Log(Sensors.transform.GetChild(i).gameObject.name);
        }
        /*foreach (DragObject currentSensor in SensorsToDrag)
        {
            currentSensor.DragFinished = OnDragEnded;
        }*/
    }

    /// <summary>
    /// OnDragEnded is called when the mouse up event occurs and consequently, 
    /// the dragg process finish.
    /// </summary>
    private void OnDragEnded(DragObject sensorToDrag)
    {
        float ClosestDistance = -1;
        Transform ClosestSnapPoint = null;
        foreach (Transform currentSnapPoint in SnapPoints)
        {
            float currentDistance = Vector3.Distance(sensorToDrag.transform.position, currentSnapPoint.transform.position);
            if (ClosestSnapPoint == null || currentDistance < ClosestDistance)
            {
                ClosestSnapPoint = currentSnapPoint;
                ClosestDistance = currentDistance;
            }
        }

        Debug.Log("ClosestSnapPoint = " + ClosestSnapPoint);
        Debug.Log("ClosestDistance = " + ClosestDistance);

        if ((ClosestSnapPoint != null) && (ClosestDistance <= SnapRange))
        {
            //HasSnappedSensor = true;
            Debug.Log("Ha hecho el snap");
            sensorToDrag.transform.position = ClosestSnapPoint.transform.position;
            sensorToDrag.transform.parent = gameObject.transform;
            float XCoordinate = sensorToDrag.transform.position.x;
            float YCoordinate = sensorToDrag.transform.position.y;
            float ZCoordinate = sensorToDrag.transform.position.z;
            float offset = 3f;
            sensorToDrag.transform.position = new Vector3(XCoordinate, YCoordinate + offset, ZCoordinate);
        }
    }
}
