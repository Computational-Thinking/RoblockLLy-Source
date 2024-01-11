// Not used at the moment






using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSceneManager : MonoBehaviour
{
    public static bool _switchOn = false;

    public static GlobalSceneManager globalInstance;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (GlobalSceneManager.globalInstance == null)
        {
            globalInstance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public static void SwitchOnSecene3(bool status)
    {
        Debug.Log("Ha entrado en el SwitchOnScene3 con status: " + status);
        if (status == true) { _switchOn = true; }
        else { _switchOn = false; }
        Debug.Log("SwitchOn: " + _switchOn);
    }
}
