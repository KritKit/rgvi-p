using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>(); //เสียงเลข
    public AudioSource numberSource; // AudioSource สำหรับเล่นเสียงเลข
    private int thousands, hundreds, tens, units;
    public static int FinalScore; //คะแนนสุดท้าย
    public Text pointsText;
    public bool switchscore = true;

    public int SceneChecker;
    public int SceneCurrent;

    void Start()
    {
        numberSource = GetComponent<AudioSource>();
        SceneCurrent = SceneManager.GetActiveScene().buildIndex;
        SceneChecker = SceneManager.GetActiveScene().buildIndex + 1;
    }
    public void Setup(int score){
        // gameObject.SetActive(true);
        pointsText.text = score.ToString() + " Points";
    }

    void Update(){
        // if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton4)){ //L1
        //     SceneManager.LoadScene("Scenes/MainMenu");
        // }
        // if(Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.JoystickButton0)){ //X
        //     SceneManager.LoadScene(SceneCurrent);
        // }
        // if(Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.JoystickButton5)){ //R1
        //     SceneManager.LoadScene(SceneChecker);
        // }
    }

    
}
