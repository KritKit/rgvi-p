using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class tt_train_sc8 : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip tutor1;
    public AudioClip oksound; 
    public AudioClip passsound; 
    public AudioClip wrongsound; 
    public AudioClip foeup; 

    public Text attext;

    public AudioClip soundToPlay;
    private bool countdownStarted = false;
    public float countdownTimer = 3f;

    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(texttime());
        // StartCoroutine(nextstage());
        audioSource = GetComponent<AudioSource>();
        // StartCoroutine(platSound2());

        audioSource.clip = tutor1;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton4)){
            audioSource.clip = oksound;
            audioSource.Play();
            StartCoroutine(nextstage());
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton5)){
            audioSource.clip = oksound;
            audioSource.Play();
            StartCoroutine(nextstage2());
        }

    }

    public void PlaySoundAndStartCountdown()
    {
        audioSource.PlayOneShot(soundToPlay);
        countdownStarted = true;
    }

    void Endgame()
    {
        // ทำงาน Endgame() ที่นี่
        Debug.Log("Game Over!");
    }


    IEnumerator nextstage()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Scenes/tutorial/train/tt_train1");
    }

    IEnumerator nextstage2()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    IEnumerator tonextstage()
    {
        yield return new WaitForSeconds(1);
        audioSource.clip = oksound;
        audioSource.Play();
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("Scenes/tutorial/train/tt_train5");

    }

    IEnumerator texttime()
    {
        attext.text = "About Break Cart.";
        yield return new WaitForSeconds(0.5f);
        attext.text = "About Break Cart..";
        yield return new WaitForSeconds(0.5f);
        attext.text = "About Break Cart...";
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(texttime());

    }
}
