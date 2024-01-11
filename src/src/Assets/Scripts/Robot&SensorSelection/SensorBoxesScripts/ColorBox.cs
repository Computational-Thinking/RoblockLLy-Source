/**
* Universidad de La Laguna
* Project: Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* File: ColorBox.cs: This file contains the logic to spawn a Color 
*       sensor when the user clicks on the Color Sensor Box. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to manage the Color Box of the "SensorSelection" scene UI.
/// </summary>
public class ColorBox : MonoBehaviour
{
    public DragObject ColorSensor = null;

    /// <summary>
    /// This function is called when the user clicks on the Color sensor box.
    /// </summary>
    public void SpawnSensor()
    {
        DragObject InstantiatedSensor = Instantiate(ColorSensor);
        InstantiatedSensor.ManageDrag();
    }
}
