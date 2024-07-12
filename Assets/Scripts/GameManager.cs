using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject ScoreMenu;
    public Text LScoreText;
    public ScoreScreen ScoreScreen;
    public AudioSource theMusic;
    public AudioSource[] soundeff;

    public bool startPlaying;

    public BeatScroller theBS;

    public static GameManager instance;

    public int currentScore;
    public int scorePerNote = 20;
    public int scoreGoodNote = 20;
    public int scorePerPerfectNote = 30;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;


    public Text scoreText;
    public Text multiText;

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public int maxscore = 0;

    public bool isPaused = false;
    public Main_to_dif main_to_dif;

    public Text timerText; // อ้างอิงไปยัง Text UI สำหรับแสดงเวลา

    private float timeElapsed = 0f; // เวลาที่ผ่านไป
    private bool timerIsRunning = true;
    private AudioSource audioSource;

    public List<AudioClip> audioClips = new List<AudioClip>(); //เสียงเลข
    public AudioSource numberSource; // AudioSource สำหรับเล่นเสียงเลข
    private int thousands, hundreds, tens, units;
    public static int FinalScore; //คะแนนสุดท้าย
    public Text pointsText;
    public bool switchscore = true;
    public bool keyboardswitch = true;
    bool isButtonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        instance = this;
        scoreText.text = "Score : 0";
        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length;
        ScoreScreen = GetComponent<ScoreScreen>();
        audioSource = GetComponent<AudioSource>();
        numberSource = GetComponent<AudioSource>();

        if(!startPlaying){ 
            startPlaying = true;
            theBS.hasStarted = true;

            theMusic.Play();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (keyboardswitch == true){
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKey(KeyCode.JoystickButton6))
            {
                if (!isButtonPressed)
                {
                    PlaySound(0); // เล่นเสียงที่ index 0
                    isButtonPressed = true;
                }
            }
            else if (Input.GetKeyDown(KeyCode.X) || Input.GetKey(KeyCode.JoystickButton4))
            {
                if (!isButtonPressed)
                {
                    PlaySound(1); // เล่นเสียงที่ index 0
                    isButtonPressed = true;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton0))
            {
                if (!isButtonPressed)
                {
                    PlaySound(2); // เล่นเสียงที่ index 0
                    isButtonPressed = true;
                }
            }
            else if (Input.GetKeyDown(KeyCode.C) || Input.GetKey(KeyCode.JoystickButton5))
            {
                if (!isButtonPressed)
                {
                    PlaySound(3); // เล่นเสียงที่ index 0
                    isButtonPressed = true;
                }
            }
            else if (Input.GetKeyDown(KeyCode.V) || Input.GetKey(KeyCode.JoystickButton7))
            {
                if (!isButtonPressed)
                {
                    PlaySound(4); // เล่นเสียงที่ index 0
                    isButtonPressed = true;
                }
            }
            else
            {
                isButtonPressed = false;
            }

            if (Input.GetKeyUp(KeyCode.JoystickButton6)){
                isButtonPressed = false;
            }
            if (Input.GetKeyUp(KeyCode.JoystickButton4)){
                isButtonPressed = false;
            }
            if (Input.GetKeyUp(KeyCode.JoystickButton0)){
                isButtonPressed = false;
            }
            if (Input.GetKeyUp(KeyCode.JoystickButton5)){
                isButtonPressed = false;
            }
            if (Input.GetKeyUp(KeyCode.JoystickButton7)){
                isButtonPressed = false;
            }
        }

        if (timerIsRunning)
        {
            timeElapsed += Time.deltaTime;
            UpdateTimerUI();
        }

        if(!startPlaying){
            if(Input.anyKeyDown){
                startPlaying = true;
                theBS.hasStarted = true;

                theMusic.Play();

            }
        }

        if(totalNotes == (normalHits+goodHits+perfectHits+missedHits)){
            if (switchscore){
                EndGame();
                switchscore = false;
            }
            // SceneManager.LoadScene("Scenes/tr1_1");

        }
        
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // รอการโหลดฉากเสร็จสิ้น
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");

            yield return null;
        }
    }
   
    public void NoteHit(){
        Debug.Log("Hit On Time");

        if(currentMultiplier - 1 < multiplierThresholds.Length){
            multiplierTracker++;

            if(multiplierThresholds[currentMultiplier - 1] <= multiplierTracker){
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        multiText.text = "Multiplier : x"+currentMultiplier;

        // currentScore += scorePerNote * currentMultiplier;

        //UI
        scoreText.text = "Score : " + currentScore;
    }

    public void NormalHit(){
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();

        normalHits++;
    }

    public void GoodHit(){
        currentScore += scoreGoodNote * currentMultiplier;
        NoteHit();

        goodHits++;
    }

    public void PerfectHit(){
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();

        perfectHits++;
    }

    public void NoteMissed(){
        Debug.Log("Missed Note");

        currentMultiplier = 1;
        multiplierTracker = 0;

        multiText.text = "Multiplier : x"+currentMultiplier;

        missedHits++;

    }

    void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;

        if (isPaused)
            {
                theMusic.Pause();
            }
            else
            {
                theMusic.UnPause();
        }
    }

    void UpdateTimerUI()
    {
        // แปลงเวลาจากวินาทีเป็น นาที:วินาที
        float minutes = Mathf.Floor(timeElapsed / 60f);
        float seconds = Mathf.Floor(timeElapsed % 60f);
        float milliseconds = Mathf.Floor((timeElapsed * 1000f) % 1000f);
        string timerString = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        // string timerString = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("000");

        // แสดงผลลงใน Text UI
        timerText.text = "Time: " + timerString;
    }

    void PlaySound(int index)
    {
        if (index >= 0 && index < soundeff.Length && soundeff[index] != null)
        {
            soundeff[index].PlayOneShot(soundeff[index].clip);
        }
        else
        {
            Debug.LogWarning("Invalid sound index!");
        }
    }

    void EndGame(){
        keyboardswitch = false;
        LScoreText.text = "Score : " + currentScore;

        thousands = currentScore / 1000;
        hundreds = (currentScore % 1000) / 100;
        tens = (currentScore % 100) / 10;
        units = currentScore % 10;
        ScoreMenu.SetActive(true);
        theMusic.Stop();
        // Time.timeScale = 0f;

        StartCoroutine(WaitAndPlayRandomSound());
    }

    IEnumerator WaitAndPlayRandomSound()
    {
        // yield return new WaitForSeconds(2.5f);
        PlaySound2(13);
        yield return new WaitForSeconds(1.5f);
        if (currentScore == 0){
            yield return new WaitForSeconds(1f);
            PlaySound2(0);
            yield return new WaitForSeconds(1f);
            PlaySound2(16);
        }else{
            StartCoroutine(PlaySoundsByDigits(thousands, hundreds, tens, units));
            yield return new WaitForSeconds(5f);
            PlaySound2(16);
        }
    }
    
    IEnumerator PlaySoundsByDigits(int thousands, int hundreds, int tens, int units)
    {
        // เล่นเสียงตามหลักพัน
        if (thousands > 0)
        {
            yield return new WaitForSeconds(1f);
            PlaySound2(thousands);
            yield return new WaitForSeconds(0.5f);
            PlaySound2(10);
        }

        // เล่นเสียงตามหลักร้อย
        if (hundreds > 0)
        {
            yield return new WaitForSeconds(1f);
            PlaySound2(hundreds);
            yield return new WaitForSeconds(0.5f);
            PlaySound2(11);
        }

        // เล่นเสียงตามหลักสิบ
        if (tens > 0)
        {
            if (tens == 2){
                yield return new WaitForSeconds(1f);
                PlaySound2(14);
                yield return new WaitForSeconds(0.5f);
                PlaySound2(12);
            }else if (tens == 1){
                yield return new WaitForSeconds(0.5f);
                PlaySound2(12);
            }else{
                yield return new WaitForSeconds(1f);
                PlaySound2(tens);
                yield return new WaitForSeconds(0.5f);
                PlaySound2(12);
            }
        }

        // เล่นเสียงตามหลักหน่วย
        if (units > 0)
        {
            yield return new WaitForSeconds(0.5f);
            PlaySound2(units);
        }
    }

    public void PlaySound2(int soundIndex){
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
}
