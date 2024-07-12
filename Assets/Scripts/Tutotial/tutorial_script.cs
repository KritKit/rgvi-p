using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorial_script : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip oksound; 

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(platSound2());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.JoystickButton5)){ //R1
            StartCoroutine(platSound());          
        }
    }

    IEnumerator platSound()
    {
        audioSource.clip = oksound;
        audioSource.Play();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Scenes/MainMenu");

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
