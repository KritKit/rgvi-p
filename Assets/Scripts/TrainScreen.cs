using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrainScreen : MonoBehaviour
{
    public Text pointsText;
    public static int totalscore = 0;
    int scoreff;

    void Update(){
        if(Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.JoystickButton4)){ //L1
            totalscore = 0;
            SceneManager.LoadScene("Scenes/MainMenu");
        }
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton5)){ //R1
            totalscore = 0;
            SceneManager.LoadScene("Scenes/tr1_1");
        }
    }
    // Use Awake or Start for initialization
    void Awake()
    {
        scoreff = totalscore;
        pointsText.text = scoreff.ToString() + " Points";
    }
}
