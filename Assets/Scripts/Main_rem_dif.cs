using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_rem_dif : MonoBehaviour
{

    public Text[] menuOptions;
    public GameObject[] BgOptions;
    public GameObject remnrtmdifmenu;
    public GameObject mainmenu;
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

    public static bool rem_keyswitch1 = true;
    public static bool rem_keyswitch2 = true;
    public GameObject expdifstage;
    public GameObject rtmdifstage;
    public Vector2 newSize = new Vector2(200, 70); // ขนาดใหม่ที่คุณต้องการเพิ่ม
    public Vector2 originalSize = new Vector2(1, 5); // ขนาดเดิมของเมนู
    public GameObject audioObject;


    void Start()
    {
        UpdateMenuHighlight();
        rem_keyswitch1 = true;
    }

    void Update()
    {
        if (rem_keyswitch1 == true || rem_keyswitch2 == true)
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
        }
    }

    void UpdateMenuHighlight()
    {
        for (int i = 0; i < menuOptions.Length; i++)
        {
            menuOptions[i].fontStyle    = (i == currentIndex) ? FontStyle.Bold : FontStyle.Normal;
            menuOptions[i].fontSize     = (i == currentIndex) ? 75 : 72;
            menuOptions[i].color        = (i == currentIndex) ? Color.white : Color.blue;

            // BgOptions[i].GetComponent<RectTransform>().sizeDelta = (i == currentIndex) ? newSize : originalSize;
            BgOptions[i].GetComponent<RectTransform>().sizeDelta = (i == currentIndex) ? new Vector2(249, 41) : new Vector2(249, 41);
            BgOptions[i].GetComponent<Image>().color = (i == currentIndex) ? new Color32(255, 0, 0, 255) : new Color32(255, 255, 255, 255);

            // menuOptions[i].alignment    = (i == currentIndex) ? TextAnchor.MiddleCenter : TextAnchor.MiddleLeft;
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
            AudioSource audioSource = FindObjectOfType<AudioSource>();
            audioSource.Stop();
            Main_to_mode.keyswitch2 = true;
            rem_keyswitch1 = false;
            remnrtmdifmenu.SetActive(false);
            mainmenu.SetActive(true);
        }else if (sceneNames[currentIndex] == "ep1_1_e"){
            explicit_game1_easy.difficultstage = 0;
            explicit_game1_easy.stagestatus = 0;
            AudioSource audioSource = FindObjectOfType<AudioSource>();
            audioSource.Stop();
            SceneManager.LoadScene(sceneNames[currentIndex]);         
        }else if (sceneNames[currentIndex] == "ep1_1_n"){
            explicit_game1_easy.difficultstage = 1;
            explicit_game1_easy.stagestatus = 0;
            AudioSource audioSource = FindObjectOfType<AudioSource>();
            audioSource.Stop();
            SceneManager.LoadScene(sceneNames[currentIndex]);
        }else if (sceneNames[currentIndex] == "ep1_1_h"){
            explicit_game1_easy.difficultstage = 2;
            explicit_game1_easy.stagestatus = 0;
            AudioSource audioSource = FindObjectOfType<AudioSource>();
            audioSource.Stop();
            SceneManager.LoadScene(sceneNames[currentIndex]);
        }else if (sceneNames[currentIndex] == "rtm1_e"){
            rtm_game1.difficultstage = 0;
            rtm_game1.stagestatus = 0;
            rtm_game1.passstage = 0;
            rtm_game1.tutorstage = 0;
            SceneManager.LoadScene(sceneNames[currentIndex]);
        }else if (sceneNames[currentIndex] == "rtm1_n"){
            rtm_game1.difficultstage = 1;
            rtm_game1.stagestatus = 0;
            rtm_game1.passstage = 0;
            rtm_game1.tutorstage = 0;
            SceneManager.LoadScene(sceneNames[currentIndex]);
        }else if (sceneNames[currentIndex] == "rtm1_h"){
            rtm_game1.difficultstage = 2;
            rtm_game1.stagestatus = 0;
            rtm_game1.passstage = 0;
            rtm_game1.tutorstage = 0;
            SceneManager.LoadScene(sceneNames[currentIndex]);
        }else{
            SceneManager.LoadScene(sceneNames[currentIndex]);
        }
    }
    
}