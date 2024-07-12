using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_to_tutor : MonoBehaviour
{

    public Text[] menuOptions;
    public GameObject[] BgOptions;
    public int currentIndex = 0;
    public string[] sceneNames;
    public AudioSource menuAudioSource;

    // เพิ่มตัวแปร AudioClip สำหรับแต่ละเมนู
    public AudioClip menuSe;
    public AudioClip menuOk;
    public AudioClip menuSelectSound1;
    public AudioClip menuSelectSound2;
    public AudioClip menuSelectSound3;
    public AudioClip menuSelectSound4;
    public AudioClip menuSelectSound5;

    public static bool keyswitch2 = true;

    public GameObject tutorstage;

    void Start()
    {
        UpdateMenuHighlight();
        keyswitch2 = true;
    }

    void Update()
    {
        if (keyswitch2 == true)
        {
            HandleInput();
        }
        else
        {
            return;
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.JoystickButton4)) //L1
        {
            MoveSelection(-1);
            menuAudioSource.PlayOneShot(menuSe);

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.JoystickButton5)) //R1
        {
            MoveSelection(1);
            menuAudioSource.PlayOneShot(menuSe);
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)) //X (A)
        {
            menuAudioSource.PlayOneShot(menuOk);
            StartCoroutine(WaitForScene());
            
           
        }
    }

    void MoveSelection(int direction)
    {
        currentIndex = (currentIndex + direction + menuOptions.Length) % menuOptions.Length;
        UpdateMenuHighlight();

        menuAudioSource.Stop();
        switch (currentIndex)
        {
            case 0:
                if (menuSelectSound1 != null)
                    menuAudioSource.PlayOneShot(menuSelectSound1);
                break;
            case 1:
                if (menuSelectSound2 != null)
                    menuAudioSource.PlayOneShot(menuSelectSound2);
                break;
            case 2:
                if (menuSelectSound3 != null)
                    menuAudioSource.PlayOneShot(menuSelectSound3);
                break;
            case 3:
                if (menuSelectSound3 != null)
                    menuAudioSource.PlayOneShot(menuSelectSound4);
                break;
            case 4:
                if (menuSelectSound3 != null)
                    menuAudioSource.PlayOneShot(menuSelectSound5);
                break;
            // เพิ่ม case เพิ่มได้ตามจำนวนเมนู
            // ...
        }
    }

    void UpdateMenuHighlight()
    {
        for (int i = 0; i < menuOptions.Length; i++)
        {
            menuOptions[i].fontStyle    = (i == currentIndex) ? FontStyle.Bold : FontStyle.Normal;
            menuOptions[i].fontSize     = (i == currentIndex) ? 59 : 55;
            menuOptions[i].color        = (i == currentIndex) ? Color.white : Color.blue;
            BgOptions[i].GetComponent<RectTransform>().sizeDelta = (i == currentIndex) ? new Vector2(249, 30) : new Vector2(249, 30);
            BgOptions[i].GetComponent<Image>().color = (i == currentIndex) ? new Color32(255, 0, 0, 255) : new Color32(255, 255, 255, 255);
        }
    }
    IEnumerator WaitForScene()
    {
        yield return new WaitForSeconds(1f);
        LoadScene();

    }
    void LoadScene()
    {
        if (sceneNames[currentIndex] == "btm"){
            tutorstage.SetActive(false);
            keyswitch2 = false;
            Main_to_dif.keyswitch = true;
            SceneManager.LoadScene("Scenes/MainMenu");
        }else if (sceneNames[currentIndex] == "rtmtt_1"){
            tutorstage.SetActive(false);
            keyswitch2 = false;
            // Main_to_dif.keyswitch = true;
            rtm_tutorial_script.passtage = 0;
            rtm_tutorial_script.stackpress = 0;
            SceneManager.LoadScene("Scenes/tutorial/rhythm/rtmtt_1");
        }else{
            SceneManager.LoadScene(sceneNames[currentIndex]);
        }
    }
    
}