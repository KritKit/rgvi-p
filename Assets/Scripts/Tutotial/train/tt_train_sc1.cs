using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class tt_train_sc1 : MonoBehaviour
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
        yield return new WaitForSeconds(14);
        SceneManager.LoadScene("Scenes/tutorial/train/tt_train2");

    }

    IEnumerator texttime()
    {
        attext.text = "About Ore Cart.";
        yield return new WaitForSeconds(0.5f);
        attext.text = "About Ore Cart..";
        yield return new WaitForSeconds(0.5f);
        attext.text = "About Ore Cart...";
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
