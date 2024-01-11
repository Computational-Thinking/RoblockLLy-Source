/**
* Universidad de La Laguna
* Project: Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* File: PanelsManager.cs : This file contains the "PanelsManager" class 
*       implementation, used to manage the behavior of the set up panels of 
*       each sensors snapped to the robot.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CLass used to manage the behavior of the set up panels of each sensors 
/// snapped to the robot.
/// </summary>
public class PanelsManager : MonoBehaviour
{
    /// <summary>
    /// Used to instantiate a new panel. 
    /// </summary>
    /// <param name="panelSensorToInstantiate"> The new panel to instantiate.
    /// </param>
    public void InstantiatePanel(GameObject panelSensorToInstantiate)
    {
        if (transform.childCount > 0)
        {
            DestroyPanel(transform.GetChild(0).name);
            Instantiate(panelSensorToInstantiate, gameObject.transform);
        } else {
            Instantiate(panelSensorToInstantiate, gameObject.transform);
        }
    }

    /// <summary>
    /// Used to destroy a specific panel. 
    /// </summary>
    /// <param name="currentPanel"> The specific panel to destroy.
    /// </param>
    public void DestroyPanel(string currentPanel)
    {
        bool found = false;
        int i = 0;
        if (transform.childCount > 0)
        {
            while ((!found) && (i < transform.childCount))
            {
                if (transform.GetChild(i).name == currentPanel)
                {
                    found = true;
                    Destroy(transform.GetChild(i).gameObject);
                }
                i++;
            }
        } else {
            Debug.LogError("There are not any panel to destroy");
        }
    }
}
