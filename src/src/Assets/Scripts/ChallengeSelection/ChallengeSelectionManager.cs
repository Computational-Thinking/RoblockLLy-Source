/**
* Universidad de La Laguna"
* Project: Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* Date: 17/08/2021
* File: ChallengeSelectionManager.cs: This file contains the class used to 
*       manage the challenge selection in the "ChallengeSelection" Scene.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class manages the choice of the challenge to be carried out.
/// </summary>
public class ChallengeSelectionManager : MonoBehaviour
{
    /// <summary>
    /// Method to select the "Labyrinth" challenge.
    /// </summary>
    public void SelectLabyrinthChallenge()
    {
        PlayerPrefs.SetInt("SelectedChallenge", 1);
    }

    /// <summary>
    /// Method to select the "Withe path" challenge.
    /// </summary>
    public void SelectIRChallenge()
    {
        PlayerPrefs.SetInt("SelectedChallenge", 2);
    }
    
    /// <summary>
    /// Method to select the "Clor path" challenge.
    /// </summary>
    public void SelectColorChallenge()
    {
        PlayerPrefs.SetInt("SelectedChallenge", 3);
    }

    /// <summary>
    /// Method to select the "Balance platform" challenge.
    /// </summary>
    public void SelectBalanceChallenge()
    {
        PlayerPrefs.SetInt("SelectedChallenge", 4);
    }

    /// <summary>
    /// Method used to manage the transition between the 
    /// "ChallengeSelection" to the "IndividualSensorSelection" scenes.
    /// </summary>
    public void GoForward()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Method used to manage the transition between the 
    /// "ChallengeSelection" to the "IndividualSelectionMenu" scenes.
    /// </summary>
    public void GoBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
