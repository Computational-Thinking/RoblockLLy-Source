using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global2 : MonoBehaviour
{
    public static Global2 global2Instance;
    private bool madeDontDestroyOnLoad = false;
    GameObject selectedRobot;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (Global2.global2Instance == null)
        {
            Global2.global2Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(gameObject);
        }

        //if (selectedRobot != null)
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!madeDontDestroyOnLoad)
        {
            selectedRobot = GameObject.FindWithTag("SelectedRobot");
            DontDestroyOnLoad(selectedRobot);
            madeDontDestroyOnLoad = true;
        }*/
    }
}
