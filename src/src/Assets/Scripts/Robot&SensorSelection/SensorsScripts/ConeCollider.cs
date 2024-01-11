/**
* Universidad de La Laguna
* Project: Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* File: ConeCollider.cs: This file contains the logic to manage the behaviour 
*       of the cone collider, used to measure the distance an object is from 
*       the sensor.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to manage the behaviour of the Cone collider used with 
/// the Ultrasound sensor.
/// </summary>
public class ConeCollider : MonoBehaviour
{
    public GameObject parentSensorUS;
    private bool collided = false;
    private GameObject collidedObject;
    private Vector3 distanceToCollided;
    private bool firstCollision = false;
    private float baseDistance;
    private bool Z = false;
    private int callCounter;
    private string direcctionToUse;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        this.GetComponent<Renderer>().enabled = false;
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        callCounter = 0;
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        collided = true;
        UpdateCollidedObject(other);
        //Debug.Log("Ha chocado con: " + other.gameObject.name);
        //Debug.Log("Posición muro: " + other.transform.position);
    }

    /// <summary>
    /// This method is used to update the colliedeObject GameObject which the
    /// cone collider has collided with.
    /// </summary>
    /// <param name="other">The Collision data associated with the cone 
    /// collider collision.</param>
    private void UpdateCollidedObject(Collision other)
    {
        if (collidedObject != null)
        {
            if (collidedObject.tag == "CollidedObject")
            {
                collidedObject.tag = "NotCollidedObject";
            }
        }
        collidedObject = other.gameObject;
        collidedObject.tag = "CollidedObject";
    }

    /// <summary>
    /// OnCollisionExit is called when this collider/rigidbody has
    /// stopped touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionExit(Collision other)
    {
        collided = false;
        firstCollision = false;
        callCounter = 0;
    }

    /// <summary>
    /// This method calculates the position vector component of the object that 
    /// has to be used to calculate the distance between the sensor and the 
    /// collided object.
    /// </summary>
    /// <returns> The distance between the sensor and the collided object. </returns>
    public float GetCollidedDistance()
    {
        Vector3 distance;
        if (collided)
        {
            callCounter++;
            distance = parentSensorUS.transform.position - collidedObject.transform.position;
            if (callCounter == 1 || callCounter == 2) // If is the first or second collision with object
            {   
                direcctionToUse = CheckDirection(callCounter, distance);
                if (callCounter == 2)
                {
                    if (direcctionToUse == "Z")
                    {
                        baseDistance = Mathf.Abs(distance.z);
                    } else if (direcctionToUse == "X") {
                        baseDistance = Mathf.Abs(distance.x);
                    }
                    return 10.0f;
                }
            }
            float finalDistance = Mathf.Infinity;
            if (callCounter > 2)
            {
                if (direcctionToUse == "Z")
                {
                    finalDistance = CalculateDistance(baseDistance, Mathf.Abs(distance.z));
                } else if (direcctionToUse == "X") {
                    finalDistance = CalculateDistance(baseDistance, Mathf.Abs(distance.x));
                }
            }
            return Mathf.Round(finalDistance);
        }
        return Mathf.Infinity;
    }

    /// <summary>
    /// This method calculates the distance between the sensor and the collided 
    /// object.
    /// </summary>
    /// <returns> The distance between the sensor and the collided object. </returns>    
    private float CalculateDistance(float baseDistance, float currentDistance)
    {
        return (currentDistance * 10.0f) / baseDistance; // Is divided by 10 because is th maximum value which the user can set.
    }

    /// <summary>
    /// This method calculates the vector position component of the collided 
    /// object, that has to be used to calculate the dis between the sensor and 
    /// the said object.
    /// </summary>
    /// <returns> The name of the component that has to be used: X or Z 
    /// depending on the position of the object. </returns> 
    private string CheckDirection(int callCounter, Vector3 currentDistance)
    {
        if (callCounter == 1)
        {
            distanceToCollided = currentDistance;
        } else if (callCounter == 2) {
            if (Mathf.Round(distanceToCollided.x) == 
                Mathf.Round(currentDistance.x))
            {
                return "Z";
            } else if (Mathf.Round(distanceToCollided.z) == 
                Mathf.Round(currentDistance.z)) {
                return "X";
            }
        }
        return "NONE";
    }

    /*private bool CheckParallel(Vector3 SensorVector, Vector3 ObjectVector)
    {
        float alignment = ((SensorVector.x * ObjectVector.x) + 
                           (SensorVector.y * ObjectVector.y) +
                           (SensorVector.z * ObjectVector.z));
        alignment = Mathf.Round(alignment);
        if (alignment == 1.0f || alignment == 0.0f)
        {
            return true;
        }
        return false;
    }*/
}
