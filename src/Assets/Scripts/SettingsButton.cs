/**
* Universidad de La Laguna"
* Project:  Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* File: SettingsButton.cs : This file contains the class used to manage 
*       the activation and deactivation of settings button.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage the activation and deactivation of the settings button.
/// </summary>
public class SettingsButton : MonoBehaviour
{
    /// <summary>
    /// Method used to manage the activation and deactivation of the settings 
    /// button.
    /// </summary>
    public GameObject SettingsMenu;

    public void SettingsMenuActivation()
    {
        if (SettingsMenu != null)
        {
            if (SettingsMenu.activeSelf)
            {
                SettingsMenu.SetActive(false);
            } else {
                SettingsMenu.SetActive(true);
            }
        }
    }
}
