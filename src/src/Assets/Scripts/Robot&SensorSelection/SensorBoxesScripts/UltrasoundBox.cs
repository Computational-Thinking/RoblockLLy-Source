/**
* Universidad de La Laguna
* Project: Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* File: UltrasoundBox.cs : This file contains the logic to spawn a Ultrasound 
*       sensor when the user clicks on the Ultrasound Sensor Box.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to manage the Ultrasound Box of the "SensorSelection" scene UI.
/// </summary>
public class UltrasoundBox : MonoBehaviour
{
    public DragObject UltrasoundSensor = null;

    /// <summary>
    /// This function is called when the user clicks on the ultrasound sensor 
    /// box.
    /// </summary>
    public void SpawnSensor()
    {
        DragObject InstantiatedSensor = Instantiate(UltrasoundSensor);
        InstantiatedSensor.ManageDrag();
    }

}
