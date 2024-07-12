using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class rtm_game_all : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>(); //เสียงเลข
    AudioSource audioSource;
    public Text scoretext;

    public static int resultscore = 0;
    private int thousands, hundreds, tens, units;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        stagefinalend();
    }

    // Update is called once per frame
    void Update()
    {
        scoretext.text = resultscore.ToString() + " / 600";

        if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.JoystickButton4)){ //L1
            SceneManager.LoadScene("Scenes/MainMenu");
        }
    }

    private void stagefinalend()
    {
        
        thousands = resultscore / 1000;
        hundreds = (resultscore % 1000) / 100;
        tens = (resultscore % 100) / 10;
        units = resultscore % 10;

        StartCoroutine(WaitAndPlayRandomSound());
        StartCoroutine(afterscore(1));
    }

    IEnumerator afterscore(int status)
    {
        if (status == 1){
            yield return new WaitForSeconds(6.5f);
            PlaySound(15);
            yield return new WaitForSeconds(2.5f);
            PlaySound(16);
        }
    }
    IEnumerator WaitAndPlayRandomSound()
    {
        PlaySound(13);
        yield return new WaitForSeconds(1.5f);
        if (resultscore == 0){
            yield return new WaitForSeconds(1f);
            PlaySound(0);

        }else{
            StartCoroutine(PlaySoundsByDigits(thousands, hundreds, tens, units));

        }
    }
    public void PlaySound(int soundIndex){
        if (soundIndex >= 0 && soundIndex < audioClips.Count){
            if (audioClips[soundIndex] != null){
                audioSource.PlayOneShot(audioClips[soundIndex]);
            }
            else{
                Debug.LogError("AudioClip is null");
                }
        }else
        {
            Debug.LogError("Invalid sound index");
        }
    }

    IEnumerator PlaySoundsByDigits(int thousands, int hundreds, int tens, int units)
    {
        // เล่นเสียงตามหลักพัน
        if (thousands > 0)
        {
            yield return new WaitForSeconds(1f);
            PlaySound(thousands);
            yield return new WaitForSeconds(0.5f);
            PlaySound(10);
        }

        // เล่นเสียงตามหลักร้อย
        if (hundreds > 0)
        {
            yield return new WaitForSeconds(1f);
            PlaySound(hundreds);
            yield return new WaitForSeconds(0.5f);
            PlaySound(11);
        }

        // เล่นเสียงตามหลักสิบ
        if (tens > 0)
        {
            if (tens == 2){
                yield return new WaitForSeconds(1f);
                PlaySound(14);
                yield return new WaitForSeconds(0.5f);
                PlaySound(12);
            }else if (tens == 1){
                yield return new WaitForSeconds(0.5f);
                PlaySound(12);
            }else{
                yield return new WaitForSeconds(1f);
                PlaySound(tens);
                yield return new WaitForSeconds(0.5f);
                PlaySound(12);
            }
        }

        // เล่นเสียงตามหลักหน่วย
        if (units > 0)
        {
            yield return new WaitForSeconds(0.5f);
            PlaySound(units);
        }
    }
}
