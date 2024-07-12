using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorial_script1 : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip oksound; 
    public AudioClip[] listsound;
    public AudioClip[] ttsound;
    public bool kswitch = false;
    public static int passtage = 0;
    public bool isGameActive = true;
    public GameObject stopMenu;
    public bool kswitch2 = true;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        passtage = 0;
        StartCoroutine(platSound());
    }

    // Update is called once per frame
    void Update()
    {
        if(kswitch == true){
            if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.DownArrow)) //X
            {            
                audioSource.Stop();
                audioSource.PlayOneShot(listsound[0]); // เล่นเสียง
                if (passtage == 0)
                {
                    kswitch = false;
                    passtage = 1;
                    StartCoroutine(platSound2());
                }
                if (passtage == 2)
                {
                    kswitch = false;
                    passtage = 3;
                    StartCoroutine(platSound3());
                }

            }

            if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.UpArrow)) //O
            {
                audioSource.Stop();
                audioSource.PlayOneShot(listsound[1]); // เล่นเสียง
            }

            if (Input.GetKeyDown(KeyCode.JoystickButton2)) //Square
            {
                audioSource.Stop();
                audioSource.PlayOneShot(listsound[2]); // เล่นเสียง
            }

            if (Input.GetKeyDown(KeyCode.JoystickButton3)) //Triangle
            {
                audioSource.Stop();
                audioSource.PlayOneShot(listsound[3]); // เล่นเสียง
            }

            if (Input.GetKeyDown(KeyCode.JoystickButton4) || Input.GetKeyDown(KeyCode.LeftArrow)) //L1
            {
                audioSource.Stop();
                audioSource.PlayOneShot(listsound[4]); // เล่นเสียง
                if (passtage == 3)
                {
                    kswitch = false;
                    StartCoroutine(platSound4());
                }
                if (passtage == 4)
                {
                    kswitch = false;
                    StartCoroutine(platSound5());
                }
            }

            if (Input.GetKeyDown(KeyCode.JoystickButton5) || Input.GetKeyDown(KeyCode.RightArrow)) //R1
            {
                audioSource.Stop();
                audioSource.PlayOneShot(listsound[5]); // เล่นเสียง
                if (passtage == 5)
                {
                    kswitch = false;
                    StartCoroutine(platSound6());
                }
                if (passtage == 6)
                {
                    kswitch = false;
                    StartCoroutine(platSound7());
                }
            }

            if (Input.GetKeyDown(KeyCode.JoystickButton6)) //L2
            {
                audioSource.Stop();
                audioSource.PlayOneShot(listsound[6]); // เล่นเสียง
            }

            if (Input.GetKeyDown(KeyCode.JoystickButton7)) //R2
            {
                audioSource.Stop();
                audioSource.PlayOneShot(listsound[7]); // เล่นเสียง
            }
        }
    }

    IEnumerator platSound()
    {
        audioSource.PlayOneShot(ttsound[0]);
        yield return new WaitForSeconds(12);
        audioSource.PlayOneShot(ttsound[1]);
        yield return new WaitForSeconds(14);
        audioSource.PlayOneShot(ttsound[2]);
        yield return new WaitForSeconds(7);
        kswitch = true;

    }

    IEnumerator platSound2()
    {
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(oksound);
        yield return new WaitForSeconds(1.5f);
        audioSource.PlayOneShot(ttsound[3]);
        yield return new WaitForSeconds(10);
        passtage = 2;
        kswitch = true;

    }

    //บทสอนเล่นL1
    IEnumerator platSound3()
    {
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(oksound);
        yield return new WaitForSeconds(1.5f);
        audioSource.PlayOneShot(ttsound[4]);
        yield return new WaitForSeconds(12);
        kswitch = true;

    }
    //บทสอนเล่นL1 (2)
    IEnumerator platSound4()
    {
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(oksound);
        yield return new WaitForSeconds(1.5f);
        audioSource.PlayOneShot(ttsound[5]);
        yield return new WaitForSeconds(10);
        passtage = 4;
        kswitch = true;

    }
    //บทสอนเล่นR1
    IEnumerator platSound5()
    {
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(oksound);
        yield return new WaitForSeconds(1.5f);
        audioSource.PlayOneShot(ttsound[6]);
        yield return new WaitForSeconds(11);
        passtage = 5;
        kswitch = true;

    }
    //บทสอนเล่นR1 (2)
    IEnumerator platSound6()
    {
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(oksound);
        yield return new WaitForSeconds(1.5f);
        audioSource.PlayOneShot(ttsound[7]);
        yield return new WaitForSeconds(10);
        passtage = 6;
        kswitch = true;

    }
    //จบบทสอนเล่น
    IEnumerator platSound7()
    {
        passtage = 7;
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(oksound);
        yield return new WaitForSeconds(1.5f);
        audioSource.PlayOneShot(ttsound[8]);
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    private void PauseGame()
    {
        if (isGameActive)
        {
            isGameActive = false;
            Time.timeScale = 0; // Pause the game
            stopMenu.SetActive(true); // Show the stopMenu
        }
    }
    private void ResumeGame()
    {
        isGameActive = true;
        Time.timeScale = 1; // Resume the game
        stopMenu.SetActive(false); // Hide the stopMenu
    }
}
