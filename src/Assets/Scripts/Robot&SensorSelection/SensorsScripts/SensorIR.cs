/**
* Universidad de La Laguna
* Project: Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* File: SensorIR.cs : This file contains the 
*       "SensorIR" class implementation. This class is
*       used to manage any logic of the Infrared sensor type.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to manage any logic of the Infrared sensor type.
/// </summary>
public class SensorIR : SensorGeneric
{
    private float range;
    private float precision;
    private float rayRange = 100.0f;
    private Transform sensorLED;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        SnapController.SetLinkedToARobotOn += base.SetLinkSensor;
        SnapControllerDefender.SetLinkedToARobotOn += base.SetLinkSensor;
        SnapControllerHummer.SetLinkedToARobotOn += base.SetLinkSensor;
        SnapControllerHunter.SetLinkedToARobotOn += base.SetLinkSensor;
        sensorLED = gameObject.transform.Find("Sensor/LED_Green");
    }
  
    /// <summary>
    /// Sets the sensor name adding the sensor type
    /// </summary>
    /// <param name="snapPoint"> The of the snap point position.</param>
    public override void SetSensorName(string snapPoint)
    {
        base.SetSensorName(snapPoint);
        gameObject.transform.name = gameObject.transform.name + "Infrarrojo";
        base.StoreSensorName(gameObject.transform.name);
    }

    /// <summary>
    /// Checks if the sensor is detecting the given color.
    /// </summary>
    /// <param name="colorToDetect"> The color to check.</param>
    public bool DetectColor(string colorToDetect)
    {
        RaycastHit sensorRayHit;
        Debug.DrawRay(sensorLED.position, sensorLED.up * rayRange, Color.yellow, 5.0f);
        if (Physics.Raycast(sensorLED.position, sensorLED.up, out sensorRayHit, rayRange))
        {
            //Debug.Log("Está chocando con: " + sensorRayHit.transform.GetComponent<Renderer>().material.name);
            if (sensorRayHit.transform.GetComponent<Renderer>().material.name == colorToDetect)
            {
                return true;
            }
        }
        return false;
    }
}
