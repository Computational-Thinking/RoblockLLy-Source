/**
* Universidad de La Laguna"
* Project:  Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* Date: 25/06/2021
* File: AddedSensorBoxScript.cs : This file contains the class used to manage 
*       the actions carried out when the user clicks on a box of the "Sensores 
*       Añadidos" panel. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddedSensorBoxScript : MonoBehaviour
{
    // Delegate used to update the sensor pannel when a box is deleted when a sensor is deleted. This delegate is associated in the "BoxesManager.cs" script.
    public delegate void UpdateAddedSensorsPanel(string currentPanelText);
    public static UpdateAddedSensorsPanel UpdateAddedSensors;

    /// <summary>
    /// This method is used to delete the sensor associated with this box.
    /// </summary>
    public void DestroySensor()
    {
        // =================== Sensor Removal ======================
        GameObject SelectedRobot = GameObject.FindGameObjectWithTag("SelectedRobot");
        string sensorName = gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text;
        Destroy(SelectedRobot.transform.Find(sensorName).gameObject);

        // ====== Deactivativation of the "CancelPanel": ======
        GameObject CancelPanel = gameObject.transform.Find("CancelPanel").gameObject;
        if (CancelPanel.activeSelf)
        {
            CancelPanel.SetActive(false);
        }

        // ===== Update of the "AddedSensorsPanel" ======
        string auxString = gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text;
        UpdateAddedSensors(auxString);
    }
    
}
