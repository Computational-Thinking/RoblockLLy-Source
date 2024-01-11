/**
* Universidad de La Laguna
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* Date: 27/06/2021
* File: BoxesManager.cs : This file contains the logic to spawn the boxes of 
* each sensor on the right box of the UI.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxesManager1 : MonoBehaviour
{
    private int activatedBoxes = 0;
    [SerializeField] private List<AddedSensorBoxScript> boxes = new List<AddedSensorBoxScript>();

    /*/// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        GameObject auxCamera = GameObject.Find("Main Camera");
        DontDestroyOnLoad(this.gameObject);
    }*/

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        AddedSensorBoxScript.UpdateAddedSensors = UpdateRightSensorsPanel;
        //SnapController.CreateNewAddedSensorBox += OnActivation;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        
    }

    /// <summary>
    /// This function is called when a sensor is added to the robot in order to 
    /// spawn the box on the "Sensors Added" box on the right part of the UI.
    /// </summary>
    private
     void OnActivation(DragObject selectedSensor)
    {
        bool activated = false;
        int i = 0;
        string sensorType = NewBoxName(selectedSensor);
        while ((!activated) && (i < transform.childCount)) 
        {
            if ((!transform.GetChild(i).gameObject.activeSelf) && (transform.GetChild(i).gameObject.tag == "AddedSensorBox"))
            {
                transform.GetChild(i).gameObject.SetActive(true);
                //Debug.Log(transform.GetChild(i).gameObject
                //);
                //transform.GetChild(i).GetComponent<AddedSensorBoxScript>().SetAssociatedSensor(selectedSensor);
                transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Sensor " + (i + 1) + ": " + sensorType; // Used to write the sensor type in the box text.
                activated = true;
                activatedBoxes++;
                selectedSensor.transform.name = "Sensor " + (i + 1) + ": " + sensorType;
            } else {
                i++;
            }
        }
    }

    /// <summary>
    /// This method is called to set up the text inside the box created when a 
    /// sensor is added to the robot.
    /// </summary>
    private string NewBoxName(DragObject selectedSensor)
    {
        string nameToReturn;
        switch (selectedSensor.name)
        {
            case "SensorUS(Clone)":
                nameToReturn = "Ultrasonido";
                break;
            case "SensorTouch(Clone)":
                nameToReturn = "Contacto";
                break;
            case "SensorIR(Clone)":
                nameToReturn = "Infrarrojo";
                break;
            case "SensorColor(Clone)":
                nameToReturn = "Color";
                break;
            default:
                Debug.Log(selectedSensor.name);
                nameToReturn = "NewSensor";
                break;
        }
        return nameToReturn;
    }

    /// <summary>
    /// This method is called by the delegate on the "AddedSensorBoxScript.cs" 
    /// script when the user deletes a sensor from the robot.
    /// </summary>
    private void UpdateRightSensorsPanel(string currentPanelText)
    {
        if (activatedBoxes == 1) // If only the first box is activated.
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            activatedBoxes--;
        } else {
            bool found = false;
            bool stop = false;
            int i = 0;
            while ((!stop) && (i < gameObject.transform.childCount))
            {
                string auxiliarText = gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text; // Is an auxiliar GameObject used to do the swap between the other boxes;
                if ((auxiliarText == currentPanelText) && (gameObject.transform.GetChild(i).gameObject.activeSelf))
                {
                    found = true;
                }
                if (found)
                {
                    // If the current box is the last one on the list or the last one activated, the current box is deactivated.
                    if ((!gameObject.transform.GetChild(i + 1).gameObject.activeSelf) || (gameObject.transform.GetChild(i + 1).gameObject == null))
                    {   activatedBoxes--;
                        stop = true ;
                        gameObject.transform.GetChild(i).gameObject.SetActive(false);
                    } else {
                        // First takes the text from the next box.
                        auxiliarText = gameObject.transform.GetChild(i + 1).gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text;

                        // Second, changes current box text by the "auxiliarText" variable content.
                        gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = auxiliarText;
                    }
                }
                i++; 
            }
        }
    }
}
