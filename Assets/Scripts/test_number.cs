using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test_number : MonoBehaviour
{
    public Text countdownText;
    
    public List<AudioClip> audioClips = new List<AudioClip>(); //เสียงเลข
    AudioSource audioSource;
    private int mtens, tens, units;
    private float timeRemaining = 298; // 5 minutes in seconds

    public int FinalScore;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        FinGame();
        // DisplayTime(timeRemaining);
    }
    void Update()
    {
        // if (timeRemaining > 0)
        // {
        //     timeRemaining -= Time.deltaTime;
        //     DisplayTime(timeRemaining);
        // }
        // else
        // {
        //     countdownText.text = "Time's up!";
        // }

        
    }

    void FinGame(){
        FinalScore = (int)timeRemaining;

        //เช็คคะแนนหลังเล่นจบ
        mtens = FinalScore / 60;
        tens = (FinalScore % 60) / 10;
        units = (FinalScore % 60) % 10;

        StartCoroutine(WaitAndPlayRandomSound());
    }
    void DisplayTime(float timeToDisplay)
    {
        // float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        // float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator WaitAndPlayRandomSound()
    {
        if (FinalScore == 0){
            yield return new WaitForSeconds(1f);
            PlaySound(0);
        }else{
            StartCoroutine(PlaySoundsByDigits(mtens, tens, units));
            yield return new WaitForSeconds(5f);
            PlaySound(15);

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
    IEnumerator PlaySoundsByDigits(int mtens, int tens, int units)
    {

        if (mtens > 0)
        {
            // if (tens == 2){
            //     yield return new WaitForSeconds(1f);
            //     PlaySound(14);
            //     yield return new WaitForSeconds(0.5f);
            //     PlaySound(12);
            // }else if (tens == 1){
            //     yield return new WaitForSeconds(0.5f);
            //     PlaySound(12);
            // }else{
                yield return new WaitForSeconds(1f);
                PlaySound(mtens);
                yield return new WaitForSeconds(1f);
                PlaySound(13);
            // }
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



        // if (tens != 0){
        //     //เล่นเสียง tens
        //     //สิบ
        //     if(units != 0){
        //         //เล่นเสียงหลักหน่วย
        //     }else{

        //     }
        // }else {

        //     if(units != 0) {
        //         //เล่นเสียงหลักหน่วย
        //     }
        // }
    }
}