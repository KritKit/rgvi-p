using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class textscene : MonoBehaviour
{
    public Text debugText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        debugText.text = "Debug Info: " + Time.time.ToString();
        if ((Input.GetKeyDown(KeyCode.Space)) || (Input.GetKeyDown(KeyCode.JoystickButton1))){
            // SceneManager.LoadScene("DifMenu");
            SceneManager.LoadScene("cggg2");
        }
    }
}
