/**
* Universidad de La Laguna
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* Date: 09/07/2021
* File: CancelPanelSpawner.cs : This file contains the logic to activate and 
*       deactivate the boxes current "AddedSensorBox" created because the new 
*       sensor added to the robot.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelPanelSpawner : MonoBehaviour
{
    public void ActivatePanel()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
    }
    public void Desactivate()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}
