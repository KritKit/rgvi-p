using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class rtm_game1 : MonoBehaviour
{
    public GameObject exMenu;
    public GameObject fnMenu;
    public GameObject stopMenu;
    public GameObject openclock;
    public Text[] stext;
    private List<int> sequence = new List<int>();
    private List<int> pressquece = new List<int>();
    private int currentIndex = 0;
    private List<int> stackround = new List<int>();
    public AudioClip BGMSong; // กำหนด เพลงพื้นหลัง
    public AudioClip clocksheck; // กำหนด AudioClip
    public AudioClip endclock; // กำหนด AudioClip
    public AudioClip finstage; // กำหนด เสียงพูดผ่านด่าน
    public AudioClip finalarm; // กำหนด เสียงผ่านด่าน
    public List<AudioClip> audioClips = new List<AudioClip>(); //เสียงเลข
    public List<AudioClip> audioStages = new List<AudioClip>(); //เสียงด่าน
    public List<AudioClip> audiofromstages = new List<AudioClip>(); //เสียงที่กดทั้งหมดด่าน
    public AudioClip[] tonesound; // กำหนด เสียงออก
    public AudioClip[] doubtsound; // กำหนด เสียงออก
    AudioSource audioSource;
    private bool countingDown = false; // ตัวแปรเพื่อตรวจสอบว่ากำลังนับถอยหลังหรือไม่
    public Text countdownText; //เก็บค่าเวลาที่นับ
    private Coroutine countdownCoroutine;
    private float countdownTime; // เวลาที่นับถอยหลัง
    private float remainingTime;  // เวลาที่เหลือ
    public Text tscore;
    private int checkscore;
    private int score;
    float texttime;
    private int thousands, hundreds, tens, units;
    public static int FinalScore; //คะแนนสุดท้าย
    private bool isGameActive = true; // กำหนดว่าเกมยังคงเล่นหรือไม่
    private bool isGameStarted = false; //เช็คการ pause

    private bool gameEnded = false; // เพิ่มตัวแปรสถานะเพื่อตรวจสอบว่าเกมจบแล้วหรือไม่

    public static int savetime; //เวลาที่เหลือจากกนับถอยหลัง

    public static int stagestatus = 0; //เช็คสถานะด่าน
    public static int passstage = 0; //เช็คสถานะด่าน

    public static int difficultstage = 0; //เช็คความยาก

    public static int speedstage = 0;
    public static int tutomode = 0;
    public static float timer = 2.1f;
    public Canvas canvasWithAudio;

    public static bool timestarted = true; 
    public static float itShowtime = 0;
    public Text ObjectText;
    public Text showtimeText;
    public bool notbad = false;
    public bool areEqual;
    public Text pressScore;
    public float speeddif;
    public static int tutorstage = 0;

    // Start is called before the first frame update
    void Start()
    {
        // difficultstage = 2;
        // passstage = 0;
        // stagestatus = 0;
        // speedstage = 0;
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(PlaySequence());
        // PlaySoundStage(stagestatus);
    }

    // Update is called once per frame
    void Update(){
        // stagestatus = 0;
        CheckInput();
        // if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.JoystickButton7)){ //joy7 = Option
        //    if (isGameStarted){
        //         PauseGame();
        //         audioSource.Pause();
        //     }else{
        //         ResumeGame();
        //         audioSource.UnPause();
        //     }
        // }
    }

    IEnumerator Countdown(float time)
    {
        countingDown = true;
        timer = time;

        while (timer > 0f){
            timer -= Time.deltaTime;
            remainingTime = timer;
            countdownText.text = timer.ToString("F2"); // "F0" ใช้เพื่อไม่แสดงทศนิยม
            if (remainingTime <= 0){
                // EndGame();
                StopCoroutine(countdownCoroutine);
                //รีเซ็ต text
                countdownText.text = "READY";
                // openclock.SetActive(true);
                // รีเซ็ตเวลา
                countdownTime = speeddif; // หรือค่าที่คุณต้องการให้เวลาเริ่มต้น
                if(currentIndex == 0){
                    checkshowtime(0);
                }
                if (pressquece.Count != sequence.Count){
                    checkshowtime(0);
                }
                pressquece.Clear();
                currentIndex = 0;
                sequence.Clear();
                StartCoroutine(PlaySequence());
                timestarted = false;
                
            }
            savetime = (int)remainingTime;
            yield return null;
        }
        countingDown = false;
    }

    IEnumerator PlaySequence(){
            if (difficultstage == 0) {
                speeddif = 3.1f;
                yield return new WaitForSeconds(0.5f);
                if (stackround.Count == 12 + passstage){
                    if (tutorstage == 1){
                        rtm_tutorial_script.passtage = 4;
                        SceneManager.LoadScene("Scenes/tutorial/rhythm/rtmtt_4");
                        tutorstage = 0;
                    }else{
                        FinGame();
                    }
                }else{
                    for (int i = 0; i < 3; i++)
                    {      
                        int randomIndex = Random.Range(0, stext.Length);
                        sequence.Add(randomIndex);
                        ObjectText.text = string.Join(", ", sequence.Select(num => (num+1).ToString()).ToArray());

                        audioSource.clip = tonesound[randomIndex];
                        audioSource.Play();
                        stext[randomIndex].color = Color.red;
                        yield return new WaitForSeconds(0.8f);
                        stext[randomIndex].color = Color.white;          
                        // yield return new WaitForSeconds(0f);
                        // stackround.Add(randomIndex);
                        if (stackround.Count > 0){
                            int lastValue = stackround[stackround.Count - 1];
                            stackround.Add(lastValue + 1);
                        }else{
                            // หาก List ยังไม่มีข้อมูล ให้เพิ่มค่า 1 เข้าไปใน List
                            stackround.Add(1);
                        }
                    } 

                    currentIndex = 0;
                    // เริ่ม Coroutine และเก็บ reference ไว้ใน countdownCoroutine
                    countdownCoroutine = StartCoroutine(Countdown(3.1f));
                    timestarted = true;
                    // openclock.SetActive(false);
                    }       
            }else if (difficultstage == 1) {
                speeddif = 2.1f;
                yield return new WaitForSeconds(0.2f);
                if (stackround.Count == 12 + passstage){
                    FinGame();
                }else{
                    for (int i = 0; i < 3; i++)
                    {      
                        int randomIndex = Random.Range(0, stext.Length);
                        sequence.Add(randomIndex);
                        ObjectText.text = string.Join(", ", sequence.Select(num => (num+1).ToString()).ToArray());


                        audioSource.clip = tonesound[randomIndex];
                        audioSource.Play();
                        stext[randomIndex].color = Color.red;
                        yield return new WaitForSeconds(0.5f);
                        stext[randomIndex].color = Color.white;          
                        // yield return new WaitForSeconds(0f);
                        // stackround.Add(randomIndex);
                        if (stackround.Count > 0){
                            int lastValue = stackround[stackround.Count - 1];
                            stackround.Add(lastValue + 1);
                        }else{
                            // หาก List ยังไม่มีข้อมูล ให้เพิ่มค่า 1 เข้าไปใน List
                            stackround.Add(1);
                        }
                    } 

                    currentIndex = 0;
                    // เริ่ม Coroutine และเก็บ reference ไว้ใน countdownCoroutine
                    countdownCoroutine = StartCoroutine(Countdown(2.1f));
                    timestarted = true;
                    // openclock.SetActive(false);
                    }       
            }else if (difficultstage == 2) {
                speeddif = 1.6f;
                // yield return new WaitForSeconds(0.2f);
                if (stackround.Count == 16 + passstage){
                    FinGame();
                }else{
                    for (int i = 0; i < 4; i++)
                    {      
                        int randomIndex = Random.Range(0, stext.Length);
                        sequence.Add(randomIndex);
                        ObjectText.text = string.Join(", ", sequence.Select(num => (num+1).ToString()).ToArray());


                        audioSource.clip = tonesound[randomIndex];
                        audioSource.Play();
                        stext[randomIndex].color = Color.red;
                        yield return new WaitForSeconds(0.4f);
                        stext[randomIndex].color = Color.white;          
                        // yield return new WaitForSeconds(0f);
                        // stackround.Add(randomIndex);
                        if (stackround.Count > 0){
                            int lastValue = stackround[stackround.Count - 1];
                            stackround.Add(lastValue + 1);
                        }else{
                            // หาก List ยังไม่มีข้อมูล ให้เพิ่มค่า 1 เข้าไปใน List
                            stackround.Add(1);
                        }
                    } 

                    currentIndex = 0;
                    // เริ่ม Coroutine และเก็บ reference ไว้ใน countdownCoroutine
                    countdownCoroutine = StartCoroutine(Countdown(1.6f));
                    timestarted = true;
                    // openclock.SetActive(false);
                    }       
            }      
    }
    void CheckInput(){
        if (Input.anyKeyDown){
        // if (isGameActive){
            int clickedIndex = GetClickedIndexByKey();
            pressquece.Add(clickedIndex);
            if (clickedIndex != -1){
                if (clickedIndex == sequence[currentIndex]){
                    
                    stext[clickedIndex].color = Color.green;              
                    currentIndex++;

                        if (currentIndex == sequence.Count){
                            audioSource.PlayOneShot(doubtsound[clickedIndex]);
                            StartCoroutine(ResetColorAfterDelay(stext[clickedIndex]));

                            checkscore += 1;
                            itShowtime = timer;
                            Debug.Log("Time Remaining: " + itShowtime.ToString("F2"));
                            areEqual = pressquece.SequenceEqual(sequence);
                            Debug.Log("Are equal: " + areEqual);
                            if (areEqual == true){
                                checkshowtime(float.Parse(itShowtime.ToString("F2"))); //เช็คเวลาที่เหลือ
                            }else if (areEqual == false){
                                showtimeText.text = "Miss (+0)";
                                score += 0;
                                pressScore.text = score.ToString();
                            }
                            // countdownText.text = "READY";

                            countdownTime = 2.1f; // หรือค่าที่คุณต้องการให้เวลาเริ่มต้น
                            texttime = checkscore;
                            // score = checkscore;

                            if (difficultstage == 0 || difficultstage == 1){
                                if (stagestatus == 0){
                                    tscore.text = texttime.ToString() +" / 12";
                                }else if (stagestatus == 1){
                                    tscore.text = texttime.ToString() +" / 15";
                                }else if (stagestatus == 2){
                                    tscore.text = texttime.ToString() +" / 18";
                                }else if (stagestatus == 3){
                                    tscore.text = texttime.ToString() +" / 21";
                                }else if (stagestatus == 4){
                                    tscore.text = texttime.ToString() +" / 24";
                                }else if (stagestatus == 5){
                                    tscore.text = texttime.ToString() +" / 12";
                                }else if (stagestatus == 6){
                                    tscore.text = texttime.ToString() +" / 15";
                                }else if (stagestatus == 7){
                                    tscore.text = texttime.ToString() +" / 18";
                                }else if (stagestatus == 8){
                                    tscore.text = texttime.ToString() +" / 21";
                                }else if (stagestatus == 9){
                                    tscore.text = texttime.ToString() +" / 24";
                                }
                            }else if(difficultstage == 2 ){
                                if (stagestatus == 0){
                                    tscore.text = texttime.ToString() +" / 16";
                                }else if (stagestatus == 1){
                                    tscore.text = texttime.ToString() +" / 20";
                                }else if (stagestatus == 2){
                                    tscore.text = texttime.ToString() +" / 24";
                                }else if (stagestatus == 3){
                                    tscore.text = texttime.ToString() +" / 28";
                                }else if (stagestatus == 4){
                                    tscore.text = texttime.ToString() +" / 32";
                                }else if (stagestatus == 5){
                                    tscore.text = texttime.ToString() +" / 16";
                                }else if (stagestatus == 6){
                                    tscore.text = texttime.ToString() +" / 20";
                                }else if (stagestatus == 7){
                                    tscore.text = texttime.ToString() +" / 24";
                                }else if (stagestatus == 8){
                                    tscore.text = texttime.ToString() +" / 28";
                                }else if (stagestatus == 9){
                                    tscore.text = texttime.ToString() +" / 32";
                                }
                            }

                            if(timer == 0){
                                StartCoroutine(PlaySequence());
                            }
                        }else{
                            audioSource.PlayOneShot(doubtsound[clickedIndex]);
                            checkscore += 1;
                            texttime = checkscore;
                            // score = checkscore;

                            if (difficultstage == 0 || difficultstage == 1){
                                if (stagestatus == 0){
                                    tscore.text = texttime.ToString() +" / 12";
                                }else if (stagestatus == 1){
                                    tscore.text = texttime.ToString() +" / 15";
                                }else if (stagestatus == 2){
                                    tscore.text = texttime.ToString() +" / 18";
                                }else if (stagestatus == 3){
                                    tscore.text = texttime.ToString() +" / 21";
                                }else if (stagestatus == 4){
                                    tscore.text = texttime.ToString() +" / 24";
                                }else if (stagestatus == 5){
                                    tscore.text = texttime.ToString() +" / 12";
                                }else if (stagestatus == 6){
                                    tscore.text = texttime.ToString() +" / 15";
                                }else if (stagestatus == 7){
                                    tscore.text = texttime.ToString() +" / 18";
                                }else if (stagestatus == 8){
                                    tscore.text = texttime.ToString() +" / 21";
                                }else if (stagestatus == 9){
                                    tscore.text = texttime.ToString() +" / 24";
                                }
                            }else if(difficultstage == 2 ){
                                if (stagestatus == 0){
                                    tscore.text = texttime.ToString() +" / 16";
                                }else if (stagestatus == 1){
                                    tscore.text = texttime.ToString() +" / 20";
                                }else if (stagestatus == 2){
                                    tscore.text = texttime.ToString() +" / 24";
                                }else if (stagestatus == 3){
                                    tscore.text = texttime.ToString() +" / 28";
                                }else if (stagestatus == 4){
                                    tscore.text = texttime.ToString() +" / 32";
                                }else if (stagestatus == 5){
                                    tscore.text = texttime.ToString() +" / 16";
                                }else if (stagestatus == 6){
                                    tscore.text = texttime.ToString() +" / 20";
                                }else if (stagestatus == 7){
                                    tscore.text = texttime.ToString() +" / 24";
                                }else if (stagestatus == 8){
                                    tscore.text = texttime.ToString() +" / 28";
                                }else if (stagestatus == 9){
                                    tscore.text = texttime.ToString() +" / 32";
                                }
                            }
                            StartCoroutine(ResetColorAfterDelay(stext[clickedIndex]));                   
                        }
                }else{
                    stext[clickedIndex].color = Color.cyan;              
                    currentIndex++;

                    if (currentIndex == sequence.Count){
                        audioSource.PlayOneShot(doubtsound[clickedIndex]);

                        StartCoroutine(ResetColorAfterDelay(stext[clickedIndex]));
                        itShowtime = timer;
                            Debug.Log("Time Remaining: " + itShowtime.ToString("F2"));
                            areEqual = pressquece.SequenceEqual(sequence);
                            Debug.Log("2Are equal: " + areEqual);
                            if (areEqual == true){
                                checkshowtime(float.Parse(itShowtime.ToString("F2"))); //เช็คเวลาที่เหลือ
                            }else if (areEqual == false){
                                showtimeText.text = "Miss (+0)";
                                score += 0;
                                pressScore.text = score.ToString();
                            }

                        // countdownText.text = "READY";

                        countdownTime = 2.1f; // หรือค่าที่คุณต้องการให้เวลาเริ่มต้น
                        texttime = checkscore;
                        // score = checkscore;
                        
                            if (difficultstage == 0 || difficultstage == 1){
                                if (stagestatus == 0){
                                    tscore.text = texttime.ToString() +" / 12";
                                }else if (stagestatus == 1){
                                    tscore.text = texttime.ToString() +" / 15";
                                }else if (stagestatus == 2){
                                    tscore.text = texttime.ToString() +" / 18";
                                }else if (stagestatus == 3){
                                    tscore.text = texttime.ToString() +" / 21";
                                }else if (stagestatus == 4){
                                    tscore.text = texttime.ToString() +" / 24";
                                }else if (stagestatus == 5){
                                    tscore.text = texttime.ToString() +" / 12";
                                }else if (stagestatus == 6){
                                    tscore.text = texttime.ToString() +" / 15";
                                }else if (stagestatus == 7){
                                    tscore.text = texttime.ToString() +" / 18";
                                }else if (stagestatus == 8){
                                    tscore.text = texttime.ToString() +" / 21";
                                }else if (stagestatus == 9){
                                    tscore.text = texttime.ToString() +" / 24";
                                }
                            }else if(difficultstage == 2 ){
                                if (stagestatus == 0){
                                    tscore.text = texttime.ToString() +" / 16";
                                }else if (stagestatus == 1){
                                    tscore.text = texttime.ToString() +" / 20";
                                }else if (stagestatus == 2){
                                    tscore.text = texttime.ToString() +" / 24";
                                }else if (stagestatus == 3){
                                    tscore.text = texttime.ToString() +" / 28";
                                }else if (stagestatus == 4){
                                    tscore.text = texttime.ToString() +" / 32";
                                }else if (stagestatus == 5){
                                    tscore.text = texttime.ToString() +" / 16";
                                }else if (stagestatus == 6){
                                    tscore.text = texttime.ToString() +" / 20";
                                }else if (stagestatus == 7){
                                    tscore.text = texttime.ToString() +" / 24";
                                }else if (stagestatus == 8){
                                    tscore.text = texttime.ToString() +" / 28";
                                }else if (stagestatus == 9){
                                    tscore.text = texttime.ToString() +" / 32";
                                }
                            }

                        if(timer == 0){                 
                            StartCoroutine(PlaySequence());
                        }
                    }else{
                        audioSource.PlayOneShot(doubtsound[clickedIndex]);
                        checkscore += 0;
                        texttime = checkscore;
                            // score = checkscore;

                            if (difficultstage == 0 || difficultstage == 1){
                                if (stagestatus == 0){
                                    tscore.text = texttime.ToString() +" / 12";
                                }else if (stagestatus == 1){
                                    tscore.text = texttime.ToString() +" / 15";
                                }else if (stagestatus == 2){
                                    tscore.text = texttime.ToString() +" / 18";
                                }else if (stagestatus == 3){
                                    tscore.text = texttime.ToString() +" / 21";
                                }else if (stagestatus == 4){
                                    tscore.text = texttime.ToString() +" / 24";
                                }else if (stagestatus == 5){
                                    tscore.text = texttime.ToString() +" / 12";
                                }else if (stagestatus == 6){
                                    tscore.text = texttime.ToString() +" / 15";
                                }else if (stagestatus == 7){
                                    tscore.text = texttime.ToString() +" / 18";
                                }else if (stagestatus == 8){
                                    tscore.text = texttime.ToString() +" / 21";
                                }else if (stagestatus == 9){
                                    tscore.text = texttime.ToString() +" / 24";
                                }
                            }else if(difficultstage == 2 ){
                                if (stagestatus == 0){
                                    tscore.text = texttime.ToString() +" / 16";
                                }else if (stagestatus == 1){
                                    tscore.text = texttime.ToString() +" / 20";
                                }else if (stagestatus == 2){
                                    tscore.text = texttime.ToString() +" / 24";
                                }else if (stagestatus == 3){
                                    tscore.text = texttime.ToString() +" / 28";
                                }else if (stagestatus == 4){
                                    tscore.text = texttime.ToString() +" / 32";
                                }else if (stagestatus == 5){
                                    tscore.text = texttime.ToString() +" / 16";
                                }else if (stagestatus == 6){
                                    tscore.text = texttime.ToString() +" / 20";
                                }else if (stagestatus == 7){
                                    tscore.text = texttime.ToString() +" / 24";
                                }else if (stagestatus == 8){
                                    tscore.text = texttime.ToString() +" / 28";
                                }else if (stagestatus == 9){
                                    tscore.text = texttime.ToString() +" / 32";
                                }
                            }
                        StartCoroutine(ResetColorAfterDelay(stext[clickedIndex]));                   
                    }
                }
            }else{
  
            }
        }
    }

    void checkshowtime(float time){
        if (time >= 0.1 && time <= 0.6){
            showtimeText.text = "Great (+5)";
            score += 5;
            pressScore.text = score.ToString();
        } else if (time >= 0.7 && time <= 1.2){
            showtimeText.text = "Perfect (+10)";
            score += 10;
            pressScore.text = score.ToString();
        } else if (Mathf.Approximately(time, 0.0f)){
            showtimeText.text = "Miss (+0)";
            score += 0;
            pressScore.text = score.ToString();
        }else{
            showtimeText.text = "Miss (+0)";
            score += 0;
            pressScore.text = score.ToString();
        }
    }

    int GetClickedIndexByKey(){
        if (gameEnded) // ตรวจสอบว่าเกมจบแล้วหรือไม่
        {
            return -1; // ถ้าเกมจบแล้วให้ไม่ทำงานฟังก์ชันนี้
        }

        // Check which key is pressed and return the corresponding index
        if (Input.GetKeyDown(KeyCode.H)||Input.GetKey(KeyCode.JoystickButton6)){ //L2
           return 0;
        }else if (Input.GetKeyDown(KeyCode.J)||Input.GetKey(KeyCode.JoystickButton4)){ //L1
            return 1;
        }else if (Input.GetKeyDown(KeyCode.K)||Input.GetKey(KeyCode.JoystickButton5)){ //R1
            return 2;
        }else if (Input.GetKeyDown(KeyCode.L)||Input.GetKey(KeyCode.JoystickButton7)){ //R2
            return 3;
        }
        return -1; // Return -1 if no matching key is pressed

    }

    IEnumerator ResetColorAfterDelay(Text text){
        yield return new WaitForSeconds(0.3f);
        text.color = Color.white;
    }

    private void FinGame()
    {       
        FinalScore = score; // กำหนดค่า score ให้กับ static variable

        // เมื่อต้องการหยุด Coroutine
        StopCoroutine(countdownCoroutine);

        // แสดงค่าเวลาที่เหลือ
        Debug.Log("Time Remaining: " + countdownTime);

        if (difficultstage == 1) {
            speedstage = stagestatus;
            rtm_scripts.speedstage = speedstage;
        }

        //เช็คคะแนนหลังเล่นจบ
        thousands = FinalScore / 1000;
        hundreds = (FinalScore % 1000) / 100;
        tens = (FinalScore % 100) / 10;
        units = FinalScore % 10;

        AudioSource audioSource = canvasWithAudio.GetComponent<AudioSource>();
        if (audioSource != null && audioSource.isPlaying){
            audioSource.Stop();
        }
        
        //หยุดทุกเสียง
        audioSource.Stop();
        audioSource.PlayOneShot(finalarm);
        audioSource.PlayOneShot(finstage);

        StartCoroutine(WaitAndPlayRandomSound());
        StartCoroutine(afterscore(1));

        stagestatus++;
        //ไปด่านต่อไป
        rtm_scripts.checkscorenumber = stagestatus;
        // explicit_script.totalscorestage += FinalScore;

        rtm_scripts.checkstagenumber = stagestatus;
        Debug.Log("Stage status: " + stagestatus);
        Debug.Log("Stage number: " + rtm_scripts.checkstagenumber);
        fnMenu.SetActive(true);

        gameEnded = true; // ตั้งค่าตัวแปรสถานะเมื่อเกมจบแล้ว
    }
    IEnumerator afterscore(int status)
    {
        if (status == 0){
            if (FinalScore == 0){
                yield return new WaitForSeconds(5.5f);
                PlaySound(15);
            }else{
                yield return new WaitForSeconds(7.5f);
                PlaySound(15);
            }
        }else{
            yield return new WaitForSeconds(6.5f);
            PlaySoundfromStage(stagestatus-1);
            yield return new WaitForSeconds(2.1f);
            PlaySound(16);
        }
    }
    IEnumerator WaitAndPlayRandomSound()
    {
        yield return new WaitForSeconds(2.1f);
        PlaySound(13);
        yield return new WaitForSeconds(1.5f);
        if (FinalScore == 0){
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
            }else{
                Debug.LogError("AudioClip is null");
                }
        }else{
            Debug.LogError("Invalid sound index");
        }
    }

    public void PlaySoundStage(int soundIndex){
        if (soundIndex >= 0 && soundIndex < audioClips.Count){
            if (audioStages[soundIndex] != null){
                audioSource.PlayOneShot(audioStages[soundIndex]);
            }else{
                Debug.LogError("AudioClip is null");
                }
        }else{
            Debug.LogError("Invalid sound index");
        }
    }

    public void PlaySoundfromStage(int soundIndex){
        if (soundIndex >= 0 && soundIndex < audioClips.Count){
            if (audiofromstages[soundIndex] != null){
                audioSource.PlayOneShot(audiofromstages[soundIndex]);
            }else{
                Debug.LogError("AudioClip is null");
                }
        }else{
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
    private void PauseGame(){
        if (isGameActive)
        {
            isGameStarted = false;
            Time.timeScale = 0; // Pause the game
            stopMenu.SetActive(true); // Show the stopMenu
        }
    }
    private void ResumeGame(){
        isGameStarted = true;
        Time.timeScale = 1; // Resume the game
        stopMenu.SetActive(false); // Hide the stopMenu
    }
}
