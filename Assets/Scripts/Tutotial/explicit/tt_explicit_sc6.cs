using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class tt_explicit_sc6 : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip tutor1;
    public AudioClip oksound; 

    public Text attext;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(texttime());
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

    IEnumerator platSound()
    {
        audioSource.clip = oksound;
        audioSource.Play();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Scenes/MainMenu");

    }

    IEnumerator nextstage()
    {
        yield return new WaitForSeconds(9);
        explicit_game1_easy.tutomode = 1;
        explicit_game1_easy.difficultstage = 0;
        SceneManager.LoadScene("Scenes/tutorial/explicit/tt_exp1");

    }

    IEnumerator texttime()
    {
        attext.text = "About Explicit.";
        yield return new WaitForSeconds(0.5f);
        attext.text = "About Explicit..";
        yield return new WaitForSeconds(0.5f);
        attext.text = "About Explicit...";
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(texttime());

    }

    IEnumerator platSound2()
    {
        yield return new WaitForSeconds(9);
        audioSource.clip = oksound;
        audioSource.Play();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Scenes/tutorial/tt1");

    }

    IEnumerator nextstage2()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
