using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class explicit_score : MonoBehaviour
{
    public Text pointsText;

    public static int allscorestage = 0; //รวมคะแนนทุกด่าน
    int scoreff;
    public List<AudioClip> audioClips = new List<AudioClip>(); //เสียงเลข
    AudioSource audioSource;
    public static int FinalScore; //คะแนนสุดท้าย
    private int thousands, hundreds, tens, units;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.JoystickButton4)){ //L1
            SceneManager.LoadScene("Scenes/MainMenu");
        }
    }
    // Use Awake or Start for initialization
    void Awake()
    {
        scoreff = allscorestage;
        pointsText.text = scoreff.ToString() + " Points";

        FinalScore = allscorestage;
        //เช็คคะแนนหลังเล่นจบ
        thousands = FinalScore / 1000;
        hundreds = (FinalScore % 1000) / 100;
        tens = (FinalScore % 100) / 10;
        units = FinalScore % 10;
        StartCoroutine(WaitAndPlayRandomSound());
    }

    IEnumerator WaitAndPlayRandomSound()
    {
        yield return new WaitForSeconds(0f);
        PlaySound(16);
        yield return new WaitForSeconds(1.5f);
        if (FinalScore == 0){
            yield return new WaitForSeconds(1f);
            PlaySound(0);
            yield return new WaitForSeconds(1f);
            PlaySound(15);
        }else{
            StartCoroutine(PlaySoundsByDigits(thousands, hundreds, tens, units));
            yield return new WaitForSeconds(5.0f);
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
            yield return new WaitForSeconds(0.5f);
            PlaySound(hundreds);
            yield return new WaitForSeconds(0.5f);
            PlaySound(11);
        }

        // เล่นเสียงตามหลักสิบ
        if (tens > 0)
        {
            if (tens == 2){
                yield return new WaitForSeconds(0.5f);
                PlaySound(14);
                yield return new WaitForSeconds(0.5f);
                PlaySound(12);
            }else if (tens == 1){
                yield return new WaitForSeconds(0.5f);
                PlaySound(12);
            }else{
                yield return new WaitForSeconds(0.5f);
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
