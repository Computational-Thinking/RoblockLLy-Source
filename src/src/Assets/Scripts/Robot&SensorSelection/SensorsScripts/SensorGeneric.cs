/**
* Universidad de La Laguna
* Project: Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* File: SensorGeneric.cs : This file contains the 
*       "SensorGeneric" class implementation. This class is a "base" class
*       used to manage the "generic" logic of a any sensor type.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used to manage the "generic" logic of a any sensor type.
/// </summary>
public abstract class SensorGeneric : MonoBehaviour
{
    [SerializeField] protected GameObject panelSensor;
    bool linkedToARobot = false;
    protected GameObject auxiliarPanel;
    protected PanelsManager panelsContainer;
    private Transform SnappedPoint; // Is the point which it has been snapped.
    // This variable is set in the "SnapController" script, when the sensor is snapped.
    protected string sensorName;

    // Delegate used to release the snap point associated to the sensor deleted. This delegate is associated in the "SnapController.cs" script.
    public delegate void SetFreePoint(Transform snappedPoint);
    public static SetFreePoint SetFreeSnappedPoint;
    
    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown()
    {
        panelsContainer = Object.FindObjectOfType<PanelsManager>();
        Debug.Log("Está entrando en el MouseDown");
        if (linkedToARobot) {
            SetPanelName(sensorName);
            panelsContainer.InstantiatePanel(panelSensor);
        }
    }
    

    public Transform GetSnappedPoint()
    {
        return SnappedPoint;
    }

    public void SetSnappedPoint(Transform newSnappedPoint)
    {
        SnappedPoint = newSnappedPoint;
    }

    /// <summary>
    /// Sets the sensor name bases on the position where it has be placed.
    /// </summary>
    /// <param name="snapPoint"> The of the snap point position.</param>
    /// <returns> The name sensor name with the location in the robot. </returns>
    private string SetSensorLocationName(string snapPoint)
    {
        string sensorLocationName;
        switch (snapPoint)
        {
            case "FrontSnap":
                sensorLocationName = "delantero";
                break;
            case "TopFrontSnapCenter":
                sensorLocationName = "frontal central";
                break;
            case "TopFrontSnapRight":
                sensorLocationName = "frontal derecho";
                break;
            case "TopFrontSnapLeft":
                sensorLocationName = "frontal izquierdo";
                break;
            case "BackSnap":
                sensorLocationName = "trasero";
                break;
            case "LeftSnap":
                sensorLocationName = "izquierdo";
                break;
            case "RightSnap":
                sensorLocationName = "derecho";
                break;
            default:
                sensorLocationName = "robot";
                break;
        }
        return sensorLocationName;
    }

    /// <summary>
    /// Sets the sensor name with the position where it has be placed.
    /// </summary>
    /// <param name="snapPoint"> The of the snap point position.</param>
    public virtual void SetSensorName(string snapPoint)
    {
        string sensorLocationName = SetSensorLocationName(snapPoint);
        gameObject.transform.name = "Sensor " + sensorLocationName + ": ";
    }

    /// <summary>
    /// Sets the panel title that is displayed when the user clicks on any 
    /// sensor.
    /// </summary>
    /// <param name="panelName"> The content of the panel title.</param>
    protected void SetPanelName(string panelName)
    {
        panelSensor.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = panelName;
    }

    protected string GetPanelName()
    {
        return panelSensor.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text;
    }

    /// <summary>
    /// Sets the value of the linkedToARobot atribute that indicates that the 
    /// sensor is linked the a robot.
    /// </summary>
    /// <param name="linkStatus"> True if it is wanted to be anchored to the 
    /// robot.</param>
    protected void SetLinkSensor(bool linkStatus)
    {
        if (linkStatus)
        {
            linkedToARobot = true;
        } else {
            linkedToARobot = false;
        }
    }

    /// <summary>
    /// Destroys the panel when the user clicks on its "Cancelar" button.
    /// </summary>
    public void CancelPanel()
    {
        panelsContainer = Object.FindObjectOfType<PanelsManager>();
        panelsContainer.DestroyPanel(panelSensor.name + "(Clone)");
        SetPanelName("");
    }

    /// <summary>
    /// Stores the name of the sensor when the sensor name configuration is 
    /// finished.
    /// </summary>
    /// <param name="newSensorName"> Name of the sensor to be stored. </param>
    protected void StoreSensorName(string newSensorName)
    {
        sensorName = newSensorName;
    }

    /// <summary>
    /// Destroys the sensor when the user clicks on the "Eliminar" button of 
    /// the right panel displayed when a anchored sensor is clicked.
    /// </summary>
    public void DeleteSensor()
    {
        GameObject SelectedRobot = GameObject.FindGameObjectWithTag("SelectedRobot");
        SetFreeSnappedPoint(SelectedRobot.transform.Find(GetPanelName()).gameObject.GetComponent<SensorGeneric>().GetSnappedPoint());
        Destroy(SelectedRobot.transform.Find(GetPanelName()).gameObject);
        SelectedRobot.GetComponent<RobotManager>().DeleteSensorFromUsedSensors(GetPanelName());
        CancelPanel();
    }
}
