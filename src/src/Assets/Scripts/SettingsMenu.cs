/**
* Universidad de La Laguna"
* Project:  Roblockly
* Author: Basilio Gómez Navarro
* Email: alu0101049151@ull.edu.es
* File: AddedSensorBoxScript.cs : This file contains the class used to manage 
*       the behaviour of the settings menu, accessible from any scene 
*       except the main menu.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Start is called on the frame when a script is enabled just before
/// any of the Update methods is called the first time.
/// </summary>
public class SettingsMenu : MonoBehaviour
{
    public GameObject statisticsPanel;
    public GameObject statisticsInfoPanel;
    public GameObject challengeTitle;
    public GameObject bestTimeText;
    public GameObject leastBlocksSolutionText;
    public GameObject averageTimeText;
    private GameObject statisticsManager;
    private List<string> createdButtons = new List<string>();

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        statisticsManager = GameObject.FindWithTag("StatisticsManager");
    }

    /// <summary>
    /// This method is called when the user clicks on "Estadísticas" button of 
    /// the settings menu.
    /// </summary>    
    public void StatisticsPanelActivation()
    {
        if (statisticsPanel.activeSelf)
        {
            statisticsPanel.SetActive(false);
        } else {
            ActivateCompletedChallengesButtons();
            statisticsPanel.SetActive(true);
        }
    }

    /// <summary>
    /// This method is called when the user clicks on any achieved challenge 
    /// button in the statistics section fo the settings menu.
    /// </summary>  
    public void StatisticsInfoPanelActivation()
    {
        if (statisticsInfoPanel.activeSelf)
        {
            statisticsInfoPanel.SetActive(false);
        } else {
            statisticsInfoPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Activates the number of buttons required based on the number of 
    /// completed challenges found in the dictionary.
    /// </summary>
    public void ActivateCompletedChallengesButtons()
    {
        Dictionary<string, List<ChallengeSolution>> auxSolutions = statisticsManager.GetComponent<StatisticsManager>().GetChallengesSolutions();
        if (auxSolutions != null)
        {
            foreach (var challengeSolution in auxSolutions)
            {
                string buttonText = GetChallengeButtonText(challengeSolution.Key);
                int i = 1;
                bool found = false;
                while(!found && (i < 5))
                {
                    if ((!statisticsPanel.transform.GetChild(i).gameObject.activeSelf) && !CheckIfButtonHasBeenCreated(buttonText))
                    {
                        createdButtons.Add(buttonText);
                        found = true;
                        statisticsPanel.transform.GetChild(i).gameObject.SetActive(true);
                        statisticsPanel.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>().text = GetChallengeButtonText(challengeSolution.Key);
                    }
                    i++;
                }
            }
        }
    }


    /// <summary>
    /// Checks if a button with the same text has been already created.
    /// </summary>
    /// <param name="textOfTheButton"> The text to check. </param>
    /// <returns> True if the button has been created, false if no. </returns>
    private bool CheckIfButtonHasBeenCreated(string textOfTheButton)
    {
        if (createdButtons == null) return false;
        for (int i = 0; i < createdButtons.Count; i++)
        {
            if (textOfTheButton == createdButtons[i])
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Given a key of an element from ChallengesSolutions dictionary, returns 
    // the text to be displayed on the buttons of "StatisticsPanel" GUI.
    /// </summary>
    /// <param name="challengeName"> The string with the key to be "translated" 
    /// </param>
    /// <returns> The text to set inside the challenge button </returns>
    private string GetChallengeButtonText(string challengeName)
    {
        string challengeButtonText;
        switch (challengeName)
        {
            case "Labyrinth": 
                challengeButtonText = "Reto del laberinto";
                break;
            case "IR":
                challengeButtonText = "Reto del sendero blanco";
                break;
            case "Color":
                challengeButtonText = "Reto del sendero de colores";
                break;
            case "Gyroscope": 
                challengeButtonText = "Reto de la plataforma móvil";
                break;
            // In case to want to return the key given the text:
            case "Reto del laberinto":
                challengeButtonText = "Labyrinth";
                break;
            case "Reto del sendero blanco":
                challengeButtonText = "IR";
                break;
            case "Reto del sendero de colores":
                challengeButtonText = "Color";
                break;
            case "Reto de la plataforma móvil":
                challengeButtonText = "Gyroscope";
                break;
            default:
                challengeButtonText = "Reto de roblockly";
                break;
        }
        return challengeButtonText;
    }

    /// <summary>
    /// Loads the statistics information to display on the statistics info 
    /// panel of the selected achieved challenge .
    /// </summary>
    public void LoadSelectedButtonStatistics()
    {
        // Getting the button text content to get the key to look for.
        string buttonText;
        buttonText = EventSystem.current.currentSelectedGameObject.gameObject.transform.GetChild(0).GetComponent<Text>().text;
        string challengeKey = GetChallengeButtonText(buttonText); // Get key given the text.
        statisticsManager.GetComponent<StatisticsManager>().SetBestTimeBlocksSoloution();
        
        LoadStatisticsTittle(buttonText);
        LoadBestTimeInfo(challengeKey);
        LoadLeastBlocksSolution(challengeKey);
        LoadAverageTime(challengeKey);

        StatisticsInfoPanelActivation();
    }

    /// <summary>
    /// Loads the title of statistics info panel
    /// </summary>
    private void LoadStatisticsTittle(string buttonText)
    {
        // Setting "RetoTitle" text field:
        challengeTitle.GetComponent<Text>().text = buttonText;
    }

    /// <summary>
    /// Loads the best value of the less time spent in solving the challenge.
    /// </summary>
    private void LoadBestTimeInfo(string challengeKey)
    {
        // Setting "MejorTiempoText" text field:
        bestTimeText.transform.GetChild(0).GetComponent<Text>().text = GetBestTime(challengeKey);
    }

    /// <summary>
    /// Loads the least blocks solution and all its info.
    /// </summary>
    private void LoadLeastBlocksSolution(string challengeKey)
    {
        List<string> solutionInfo = GetLeastBlocks(challengeKey);
        // Setting "SolConMenosBloquesText" text field:
        // Setting "TiempoText" text field of "SolConMenosTiempoText" GUI section:
        leastBlocksSolutionText.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = solutionInfo[0];
        // Setting "BloquesUtilizadosText" text field of "SolConMenosTiempoText" GUI section:
        leastBlocksSolutionText.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = solutionInfo[1];
        // Setting "porcentajeDeAcercamiento" text field of "SolConMenosTiempoText" GUI section:
        leastBlocksSolutionText.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = solutionInfo[2];
    }

    /// <summary>
    /// Loads the value of the average time spent in solving the challenge.
    /// </summary>
    private void LoadAverageTime(string challengeKey)
    {
        List<float> auxAverageTime = GetAverageTime(challengeKey);
        string averageTime;
        // Setting "TiempoPromedio" text field:
        if (auxAverageTime[0] == 0.0f)
        {
            averageTime = auxAverageTime[1].ToString("f2");
            averageTimeText.transform.GetChild(0).GetComponent<Text>().text = averageTime + " segundos";
        } else {
            averageTime = auxAverageTime[0].ToString();
            averageTimeText.transform.GetChild(0).GetComponent<Text>().text = averageTime + " minuto(s)";
        }
    }

    /// <summary>
    /// This method calculates the least time spent in solving the challenge.
    /// </summary>
    private string GetBestTime(string challengeKey)
    {
        int i = 0;
        Dictionary<string, List<ChallengeSolution>> auxDictionary = statisticsManager.GetComponent<StatisticsManager>().GetChallengesSolutions();
        while (i < auxDictionary[challengeKey].Count)
        {
            if (auxDictionary[challengeKey][i].GetBestTime())
            {
                return auxDictionary[challengeKey][i].GetSolutionTime();
            }
            i++;
        }
        return " ";
    }

    /// <summary>
    /// Calculates the least blocks solution for the challenge.
    /// </summary>
    private List<string> GetLeastBlocks(string challengeKey)
    {
        int i = 0;
        List<string> solutionInfo = new List<string>();
        Dictionary<string, List<ChallengeSolution>> auxDictionary = statisticsManager.GetComponent<StatisticsManager>().GetChallengesSolutions();
        while(i < auxDictionary[challengeKey].Count)
        {
            if (auxDictionary[challengeKey][i].GetBestBlocks())
            {
                // Setting time elapsed to complete the challenge
                solutionInfo.Add(auxDictionary[challengeKey][i].GetSolutionTime());
                
                // Setting amount of used blocks;
                string auxBlocksNumber = auxDictionary[challengeKey][i].GetBlocksNumber().ToString();
                solutionInfo.Add(auxBlocksNumber);
                
                // Setting progress percentage
                //auxDictionary[challengeKey][i].CalculateProgress(challengeKey);
                string auxProgressPercentage = auxDictionary[challengeKey][i].GetProgress().ToString("f2");
                auxProgressPercentage = auxProgressPercentage + "%";
                solutionInfo.Add(auxProgressPercentage);
                return solutionInfo;
            }
            i++;
        }
        return solutionInfo;
    }

    /// <summary>
    /// Calculates the average time spent in solve the challenge. 
    /// </summary>
    private List<float> GetAverageTime(string challengeKey)
    {

        List<ChallengeSolution> selectedChallengeSolutions = statisticsManager.GetComponent<StatisticsManager>().GetChallengesSolutions()[challengeKey];
        float totalMinutes = 0.0f;
        float totalSeconds = 0.0f;
        foreach (ChallengeSolution solution in selectedChallengeSolutions)
        {
            List<float> auxSolutionTime = solution.GetSolutionTimeFloat();
            totalMinutes += solution.GetSolutionTimeFloat()[0];
            totalSeconds += solution.GetSolutionTimeFloat()[1];
        }
        float averageMinutes = (totalMinutes / selectedChallengeSolutions.Count);
        float averageSeconds = (totalSeconds / selectedChallengeSolutions.Count);
        List<float> averageTime = new List<float>();
        averageTime.Add(averageMinutes);
        averageTime.Add(averageSeconds);
        return averageTime;
    }

    /// <summary>
    /// Method called when the user clicks on "Cancelar" button.
    /// </summary>
    public void CancelMenu()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Method called when the user clicks on "Volver a menú inicial" button.
    /// </summary>
    public void BackToMainMenu()
    {
        if (GameObject.FindWithTag("SelectedRobot") != null)
        {
            Finder.RemoveElementByTag("SelectedRobot");
            DestroySelectedRobot();
        }
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Method called when the user clicks on "Volver a menú inicial" button
    /// to destroy the current selected robot.
    /// </summary>
    private void DestroySelectedRobot()
    {
        GameObject auxRobot = GameObject.FindWithTag("SelectedRobot");
        if (auxRobot != null)
        {
            Destroy(auxRobot);
        } else {
            Debug.LogError("There is not any \"SelectedRobot\" currently");
        }
    }

    /// <summary>
    /// Method to quit the game.
    /// </summary>
    public void QuitGame ()
    {   Debug.Log("Quit!"); // To check that it runs properly in Unity
        Application.Quit();
    }
}
