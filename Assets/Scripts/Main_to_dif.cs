using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_to_dif : MonoBehaviour
{

    public Text[] menuOptions;
    public GameObject[] BgOptions;
    public GameObject tutormenu;
    public GameObject startmenu;
    public GameObject mainmenu;
    public GameObject stopmenu;
    public int currentIndex = 0;
    public string[] sceneNames;
    public AudioSource menuAudioSource;

    // เพิ่มตัวแปร AudioClip สำหรับแต่ละเมนู
    public AudioClip menuSe;
    public AudioClip menuOk;
    public static AudioClip menuSelectSound0;
    public AudioClip menuSelectSound1;
    public AudioClip menuSelectSound2;
    public AudioClip menuSelectSound3;
    public AudioClip menuSelectSound4;
    public AudioClip menuSelectSound5;
    public AudioClip menuSelectSound6;

    public static bool keyswitch = true;
    public GameObject expdifstage;
    public GameObject rtmdifstage;
    public Vector2 newSize = new Vector2(200, 70); // ขนาดใหม่ที่คุณต้องการเพิ่ม
    public Vector2 originalSize = new Vector2(1, 5); // ขนาดเดิมของเมนู
    public GameObject audioObject;

    public bool isGameActive = true;


    void Start()
    {
        UpdateMenuHighlight();
        keyswitch = true;
        startmenu.SetActive(true);
    }

    void Update()
    {
        if (keyswitch == true)
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
            
           
        }else if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.JoystickButton9)){ //joy6 = L2
           if (isGameActive){
                PauseGame();
            }else{
                ResumeGame();
            }
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
            case 5:
                if (menuSelectSound3 != null)
                    menuAudioSource.PlayOneShot(menuSelectSound6);
                break;
            // เพิ่ม case เพิ่มได้ตามจำนวนเมนู
            // ...
        }
    }

    void UpdateMenuHighlight(){
        for (int i = 0; i < menuOptions.Length; i++){
            menuOptions[i].fontStyle    = (i == currentIndex) ? FontStyle.Bold : FontStyle.Normal;
            menuOptions[i].fontSize     = (i == currentIndex) ? 75 : 72;
            menuOptions[i].color        = (i == currentIndex) ? Color.white : Color.blue;
            BgOptions[i].GetComponent<RectTransform>().sizeDelta = (i == currentIndex) ? new Vector2(249, 41) : new Vector2(249, 41);
            BgOptions[i].GetComponent<Image>().color = (i == currentIndex) ? new Color32(255, 0, 0, 255) : new Color32(255, 255, 255, 255);
        }
    }
    IEnumerator WaitForScene(){
        yield return new WaitForSeconds(1f);
        LoadScene();
    }
    void LoadScene(){
        if (sceneNames[currentIndex] == "tutor"){
            AudioSource audioSource = FindObjectOfType<AudioSource>();
            audioSource.Stop();
            startmenu.SetActive(false);
            tutormenu.SetActive(true);
            keyswitch = false;
            Main_to_tutor.keyswitch2 = true;
        }else if (sceneNames[currentIndex] == "start"){
            AudioSource audioSource = FindObjectOfType<AudioSource>();
            audioSource.Stop();
            startmenu.SetActive(false);
            mainmenu.SetActive(true);
            keyswitch = false;
        }else{
            SceneManager.LoadScene(sceneNames[currentIndex]);
        }
    }

    private void PauseGame()
    {
        if (isGameActive)
        {
            isGameActive = false;
            Time.timeScale = 0; // Pause the game
            stopmenu.SetActive(true); // Show the stopMenu
        }
    }
    private void ResumeGame()
    {
        isGameActive = true;
        Time.timeScale = 1; // Resume the game
        stopmenu.SetActive(false); // Hide the stopMenu
    }
    
}