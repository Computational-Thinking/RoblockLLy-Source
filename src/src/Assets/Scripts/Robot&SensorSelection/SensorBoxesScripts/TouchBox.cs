/**
* Universidad de La Laguna
* Project: Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* File: TouchBox.cs : This file contains the logic to spawn a Touch 
*       sensor when the user clicks on the Touch Sensor Box.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to manage the Touch Box of the "SensorSelection" scene UI.
/// </summary>
public class TouchBox : MonoBehaviour
{
    public DragObject TouchSensor = null;

    /// <summary>
    /// This function is called when the user clicks on the touch sensor box.
    /// </summary>
    public void SpawnSensor()
    {
        DragObject InstantiatedSensor = Instantiate(TouchSensor);
        InstantiatedSensor.ManageDrag();
    }
}
