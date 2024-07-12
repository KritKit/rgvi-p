using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class tt_train_sc4 : MonoBehaviour
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
        StartCoroutine(texttime());
        StartCoroutine(nextstage());
        audioSource = GetComponent<AudioSource>();
        // StartCoroutine(platSound2());

        audioSource.clip = tutor1;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (countdownStarted == true)
        {
            countdownTimer -= Time.deltaTime;
            if (countdownTimer <= 0f)
            {
                countdownStarted = false;
                audioSource.clip = wrongsound;
                audioSource.Play();
                countdownTimer = 3f;
                StartCoroutine(nextstage2());
            }
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            if (countdownStarted == true)
            {
                // กดปุ่ม A ให้ทัน
                countdownStarted = false;
                countdownTimer = 3f;
                Debug.Log("Pressed A on time!");
                audioSource.clip = passsound;
                audioSource.Play();
                StartCoroutine(tonextstage());
            }
            else
            {
                // ไม่ใช่เวลาที่เหมาะสม
                Debug.Log("Pressed A too early!");
            }
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
        yield return new WaitForSeconds(5);
        countdownStarted = true;
        audioSource.clip = foeup;
        audioSource.Play();

    }

    IEnumerator nextstage2()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Scenes/tutorial/train/tt_train4");

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
        attext.text = countdownTimer.ToString("F2");
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(texttime());

    }
}
