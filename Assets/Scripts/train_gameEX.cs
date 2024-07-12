using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class train_gameEX : MonoBehaviour
{
    public GameObject TrainMenu;
    public GameObject stopMenu;

    public AudioClip exdeath; // เสียงที่จะเล่นเมื่อเกมส์จบ
    public AudioClip hornsound; // เสียงแตร
    public AudioClip carsound; // เสียงรถผ่าน
    public Text textl1;
    public Text textr1;
    public Text timerText;
    public Text dangerText;
    public Color targetColor;
    private Color originalColor;
    public AudioClip changeColorSound; // เสียงที่จะเล่นเมื่อ Text เปลี่ยนสี
    private AudioSource audioSource;
    private bool isKeyPressedA;
    private bool isKeyPressedD;
    private bool isPlayingSound;
    private int thousands, hundreds, tens, units;
    private int score = 0; // คะแนน
    public static int FinalScore; //คะแนนสุดท้าย
    public List<AudioClip> audioClips = new List<AudioClip>(); //เสียงเลข
    public AudioSource numberSource; // AudioSource สำหรับเล่นเสียงเลข
    public int keystatus = 0; // สถานะการกดปุ่ม
    public int carclear = 0; // สถานะการผ่านรถ
    public bool keyboardSwitch = true; // สถานะการเปิดปิดคีย์บอร์ด
    private bool isGameActive = true; // กำหนดว่าเกมยังคงเล่นหรือไม่
    private bool isGameStarted = false; //เช็คการ pause

    void Start()
    {
        originalColor = textl1.color;
        audioSource = GetComponent<AudioSource>(); // เรียกใช้ AudioSource จาก GameObject ของคุณ
        numberSource = GetComponent<AudioSource>();

        isPlayingSound = false;
        StartCoroutine(StartCountdown());
    }

    void Update()
    {
        // กดปุ่ม A และ D พร้อมกัน
        // if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.D))
        // {
        //     StartCoroutine(ChangeTextColor());
        // }

        //หน้าจอหยุดเกม
        if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.Joystick1Button9)){ //joy6 = L2
           if (isGameStarted){
                // If the game is already started, pause the game and show the stopMenu
                PauseGame();

            }else{
                // If the game is not started, resume the game
                ResumeGame();

            }
        }

        if (timerText.text == "0" && keystatus == 0){
            if (carclear == 0){
                EndGame();
                keystatus = 1;
            }else if (carclear == 1){
                int randomIndex = Random.Range(0, 2);
                if (randomIndex == 0){
                    SceneManager.LoadScene("Scenes/tr1_1");
                }else if (randomIndex == 1){
                    SceneManager.LoadScene("Scenes/tr1_2");               
                }
            }
        }
        if (timerText.text == "0"){
            if (keystatus == 1){
                StartCoroutine(waitforcarpass());
            }          
        }

        if (keyboardSwitch == true){
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.JoystickButton4))
            {
                isKeyPressedA = true;

                if (isKeyPressedD)
                {
                    StartCoroutine(ChangeTextColor());
                }
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.JoystickButton5))
            {
                isKeyPressedD = true;

                if (isKeyPressedA)
                {
                    StartCoroutine(ChangeTextColor());
                }
            }

            // ปล่อยปุ่ม A หรือ D
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.JoystickButton4) || Input.GetKeyUp(KeyCode.JoystickButton5))
            {
                textl1.color = originalColor;
                textr1.color = originalColor;
                isKeyPressedA = false;
                isKeyPressedD = false;

                // หยุดเล่นเสียง
                isPlayingSound = false;
                audioSource.Stop();
                keystatus = 0;
            }
        }
    }

    IEnumerator ChangeTextColor()
    {
        isKeyPressedA = true;
        isKeyPressedD = true;

        while (isKeyPressedA && isKeyPressedD)
        {
            keystatus = 1;
            // เปลี่ยนสี Text เป็นสีแดง
            textl1.color = Color.red;
            textr1.color = Color.red;

            // เริ่มเล่นเสียงเมื่อยังไม่ได้เล่นอยู่
            if (!isPlayingSound)
            {
                isPlayingSound = true;
                audioSource.PlayOneShot(changeColorSound);
                StartCoroutine(PlayCarSoundWithDelay(5.0f));              

            }

            yield return null;
        }
        
    }

    IEnumerator waitforcarpass(){
        yield return new WaitForSeconds(12);
        dangerText.color = Color.green;
        dangerText.text = "OK";
        carclear = 1;
    }

    IEnumerator StartCountdown()
    {
        // นับถอยหลังเป็นเวลา 3 วินาที
        float countdown = 3f;
        while (countdown > 0)
        {
            // อัปเดตข้อความบน timerText
            timerText.text = Mathf.CeilToInt(countdown).ToString();

            // ลดเวลา
            countdown -= Time.deltaTime;

            yield return null;
        }

        // เมื่อนับถอยหลังเสร็จสิ้น ซ่อน timerText
        timerText.color = Color.gray;
        timerText.text = "0";
        // timerText.gameObject.SetActive(false);

    }

    IEnumerator PlayCarSoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.PlayOneShot(carsound);
    }

    private void EndGame()
    {
        TrainScreen.totalscore += score; // กำหนดค่า score ให้กับ static variable
        FinalScore = TrainScreen.totalscore; // กำหนดค่า score ให้กับ static variable
        keyboardSwitch = false;
        Debug.Log("Game Over. Final Score: " + score);
        TrainMenu.SetActive(true);

        StopCoroutine(StartCountdown());
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
            yield return new WaitForSeconds(3f);
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
