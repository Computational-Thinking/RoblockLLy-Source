/**
* Universidad de La Laguna
* Project: Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
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
public class SnapController : MonoBehaviour
{                                       
    public List<Transform> SnapPoints; // Contains all the snap points of each 
                                       // robot.
    public float SnapRange = 15.0f;
    DragObject[] SensorsToDrag; // Contains all the draggable
                                // sensors;

    /*The delegate below is used to indicate that a new sensor has been linked to the robot. Is used in the scripts from the "SensorsScripts" directory*/
    public delegate void SetLinkedToARobotDelegate(bool status);
    public static event SetLinkedToARobotDelegate SetLinkedToARobotOn;

    private List<Transform> UsedSnapPoints = new List<Transform>();

    /// <summary>
    // Start is called before the first frame update
    /// </summary>
   void Start()
    {
        SensorGeneric.SetFreeSnappedPoint = SetFreeSnapPoint;
        DragObject.ActiveSnapPointsColor = ActivateSnapPoints;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        AddNewSensor();
    }

    /// <summary>
    /// AddNewSensor is called in every frame on the Update loop to check if
    /// a new sensor has been created.
    /// </summary>
    private void AddNewSensor()
    {
        SensorsToDrag = Object.FindObjectsOfType<DragObject>(); // Looks for 
                                                    // each new added sensor.
        if (SensorsToDrag.Length > 0)
        {
            for (int i = 0; i < SensorsToDrag.Length; i++)
            {
                if (SensorsToDrag[i].tag == "NewSensor")
                {
                    SensorsToDrag[i].tag = "NotNewSensor";
                    SensorsToDrag[i].DragFinished = OnDragEnded;
                }
            }
        }
    }

    /// <summary>
    /// OnDragEnded is called when the mouse up event occurs and consequently, 
    /// the dragg process finish.
    /// </summary>
    private void OnDragEnded(DragObject sensorToDrag)
    {
        SnapSensor(sensorToDrag);
    }

    /// <summary>
    /// This function is called when sensor drag ends. 
    /// </summary>
    /// <param name="sensorToDrag"> The dragged sensor to snap to the robot.</param>
    private void SnapSensor(DragObject sensorToDrag)
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

        bool used = false; // Auxiliary variable to check if the closest snap 
                           // point has been used.
        if (UsedSnapPoints.Count > 0) // Check if any snap point has been used.
        {
            //Debug.Log("UsedSanpPoints.Count: " + UsedSnapPoints.Count);
            for (int i = 0; i < UsedSnapPoints.Count; i++)
            {
                if(UsedSnapPoints[i] == ClosestSnapPoint)
                {
                    used = true;
                }
            }
        }
        if (!used) // If the Closest Sanp Point is free
        {
            //Debug.Log("ClosestSnapPoint = " + ClosestSnapPoint);
            //Debug.Log("ClosestDistance = " + ClosestDistance);
            if ((ClosestSnapPoint != null) && (ClosestDistance <= SnapRange))
            { // If the snap was successful:
                UsedSnapPoints.Add(ClosestSnapPoint);
                sensorToDrag.GetComponent<SensorGeneric>().SetSnappedPoint(ClosestSnapPoint);
                //Debug.Log("Ha hecho el snap");
                SetSnappedSensor(sensorToDrag, ClosestSnapPoint);
                sensorToDrag.transform.parent = gameObject.transform; // Adds the sensor as a child of the robot.
                SetLinkedToARobotOn(true);
            }
            else // If the sensor doesn't snaps any point.
            {
                ActivateSnapPoints(false); // Turn off color in the snap points.
                Destroy(sensorToDrag.gameObject);
            }
        }
        else // If the sensor doesn't snaps any point.
        {
            ActivateSnapPoints(false); // Turn off color in the snap points.
            Destroy(sensorToDrag.gameObject);
        }
    }

    /// <summary>
    /// This method set the snapped sensor position, name and adds it to the 
    /// Added used sensors dictionary.
    /// </summary>
    /// <param name="sensorToDrag"> The dragged sensor to snap to the robot.
    /// </param>
    /// <param name="ClosestSnapPoint"> The closest snap point to the dragged 
    /// sensor.</param>
    private void SetSnappedSensor(DragObject sensorToDrag, Transform ClosestSnapPoint)
    {
        switch (sensorToDrag.name)
        {
            case "SensorUS(Clone)":
                SetUltrasoundSensor(sensorToDrag, ClosestSnapPoint);
                sensorToDrag.GetComponent<SensorUS>().SetSensorName(ClosestSnapPoint.tag);
                this.GetComponent<RobotManager>().AddUsedSensor(sensorToDrag.name, "SensorUS"); // Adds the sensor type to the usedSensors Dictionary in RobotManager.cs
                break;
            case "SensorTouch(Clone)":
                SetTouchSensor(sensorToDrag, ClosestSnapPoint);
                sensorToDrag.GetComponent<SensorTouch>().SetSensorName(ClosestSnapPoint.tag);
                this.GetComponent<RobotManager>().AddUsedSensor(sensorToDrag.name, "SensorTouch");
                break;
            case "SensorIR(Clone)":
                SetInfraredColorSensor(sensorToDrag, ClosestSnapPoint);
                sensorToDrag.GetComponent<SensorIR>().SetSensorName(ClosestSnapPoint.tag);
                this.GetComponent<RobotManager>().AddUsedSensor(sensorToDrag.name, "SensorIR");
                break;
            case "SensorColor(Clone)":
                SetInfraredColorSensor(sensorToDrag, ClosestSnapPoint);
                sensorToDrag.GetComponent<SensorColor>().SetSensorName(ClosestSnapPoint.tag);
                this.GetComponent<RobotManager>().AddUsedSensor(sensorToDrag.name, "SensorColor");
                break;
            default:
                Debug.Log("There aren't any sensor of this type");
                break;
        }
    }

    /// <summary>
    /// This method is used to set up the position of any Ultrasound sensor 
    /// when it is snapped to the robot.
    /// </summary>
    private void SetUltrasoundSensor(DragObject sensorToDrag, Transform ClosestSnapPoint)
    {
        //Debug.Log ("Es un sensor de ultrasonido");
        //====== Position transform ================================
        sensorToDrag.transform.position = ClosestSnapPoint.transform.position;
        float XPosCoord = sensorToDrag.transform.position.x;
        float YPosCoord = sensorToDrag.transform.position.y;
        float ZPosCoord = sensorToDrag.transform.position.z;
        float offset;

        //====== Rotation transform ================================
        sensorToDrag.transform.rotation = ClosestSnapPoint.transform.rotation;
        switch (ClosestSnapPoint.tag)
        {
            case "FrontSnap":
                offset = 3f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 90.0f, 0.0f);
                break;
            case "TopFrontSnapCenter":
                offset = 1.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 90.0f, -60.0f);
                break;
            case "TopFrontSnapRight":
                offset = 1.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 90.0f, -60.0f);
                break;
            case "TopFrontSnapLeft":
                offset = 1.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 90.0f, -60.0f);
                break;
            case "BackSnap":
                offset = 3f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, -90.0f, -90.0f);
                break;
            case "LeftSnap":
                offset = 1.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 5.0f, -60.0f);
                break;
            case "RightSnap":
                offset = 1.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 90.0f, -60.0f);
                break;
            default: // Set up the sensor to the front by default;
                offset = 1.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 90.0f, -60.0f);
                break;
        }
    }
    
    /// <summary>
    /// This method is used to set up the position of any Touch sensor 
    /// when it is snapped to the robot.
    /// </summary>
    private void SetTouchSensor(DragObject sensorToDrag, Transform ClosestSnapPoint)
    {
        //====== Position transform ================================
        sensorToDrag.transform.position = ClosestSnapPoint.transform.position;
        float XPosCoord = sensorToDrag.transform.position.x;
        float YPosCoord = sensorToDrag.transform.position.y;
        float ZPosCoord = sensorToDrag.transform.position.z;
        float offset;

        //====== Rotation transform ================================
        sensorToDrag.transform.rotation = ClosestSnapPoint.transform.rotation;
        switch (ClosestSnapPoint.tag)
        {
            case "FrontSnap":
                offset = 1.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 90.0f, 0.0f);
                break;
            case "TopFrontSnapCenter":
                offset = 0.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 90.0f, -60.0f);
                break;
            case "TopFrontSnapRight":
                offset = 0.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 90.0f, -60.0f);
                break;
            case "TopFrontSnapLeft":
                offset = 0.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 90.0f, -60.0f);
                break;
            case "BackSnap":
                offset = 1.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 90.0f, -90.0f);
                break;
            case "LeftSnap":
                offset = 0.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 0.0f, -60.0f);
                break;
            case "RightSnap":
                offset = 0.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 90.0f, -60.0f);
                break;
            default: // Set up the sensor to the front by default;
                offset = 1.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 90.0f, 0.0f);
                break;
        }
    }

    /// <summary>
    /// This method is used to set up the position of any Infrared or Color
    /// sensor when it is snapped to the robot.
    /// </summary>
    private void SetInfraredColorSensor(DragObject sensorToDrag, Transform ClosestSnapPoint)
    {
        //====== Position transform ================================
        sensorToDrag.transform.position = ClosestSnapPoint.transform.position;
        float XPosCoord = sensorToDrag.transform.position.x;
        float YPosCoord = sensorToDrag.transform.position.y;
        float ZPosCoord = sensorToDrag.transform.position.z;
        float offset;

        //====== Rotation transform ================================
        sensorToDrag.transform.rotation = ClosestSnapPoint.transform.rotation;
        switch (ClosestSnapPoint.tag)
        {
            case "FrontSnap":
                offset = -1.0f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(70.0f, 180.0f, 0.0f);
                break;
            case "TopFrontSnapCenter":
                offset = -1.0f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(110.0f, 0.0f, 180.0f);
                break;
            case "TopFrontSnapRight":
                offset = -1.0f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(150.0f, 0.0f, 180.0f);
                break;
            case "TopFrontSnapLeft":
                offset = -1.0f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(150.0f, 0.0f, 180.0f);
                break;
            case "BackSnap":
                offset = -1.0f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(60.0f, 0.0f, 0.0f);
                break;
            case "LeftSnap":
                offset = -1.0f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(60.0f, 90.0f, 0.0f);
                break;
            case "RightSnap":
                offset = -1.0f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(60.0f, 180.0f, 0.0f);
                break;
            default: // Set up the sensor to the front by default;
                offset = 1.5f;
                sensorToDrag.transform.position = new Vector3(XPosCoord, YPosCoord + offset, ZPosCoord);
                sensorToDrag.transform.Rotate(0.0f, 90.0f, -60.0f);
                break;
        }
    }

    /// <summary>
    /// This method is used to set free a snap point when a sensor is removed 
    /// from him.
    /// </summary>
    /// <param name="snappedPoint"> The snap point to set free.
    /// </param>
    private void SetFreeSnapPoint(Transform snappedPoint)
    {
        for (int i = 0; i < UsedSnapPoints.Count; i++)
        {
            if (UsedSnapPoints[i] == snappedPoint)
            {
                UsedSnapPoints.Remove(UsedSnapPoints[i]);
            }
        }
    }

    /// <summary>
    /// This method is used to activate and deactivate the snap points when the 
    /// user drags and drops a sensor. 
    /// </summary>
    /// <param name="activation"> True if want to actiate the snap points.
    /// </param>
    private void ActivateSnapPoints(bool activation)
    {
        if (activation)
        {
            for (int i = 0; i < SnapPoints.Count; i++)
            {
                SnapPoints[i].GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 0, 0, 1)); // Change color to red.
            }
        } else {
            for (int i = 0; i < SnapPoints.Count; i++)
            {
                SnapPoints[i].GetComponent<Renderer>().material.SetColor("_Color", new Color(0, 0, 0, 0)); // Changes back the color to transparent.
            }
        }
    }
}
