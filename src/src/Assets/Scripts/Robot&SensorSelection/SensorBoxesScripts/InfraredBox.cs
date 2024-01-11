/**
* Universidad de La Laguna
* Project: Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* File: InfraredBox.cs : This file contains the logic to spawn a Infrared 
*       sensor when the user clicks on the Infrared Sensor Box.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to manage the Infrared Box of the "SensorSelection" scene UI.
/// </summary>
public class InfraredBox : MonoBehaviour
{
    public DragObject InfraredSensor = null;

    /// <summary>
    /// This function is called when the user clicks on the infrared sensor box.
    /// </summary>
    public void SpawnSensor()
    {
        DragObject InstantiatedSensor = Instantiate(InfraredSensor);
        InstantiatedSensor.ManageDrag();
    }
}
