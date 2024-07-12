using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rtm_tutorial_script : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip oksound; 
    public AudioClip[] listsound;
    public AudioClip[] ttsound;
    public AudioClip[] doubtsound; // กำหนด เสียงออก
    public bool kswitch = false;
    public bool kswitch2 = false;
    public static int passtage = 0;
    public static int stackpress = 0;

    // Start is called before the first frame update
    void Start(){
        // passtage = 0;
        kswitch = false;
        audioSource = GetComponent<AudioSource>();
        if (passtage == 0){
            StartCoroutine(platSound());
        }else if( passtage == 1){
            StartCoroutine(playtest());
        }else if( passtage == 2){
            StartCoroutine(platSound2());
        }else if( passtage == 4){
            StartCoroutine(platSound3());
        }

    }

    // Update is called once per frame
    void Update(){
        if(kswitch == true){
            CheckInput();
        }

        if(stackpress == 5){
            passtage = 2;
            SceneManager.LoadScene("Scenes/tutorial/rhythm/rtmtt_3");
            stackpress = 0;
            kswitch = false;
        }

        if (kswitch2 == true){
            if (Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKey(KeyCode.JoystickButton5)){ //R1
            SceneManager.LoadScene("Scenes/MainMenu");
            }
            if (Input.GetKeyDown(KeyCode.DownArrow)||Input.GetKey(KeyCode.JoystickButton4)){ //L1
                passtage = 0;
                stackpress = 0;
                SceneManager.LoadScene("Scenes/tutorial/rhythm/rtmtt_1");
            }
        }
    }
    
    void CheckInput(){
        if (Input.anyKeyDown){
            int clickedIndex = GetClickedIndexByKey();
            // audioSource.Stop();
            audioSource.PlayOneShot(doubtsound[clickedIndex]);
            if (clickedIndex == 1){
                stackpress += 1;
            }
        }
    }

    int GetClickedIndexByKey(){
        // Check which key is pressed and return the corresponding index
        if (Input.GetKeyDown(KeyCode.DownArrow)||Input.GetKey(KeyCode.JoystickButton0)){ //X
           return 1;
        }
        return -1; // Return -1 if no matching key is pressed
    }

    IEnumerator platSound()
    {
        yield return new WaitForSeconds(0);
        audioSource.PlayOneShot(ttsound[0]);
        yield return new WaitForSeconds(12);
        audioSource.PlayOneShot(ttsound[1]);
        yield return new WaitForSeconds(9);
        audioSource.PlayOneShot(ttsound[2]);
        yield return new WaitForSeconds(12);
        audioSource.PlayOneShot(ttsound[3]);
        yield return new WaitForSeconds(7);
        // kswitch = true;
        passtage = 1;
        SceneManager.LoadScene("Scenes/tutorial/rhythm/rtmtt_2");
    }

    IEnumerator playtest(){
        audioSource.PlayOneShot(doubtsound[0]);
        kswitch = true;
        yield return new WaitForSeconds(0.5f);
        kswitch = false;
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(playtest());

    }

    IEnumerator platSound2()
    {
        audioSource.PlayOneShot(ttsound[4]);
        yield return new WaitForSeconds(12);
        // kswitch = true;
        passtage = 3;
        rtm_game1.tutorstage = 1;
        rtm_game1.difficultstage = 0;
        SceneManager.LoadScene("Scenes/rhythm/easy/rtm1_e");
    }
    IEnumerator platSound3()
    {
        kswitch = false;
        kswitch2 = true;
        audioSource.PlayOneShot(ttsound[5]);
        yield return new WaitForSeconds(0);
        

    }
}

