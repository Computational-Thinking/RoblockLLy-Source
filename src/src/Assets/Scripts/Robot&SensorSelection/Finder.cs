/**
* Universidad de La Laguna
* Project: Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* File: Finder.cs : This file contains the "Finder" class 
*       implementation which is used to manage the status of objects that
*       have to remain between the different scenes, as is the case of the 
*       "selectedRobot". 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CLass used to manage the status of objects that have to remain between the 
/// different scenes, as is the case of the "selectedRobot".
/// </summary>
public class Finder : MonoBehaviour
{
    // This list will contain all the objects that we want to keep 
    // (DontDestroyOnLoad) between the different scenes.
    private static List<GameObject> findAbleObjects = new List<GameObject>();

    public static Finder finderInstance; // Singleton pattern instance.

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        // Checking to keep the singleton pattern.
        if (Finder.finderInstance == null)
        {
            Finder.finderInstance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Used to check if an object with a specific tag is inside the 
    /// findAbleObjects list. 
    /// </summary>
    /// <param name="tagToCheck"> The tag to check if is inside the 
    /// findAbleObject
    /// </param>
    /// <returns> True if the object with specific tag is inside te 
    /// findAbleObjects list. 
    /// </returns>
    public static bool CheckContains(string tagToCheck)
    {
        for (int i = 0; i < findAbleObjects.Count; i++)
        {
            //Debug.Log("current tag: " + tagToCheck);
            if (findAbleObjects[i].tag == tagToCheck)
            {
                //Debug.Log("Ha entrado en el if");
                return true;
            }   
        }
        return false;
    }

    /// <summary>
    /// Used to add a new element to the findAbleObjects list.
    /// </summary>
    /// <param name="objectToAdd"> The GameObject to addto check if is inside
    /// </param>
    public static void AddObject(GameObject objectToAdd)
    {
        findAbleObjects.Add(objectToAdd);
    }

    /// <summary>
    /// Usedto remove an object with a specific tag that is inside the 
    /// findAbleObjects list. 
    /// </summary>
    /// <param name="objectTag"> The tag of the element of the list to remove.
    /// </param>
    public static void RemoveElementByTag(string objectTag)
    {
        for (int i = 0; i < findAbleObjects.Count; i++)
        {
            if (findAbleObjects[i].tag == objectTag)
            {
                findAbleObjects.RemoveAt(i);
            }
        }
    }
}
