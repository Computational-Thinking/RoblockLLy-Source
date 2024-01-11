/**
* Universidad de La Laguna
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* File: RobotManager.cs : This file contains the 
*       "RobotManager" class implementation. This class is
*       used to manage the general robot behaviour.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RobotManager : MonoBehaviour
{
    private bool Gyroscope = false; // True if the user click on 
                                       // "giróscopo" checkbox.
    public SensorGyroscope GyroscopeSensor = null;
    private SensorGyroscope InstantiatedGyroscope;
    private bool Microphone = false; // True if the user clicks on 
                                     // "microphone" checkbox.
    private GameObject BalanceSphere; // Is the sphere under the platform of the "BalanceChallenge".
    
    private Dictionary<string, string> usedSensors = new Dictionary<string, string>(); // Save the sensors snapped to the robot to know the blocks to show in the coding panel of the UGUI. This check is carried out in SnapController script and SensorGeneric (DeleteSensor method).

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Kinematic(true);
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        CheckGyroscope.CheckboxGyroscope = GyroscopeActivation;
        //CheckMicrophone.CheckboxMicrophone = MicrophoneActivation;
        CheckGyroscope.DeactivateGyroscope = GyroscopeDeactivation;
    }

    /// <summary>
    /// Is called when the user clisk on the "Giróscopo" checkbox.
    /// </summary>
    public void GyroscopeActivation()
    {
        Gyroscope = true;
        InstantiatedGyroscope = Instantiate(GyroscopeSensor,
        gameObject.transform);
        AddUsedSensor("Gyroscope", "Gyroscope");
    }

    /// <summary>
    /// Is called when the "Giróscopo" checkbox is unchecked.
    /// </summary>
    public void GyroscopeDeactivation()
    {
        Gyroscope = false;
        Destroy(gameObject.transform.Find(InstantiatedGyroscope.gameObject.name).gameObject);
        DeleteSensorFromUsedSensors("Gyroscope");  
    }

    public bool GetGyrsocopeStatus()
    {
        return Gyroscope;
    }


   /* public void MicrophoneActivation() // Not used
    {
        Microphone = true;
        AddUsedSensor("Microphone", "Microphone");
    }

    public void MicrophoneDeactivation() // Not used
    {
        Microphone = false;
        DeleteSensorFromUsedSensors("Microphone");
    }*/

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        CheckGyroscope.CheckboxGyroscope -= GyroscopeActivation;
        //CheckMicrophone.CheckboxMicrophone -= MicrophoneActivation;
        CheckGyroscope.DeactivateGyroscope -= GyroscopeDeactivation;
    }

    /// <summary>
    /// Adds a new sensor to the usedSensors dictionary.
    /// </summary>
    /// <param name="usedSensor"> The name of the used sensor
    /// </param>
    /// <param name="usedSensorType"> The type of the used sensor
    /// </param>
    public void AddUsedSensor(string usedSensor, string usedSensorType)
    {
        if (!usedSensors.ContainsValue(usedSensor))
        {
            usedSensors.Add(usedSensor, usedSensorType);
        }
    }

    /// <summary>
    /// checks if a sensor is inside the usedSensors dictionary.
    /// </summary>
    /// <param name="sensorType"> The name of the sensor to check.
    /// </param>
    /// <return> True if the sensor is inside the dictionary, false if no </returns>
    public bool CheckSensor(string sensorType)
    {
        if (usedSensors.ContainsValue(sensorType))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Delete given sensor from the usedSensors dictionary.
    /// </summary>
    /// <param name="usedSensor"> The name of the sensor to delet.
    /// </param>
    public void DeleteSensorFromUsedSensors(string usedSensor)
    {
        usedSensors.Remove(usedSensor);
    }

    /// <summary>
    /// Enables or disables the kinematic of the robot.
    /// </summary>
    /// <param name="status"> True for enable or false for disable the robot 
    /// kinematic </param> 
    public void Kinematic(bool status)
    {
        Rigidbody robotRb = GetComponent<Rigidbody>();
        robotRb.isKinematic = status;
    }

    /// <summary>
    /// Checks if the contact sensor with the given name is colliding with any 
    /// object.
    /// </summary>
    /// <param name="touchSensorToFind"> The name of the touch sensor to check 
    /// for.
    /// </param>
    /// <return> True if the sensor is making contact, false if no </returns>
    public bool GetTouchInfo(string touchSensorToFind)
    {
        GameObject sensorToFind;
        string sensorNameToFind = "Sensor " + touchSensorToFind + ": Contacto";
        if (!CheckSensorExist(sensorNameToFind))
        {   
            string error = "No has colocado ningún sensor en la posición " + touchSensorToFind;
            GameObject errorPanel = GameObject.Find("Canvas").transform.Find("ErrorPanel").gameObject;
            errorPanel.GetComponent<ErrorPanel>().ShowErrorSensorPosition(error);
            return false;
        }
        sensorToFind = transform.Find(sensorNameToFind).gameObject;
        return sensorToFind.GetComponent<SensorTouch>().GetContact();
    }

    /// <summary>
    /// Checks if the IR sensor with the given name is detecting the specific 
    /// given color.
    /// </summary>
    /// <param name="iRSensorToFind"> The name of the IR sensor to check for.
    /// </param>
    /// <param name="colorToDetect"> The name of the color to be detected.
    /// </param>
    /// <return> True if the sensor is detecing the color, false if no </returns>
    public bool GetIRInfo(string iRSensorToFind, string colorToDetect)
    {
        GameObject sensorToFind;
        string sensorNameToFind = "Sensor " + iRSensorToFind + ": Infrarrojo";
        if (!CheckSensorExist(sensorNameToFind))
        {   
            string error = "No has colocado ningún sensor en la posición " + iRSensorToFind;
            GameObject errorPanel = GameObject.Find("Canvas").transform.Find("ErrorPanel").gameObject;
            errorPanel.GetComponent<ErrorPanel>().ShowErrorSensorPosition(error);
            return false;
        }
        sensorToFind = transform.Find(sensorNameToFind).gameObject;
        return sensorToFind.GetComponent<SensorIR>().DetectColor(colorToDetect);
    }
    
    
    /// <summary>
    /// Checks if the Color sensor with the given name is detecting the specific 
    /// given color.
    /// </summary>
    /// <param name="colorSensorToFind"> The name of the Color sensor to check 
    /// for.
    /// </param>
    /// <param name="colorToDetect"> The name of the color to be detected.
    /// </param>
    /// <return> True if the sensor is detecing the color, false if no </returns>
    public bool GetColorInfo(string colorSensorToFind, string colorToDetect)
    {
        GameObject sensorToFind;
        string sensorNameToFind = "Sensor " + colorSensorToFind + ": Color";
        if (!CheckSensorExist(sensorNameToFind))
        {   
            string error = "No has colocado ningún sensor en la posición " + colorSensorToFind;
            GameObject errorPanel = GameObject.Find("Canvas").transform.Find("ErrorPanel").gameObject;
            errorPanel.GetComponent<ErrorPanel>().ShowErrorSensorPosition(error);
            return false;
        }
        sensorToFind = transform.Find(sensorNameToFind).gameObject;
        return sensorToFind.GetComponent<SensorColor>().DetectColor(colorToDetect);
    }


    /// <summary>
    /// Checks the distance the Ultasound sensor is.
    /// </summary>
    /// <param name="uSSensorToFind"> The name of the US sensor to check for.
    /// </param>
    /// <return> The distance between the object and the sensor </returns>
    public float GetUSInfo(string uSSensorToFind)
    {
        GameObject sensorToFind;
        string sensorNameToFind = "Sensor " + uSSensorToFind + ": Ultrasonido";
        if (!CheckSensorExist(sensorNameToFind))
        {   
            string error = "No has colocado ningún sensor en la posición " + uSSensorToFind;
            GameObject errorPanel = GameObject.Find("Canvas").transform.Find("ErrorPanel").gameObject;
            errorPanel.GetComponent<ErrorPanel>().ShowErrorSensorPosition(error);
            return Mathf.Infinity;
        }
        sensorToFind = transform.Find(sensorNameToFind).gameObject;
        float distance = sensorToFind.GetComponent<SensorUS>().GetDistance();
        return distance;
    }

    /// <summary>
    /// Checks if the robot is inclined in the given direction.
    /// </summary>
    /// <param name="direction"> The direction to check if the robot is 
    /// inclined.
    /// </param>
    /// <return> True if the robot ins inclined in the given direction, false 
    /// if no. </returns>
    public bool GetGyroscopeInfo(string direction)
    {
        if ((SceneManager.GetActiveScene().name == "BalanceChallenge") && (Gyroscope))
        {
            string status = InstantiatedGyroscope.GyroscopeBehavior();
            if (status == direction)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Checks if a specific sensor is in a specific position on the robot.
    /// </summary>
    /// <param name="SensorNameToFind"> The name of the sensor to check.
    /// </param>
    /// <return> True if the sensor is making contact, false if no </returns>
    private bool CheckSensorExist(string SensorNameToFind)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name == SensorNameToFind)
            {
                return true;
            }
        }
        return false;
    }
}
