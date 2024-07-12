using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class tt_train_sc3 : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip tutor1;
    public AudioClip oksound; 

    public Text attext;

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
        yield return new WaitForSeconds(12);
        SceneManager.LoadScene("Scenes/tutorial/train/tt_train4");

    }

    IEnumerator texttime()
    {
        attext.text = "About Foe.";
        yield return new WaitForSeconds(0.5f);
        attext.text = "About Foe..";
        yield return new WaitForSeconds(0.5f);
        attext.text = "About Foe...";
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
}
