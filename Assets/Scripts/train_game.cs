using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class train_game : MonoBehaviour
{
    public Text oktext;
    public Text wrtext;
    public Text foetext;
    public GameObject TrainMenu;
    public GameObject stopMenu;
    public GameObject scrMenu;
    public AudioClip[] soundClips; // สร้าง Array เก็บเสียง
    public AudioClip exdeath; //ตอนแพ้
    public AudioClip minecart; //รถราง
    public AudioClip ambient; //สภาพแวดล้อม
    public AudioClip trainpass; //เปลี่ยนฉาก
    public KeyCode[] keyCodes; // สร้าง Array เก็บปุ่ม

    private int score = 0; // คะแนน
    private int wrongAttempts = 0; // จำนวนครั้งที่กดผิด
    private AudioSource audioSource; // Component เสียง
    private bool isGameActive = true; // กำหนดว่าเกมยังคงเล่นหรือไม่
    private bool endGameRequested = false;
    public static int FinalScore; //คะแนนสุดท้าย
    public List<AudioClip> audioClips = new List<AudioClip>(); //เสียงเลข
    private AudioSource numberSource;

    private int thousands, hundreds, tens, units;

    private bool correctKeyPressed = false; // ตัวแปรเพื่อติดตามว่าปุ่มที่ถูกต้องถูกกดหรือไม่

    private int lastSoundIndex = -1; // ตัวแปรเพื่อเก็บดัชนีของเสียงที่เล่นล่าสุด
    private int consecutiveRepeats = 0; // ตัวแปรเพื่อเก็บจำนวนครั้งที่เสียงซ้ำกัน
    private float countdownTime; // เวลาที่นับถอยหลัง
    private bool countingDown = false; // ตัวแปรเพื่อตรวจสอบว่ากำลังนับถอยหลังหรือไม่
    public Text countdownText; //เก็บค่าเวลาที่นับ
    private Coroutine countdownCoroutine;
    private float remainingTime;  // เวลาที่เหลือ
    private bool isGameStarted = false; //เช็คการ pause

    private bool keyboardSwitch = false;

    public AudioSource soundtrain;
    public AudioSource soundnature;
    public string[] foestage = new string[] {"UP", "DOWN"};

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        numberSource = GetComponent<AudioSource>();

        soundtrain = GetComponents<AudioSource>()[1]; // ให้ audioSource2 เก็บ AudioSource ที่เป็นตัวที่ 2
        soundnature = GetComponents<AudioSource>()[2]; // ให้ audioSource3 เก็บ AudioSource ที่เป็นตัวที่ 3
    
        // countdownText.text = countdownTime.ToString("F0");
        StartCoroutine(PlayRandomSound());
        countdownTime = 3f; // กำหนดค่าเริ่มต้นของเวลา
        
    }

    private void Update()
    {
        if (isGameActive){
            if (score >= 200){
                SceneManager.LoadScene("Scenes/tr1_2");
                TrainScreen.totalscore += score; // กำหนดค่า score ให้กับ static variable
                audioSource.clip = trainpass;
                audioSource.Play();
            }
            if (keyboardSwitch == false){
                CheckInput();
            }           
        }

        //หน้าจอดำๆ
        // if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.Joystick1Button9)){ //Options
        //     scrMenu.SetActive(!scrMenu.activeSelf);
        // }

        //หน้าจอหยุดเกม
        if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.Joystick1Button9)){ //joy6 = L2
           if (isGameStarted){
                // If the game is already started, pause the game and show the stopMenu
                PauseGame();
                StopAudio(soundtrain);
                StopAudio(soundnature);
            }else{
                // If the game is not started, resume the game
                ResumeGame();
                PlayAudio(soundtrain);
                PlayAudio(soundnature);
            }
        }

        void PlayAudio(AudioSource audioSource)
        {
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        void StopAudio(AudioSource audioSource)
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    IEnumerator Countdown(float time)
    {
        countingDown = true;
        float timer = time;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            remainingTime = timer;
            countdownText.text = timer.ToString("F2"); // "F0" ใช้เพื่อไม่แสดงทศนิยม
            yield return null;
        }
        countingDown = false;
    }
    // เล่นเสียงตาม index ที่ระบุ
    public void PlaySound(int soundIndex){
        if (soundIndex >= 0 && soundIndex < audioClips.Count){
            if (audioClips[soundIndex] != null){
                numberSource.PlayOneShot(audioClips[soundIndex]);
            }
            else{
                Debug.LogError("AudioClip is null");
            }
        }else
        {
            Debug.LogError("Invalid sound index");
        }
    }
    IEnumerator PlayRandomSound(){
        // int consecutiveRepeats = 0; // ติดตามการทำซ้ำติดต่อกัน

        float delayTime = Random.Range(1f, 5f);
        while (isGameActive && !endGameRequested){
            yield return new WaitForSeconds(delayTime); // สุ่มเวลาก่อนที่จะเล่นเสียง
            // int randomIndex = Random.Range(0, soundClips.Length);            

            if (delayTime <= 2f){
                SceneManager.LoadScene("Scenes/tr1_ex");
                TrainScreen.totalscore += score; // กำหนดค่า score ให้กับ static variable

            }
            keyboardSwitch = false;
            int randomIndex;
            // เลือกดัชนีใหม่ที่ไม่ซ้ำกับเสียงที่เล่นล่าสุด
            do{
                randomIndex = Random.Range(0, soundClips.Length);
            } while (randomIndex == lastSoundIndex);

            // ตรวจสอบว่าเกินจำนวนครั้งที่กำหนดหรือไม่
            if (consecutiveRepeats == 3){
                // ถ้าเกินให้เลือกดัชนีใหม่ทันที
                consecutiveRepeats = 0;
                lastSoundIndex = -1;
                continue;
            }

            lastSoundIndex = randomIndex;

            audioSource.clip = soundClips[lastSoundIndex];
            foetext.text = "Foe : "+foestage[lastSoundIndex];
            // audioSource.clip = soundClips[randomIndex];
            audioSource.Play();
            // เริ่ม Coroutine และเก็บ reference ไว้ใน countdownCoroutine
            countdownCoroutine = StartCoroutine(Countdown(delayTime));

            yield return new WaitForSeconds(2f); // รอเล่นเสียง 2 วินาที

            if (!audioSource.isPlaying && !correctKeyPressed){
                // ผู้เล่นไม่กดเสียงทันเวลา
                wrongAttempts++;
                Debug.Log("Time's up! Wrong attempts: " + wrongAttempts);
                wrtext.text = wrongAttempts.ToString();
                keyboardSwitch = true;
            }
            
            if (wrongAttempts == 1){
                // เกมจบ
                EndGame();
                yield break;
            }

            // อัพเดทตัวนับการทำซ้ำติดต่อกัน
            consecutiveRepeats = (audioSource.clip != null && audioSource.clip == soundClips[randomIndex]) ? consecutiveRepeats + 1 : 0;

            if (correctKeyPressed == false){

                //ถ้าไม่กดปุ่มใดๆเลย
                wrongAttempts++;
                Debug.Log("Missed! Wrong attempts: " + wrongAttempts);
                wrtext.text = wrongAttempts.ToString();

                // เล่นเสียงที่ [3]
                AudioSource[] otherAudioSources = GetComponentsInChildren<AudioSource>();
                otherAudioSources[3].Play();

                // เมื่อต้องการหยุด Coroutine
                StopCoroutine(countdownCoroutine);

                //รีเซ็ต text
                countdownText.text = "READY";

                // รีเซ็ตเวลา
                countdownTime = 3f; // หรือค่าที่คุณต้องการให้เวลาเริ่มต้น

                if (wrongAttempts == 1)
                {
                    // เกมจบ
                    EndGame();
                }
            }
            // เพิ่มบรรทัดนี้เพื่อรีเซ็ตตัวแปรเมื่อผู้เล่นไม่กดปุ่มที่ถูกต้อง
            correctKeyPressed = false;
        }
    }

    private void CheckInput(){
        foreach (KeyCode keyCode in keyCodes)
        {
            if (Input.GetKeyDown(keyCode) && !correctKeyPressed)
            {
                if (audioSource.clip != null)
                {
                    int soundIndex = System.Array.IndexOf(keyCodes, keyCode);
                    if (soundIndex >= 0 && soundIndex < soundClips.Length)
                    {
                        if (audioSource.clip == soundClips[soundIndex])
                        {
                            // ผู้เล่นกดปุ่มที่ถูกต้อง
                            correctKeyPressed = true;
                            int texttime;
                            texttime = (int)(remainingTime * 10);
                            score += texttime;

                            keyboardSwitch = true;

                            // score++;
                            // score = score + 25;
                            Debug.Log("Correct! Score: " + score);
                            oktext.text = score.ToString();

                            // เล่นเสียงที่ [4]
                            AudioSource[] otherAudioSources = GetComponentsInChildren<AudioSource>();
                            otherAudioSources[4].Play();

                            // เมื่อต้องการหยุด Coroutine
                            StopCoroutine(countdownCoroutine);

                            //รีเซ็ต text
                            countdownText.text = "READY";

                            // รีเซ็ตเวลา
                            countdownTime = 3f; // หรือค่าที่คุณต้องการให้เวลาเริ่มต้น
                            
                        }
                        else
                        {
                            // ผู้เล่นกดเสียงที่ผิด
                            correctKeyPressed = true;
                            wrongAttempts++;
                            Debug.Log("Wrong! Wrong attempts: " + wrongAttempts);
                            wrtext.text = wrongAttempts.ToString();

                            // เล่นเสียงที่ [3]
                            AudioSource[] otherAudioSources = GetComponentsInChildren<AudioSource>();
                            otherAudioSources[3].Play();

                            keyboardSwitch = true;

                            // เมื่อต้องการหยุด Coroutine
                            StopCoroutine(countdownCoroutine);

                            //รีเซ็ต text
                            countdownText.text = "READY";

                            // รีเซ็ตเวลา
                            countdownTime = 3f; // หรือค่าที่คุณต้องการให้เวลาเริ่มต้น
                            
                        }
                    }
                }
            }
        }
    }
    private void EndGame()
    {
        TrainScreen.totalscore += score; // กำหนดค่า score ให้กับ static variable
        FinalScore = TrainScreen.totalscore; // กำหนดค่า score ให้กับ static variable
        isGameActive = false;
        endGameRequested = true; // เพิ่มตัวแปรเพื่อบอกว่าเกมจบแล้ว
        Debug.Log("Game Over. Final Score: " + score);
        TrainMenu.SetActive(true);

        // เมื่อต้องการหยุด Coroutine
        StopCoroutine(countdownCoroutine);

        // แสดงค่าเวลาที่เหลือ
        Debug.Log("Time Remaining: " + countdownTime);

        //เช็คคะแนนหลังเล่นจบ
        thousands = FinalScore / 1000;
        hundreds = (FinalScore % 1000) / 100;
        tens = (FinalScore % 100) / 10;
        units = FinalScore % 10;

        //หยุดทุกเสียง
        audioSource.Stop();

        // เล่นเสียง exdeath 1 ครั้ง
        audioSource.clip = exdeath;
        audioSource.Play();

        // หา AudioSource อื่น ๆ ใน GameObject นี้
        AudioSource[] otherAudioSources = GetComponentsInChildren<AudioSource>();

        // หยุดทุก AudioSource ที่ไม่ใช่ audioSource ที่เก็บ exdeath
        foreach (var audioSrc in otherAudioSources)
        {
            if (audioSrc.clip != exdeath)
            {
                audioSrc.Stop();
            }
        }
         StartCoroutine(WaitAndPlayRandomSound());
    }

    IEnumerator WaitAndPlayRandomSound()
    {
        yield return new WaitForSeconds(2.5f);
        PlaySound(13);
        yield return new WaitForSeconds(1.5f);
        if (FinalScore == 0){
            yield return new WaitForSeconds(1f);
            PlaySound(0);
            yield return new WaitForSeconds(1f);
            PlaySound(15);
        }else{
            StartCoroutine(PlaySoundsByDigits(thousands, hundreds, tens, units));
            yield return new WaitForSeconds(5f);
            PlaySound(15);
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

    private void PauseGame()
    {
        if (isGameActive)
        {
            isGameStarted = false;
            Time.timeScale = 0; // Pause the game
            stopMenu.SetActive(true); // Show the stopMenu
        }
    }

    private void ResumeGame()
    {
        isGameStarted = true;
        Time.timeScale = 1; // Resume the game
        stopMenu.SetActive(false); // Hide the stopMenu
    }

    
}