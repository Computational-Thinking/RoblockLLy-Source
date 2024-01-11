/**
* Universidad de La Laguna
* Project: Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* File: SensorUS.cs : This file contains the "SensorUS" class implementation. 
*       This class is used to manage any logic of the Ultrasound sensor type.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to manage any logic of the Ultrasound sensor type.
/// </summary>
public class SensorUS : SensorGeneric
{
    private float range;
    private float error;
    //pruebas
    public float dist = 0.0f;
    //public GameObject obstacle;



    private GameObject coneCollider;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        coneCollider = gameObject.transform.Find("ConeCollider").gameObject;
        
        coneCollider.SetActive(false);
    }

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
    }
    /*void Update() {
      Vector3 right = transform.TransformDirection(Vector3.right);
      RaycastHit hit;
      Ray forwardRay = new Ray(transform.position-right*5.0f, -right*100.0f);
      dist = 10.0f;
      //obstacle = null;
      Debug.DrawRay(transform.position-right*5.0f,-right*100.0f, Color.red);
      if (Physics.Raycast(transform.position-right*5.0f,-right*100.0f, out hit)) {
        //obstacle = hit.transform.gameObject;
        dist = hit.distance/20.0f;
      }
    }*/

    /// <summary>
    /// Sets the sensor name adding the sensor type
    /// </summary>
    /// <param name="snapPoint"> The of the snap point position.</param>
    public override void SetSensorName(string snapPoint)
    {
        base.SetSensorName(snapPoint);
        gameObject.transform.name = gameObject.transform.name + "Ultrasonido";
        if (string.Compare(gameObject.transform.name, "Sensor derecho: Ultrasonido") == 0) {
          gameObject.transform.name = "Sensor izquierdo: Ultrasonido";
        }
        else if (string.Compare(gameObject.transform.name, "Sensor izquierdo: Ultrasonido") == 0) {
          gameObject.transform.name = "Sensor derecho: Ultrasonido";
        }
        Debug.Log(gameObject.transform.name);
        base.StoreSensorName(gameObject.transform.name);
    }

    public float GetDistance()
    {
      Vector3 right = transform.TransformDirection(Vector3.right);
      RaycastHit hit;
      Ray forwardRay = new Ray(transform.position-right*5.0f, -right*100.0f);
      float value = 10.0f;
      //obstacle = null;
      //Debug.DrawRay(transform.position-right*5.0f,-right*100.0f, Color.red);
      if (Physics.Raycast(transform.position-right*5.0f,-right*100.0f, out hit)) {
        //obstacle = hit.transform.gameObject;
        value = hit.distance/20.0f;
      }
      //float value = coneCollider.GetComponent<ConeCollider>().GetCollidedDistance();
      //dist = value;
      return value;
    }
}
