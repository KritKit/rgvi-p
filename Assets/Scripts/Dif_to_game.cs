using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dif_to_game : MonoBehaviour
{

    public Text[] difOptions;
    public int currentIndex = 0;
    public string[] sceneNames;
    public AudioSource difAudioSource;

    // เพิ่มตัวแปร AudioClip สำหรับแต่ละเมนู
    public AudioClip difSe;
    public AudioClip difOk;
    public AudioClip difSelectSound1;
    public AudioClip difSelectSound2;
    public AudioClip difSelectSound3;


    void Start()
    {
        UpdateDifHighlight();
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            MoveSelection(-1);
            difAudioSource.PlayOneShot(difSe);

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            MoveSelection(1);
            difAudioSource.PlayOneShot(difSe);
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            difAudioSource.PlayOneShot(difOk);
            StartCoroutine(WaitForScene());
        }
    }

    void MoveSelection(int direction)
    {
        currentIndex = (currentIndex + direction + difOptions.Length) % difOptions.Length;
        UpdateDifHighlight();

        difAudioSource.Stop();
        switch (currentIndex)
        {
            case 0:
                if (difSelectSound1 != null)
                    difAudioSource.PlayOneShot(difSelectSound1);
                break;
            case 1:
                if (difSelectSound2 != null)
                    difAudioSource.PlayOneShot(difSelectSound2);
                break;
            case 2:
                if (difSelectSound3 != null)
                    difAudioSource.PlayOneShot(difSelectSound3);
                break;
            // เพิ่ม case เพิ่มได้ตามจำนวนเมนู
            // ...
        }
    }

    void UpdateDifHighlight()
    {
        for (int i = 0; i < difOptions.Length; i++)
        {
            difOptions[i].fontStyle = (i == currentIndex) ? FontStyle.Bold : FontStyle.Normal;
            difOptions[i].fontSize = (i == currentIndex) ? 80 : 56;
        }
    }
    IEnumerator WaitForScene()
    {
        yield return new WaitForSeconds(1f);
        LoadScene();
    }
    void LoadScene()
    {
        SceneManager.LoadScene(sceneNames[currentIndex]);
    }
}