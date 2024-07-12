using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class explicit_game1_easy : MonoBehaviour
{
    public GameObject exMenu;
    public GameObject fnMenu;
    public GameObject stopMenu;
    public GameObject openclock;
    public Text[] stext;
    private List<int> sequence = new List<int>();
    private int currentIndex = 0;
    private List<int> stackround = new List<int>();
    public AudioClip clocksheck; // กำหนด AudioClip
    public AudioClip endclock; // กำหนด AudioClip
    public AudioClip finstage; // กำหนด เสียงพูดผ่านด่าน
    public AudioClip finalarm; // กำหนด เสียงผ่านด่าน
    public AudioClip next_stage; // กำหนด เสียงรอบต่อไป
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
    public static int firststage_e = 0; //เช็คสถานะด่าน

    public static int difficultstage = 0; //เช็คความยาก

    public static int speedstage = 0;
    public static int tutomode = 0;

    // Start is called before the first frame update
    void Start()
    {
        // speedstage = 0;
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(PlaySequence());
        PlaySoundStage(stagestatus);
    }

    // Update is called once per frame
    void Update()
    {
        // stagestatus = 0;
        // Check player input
        CheckInput();
        // if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.JoystickButton9)){ //joy6 = L2
        //    if (isGameStarted){
        //         // If the game is already started, pause the game and show the stopMenu
        //         PauseGame();
        //         audioSource.Pause();
        //     }else{
        //         // If the game is not started, resume the game
        //         ResumeGame();
        //         audioSource.UnPause();
        //     }
        // }
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
            if (remainingTime <= 0){
                // EndGame();
                StopCoroutine(countdownCoroutine);

                //รีเซ็ต text
                countdownText.text = "READY";
                openclock.SetActive(true);
                // รีเซ็ตเวลา
                countdownTime = 7f; // หรือค่าที่คุณต้องการให้เวลาเริ่มต้น
                StartCoroutine(PlaySequence());
            }
            savetime = (int)remainingTime;
            yield return null;
        }
        countingDown = false;
    }

    IEnumerator PlaySequence()
    {
            if (stackround.Count != 0)
            {
                audioSource.clip = clocksheck;
                audioSource.Stop();
                yield return new WaitForSeconds(1.0f);
                audioSource.PlayOneShot(next_stage);
            }else{
                yield return new WaitForSeconds(1.5f);
            }
            
            if (difficultstage == 0) {
                if (stackround.Count == 3 + firststage_e){
                    if (tutomode == 1 ){
                        SceneManager.LoadScene("Scenes/tutorial/explicit/tt_exp6");
                        tutomode = 0;
                    }else{
                        FinGame();
                    }
                }else{
                    if (stackround.Count != 0)
                    {

                        yield return new WaitForSeconds(1.5f);
                        for (int i = 0; i < stackround.Count; i++)
                        {
                            audioSource.clip = tonesound[stackround[i]];
                            audioSource.Play();
                            stext[stackround[i]].color = Color.red;
                            yield return new WaitForSeconds(0.5f);
                            stext[stackround[i]].color = Color.white;
                            yield return new WaitForSeconds(0.5f);
                            audioSource.Stop();
                        }
                    }

                    for (int i = 0; i < 1; i++)
                    {  
                        int randomIndex = Random.Range(0, stext.Length);
                        sequence.Add(randomIndex);

                        // Display the current step in the sequence
                        audioSource.clip = tonesound[randomIndex];
                        audioSource.Play();
                        stext[randomIndex].color = Color.red;
                        yield return new WaitForSeconds(0.5f);
                        stext[randomIndex].color = Color.white;          
                        yield return new WaitForSeconds(0.5f);
                        // กำหนด AudioClip ที่ต้องการให้ AudioSource เล่น
                        audioSource.clip = clocksheck;
                        audioSource.volume = 0.5f;
                        // เล่นเสียง
                        audioSource.Play();
                        stackround.Add(randomIndex);
                    }   
                    currentIndex = 0;
                    
                    // เริ่ม Coroutine และเก็บ reference ไว้ใน countdownCoroutine
                    countdownCoroutine = StartCoroutine(Countdown(7));
                    openclock.SetActive(false);
                    }       
            }else if (difficultstage == 1 ){
                if (speedstage == 4){
                    if (stackround.Count == 3 + firststage_e){
                        FinGame();
                    }else{
                        if (stackround.Count != 0){
                            yield return new WaitForSeconds(1.5f);
                            for (int i = 0; i < stackround.Count; i++){
                                audioSource.clip = tonesound[stackround[i]];
                                audioSource.Play();
                                stext[stackround[i]].color = Color.red;
                                yield return new WaitForSeconds(0.25f);
                                stext[stackround[i]].color = Color.white;
                                yield return new WaitForSeconds(0.25f);
                                audioSource.Stop();
                            }
                        }
                        for (int i = 0; i < 1; i++){  
                            int randomIndex = Random.Range(0, stext.Length);
                            sequence.Add(randomIndex);
                            audioSource.clip = tonesound[randomIndex];
                            audioSource.Play();
                            stext[randomIndex].color = Color.red;
                            yield return new WaitForSeconds(0.25f);
                            stext[randomIndex].color = Color.white;          
                            yield return new WaitForSeconds(0.25f);
                            audioSource.clip = clocksheck;
                            audioSource.volume = 0.5f;
                            audioSource.Play();
                            stackround.Add(randomIndex);
                        }   
                        currentIndex = 0;
                        countdownCoroutine = StartCoroutine(Countdown(7));
                        openclock.SetActive(false);
                        }       
                }else{
                    if (stackround.Count == 3 + firststage_e){
                        FinGame();
                    }else{
                        if (stackround.Count != 0)
                        {
                            yield return new WaitForSeconds(1.5f);
                            for (int i = 0; i < stackround.Count; i++)
                            {
                                audioSource.clip = tonesound[stackround[i]];
                                audioSource.Play();
                                stext[stackround[i]].color = Color.red;
                                yield return new WaitForSeconds(0.5f);
                                stext[stackround[i]].color = Color.white;
                                yield return new WaitForSeconds(0.5f);
                                audioSource.Stop();
                            }
                        }

                        for (int i = 0; i < 1; i++)
                        {  
                            int randomIndex = Random.Range(0, stext.Length);
                            sequence.Add(randomIndex);

                            // Display the current step in the sequence
                            audioSource.clip = tonesound[randomIndex];
                            audioSource.Play();
                            stext[randomIndex].color = Color.red;
                            yield return new WaitForSeconds(0.5f);
                            stext[randomIndex].color = Color.white;          
                            yield return new WaitForSeconds(0.5f);
                            // กำหนด AudioClip ที่ต้องการให้ AudioSource เล่น
                            audioSource.clip = clocksheck;
                            audioSource.volume = 0.5f;
                            // เล่นเสียง
                            audioSource.Play();
                            stackround.Add(randomIndex);
                        }   
                        currentIndex = 0;
                        
                        // เริ่ม Coroutine และเก็บ reference ไว้ใน countdownCoroutine
                        countdownCoroutine = StartCoroutine(Countdown(7));
                        openclock.SetActive(false);
                        }       
                }
            }else if (difficultstage == 2 ){
                if (stackround.Count == 3 + firststage_e){
                    FinGame();
                }else{
                    if (stackround.Count != 0)
                    {
                        for (int i = 0; i < stackround.Count; i++)
                        {
                            audioSource.clip = tonesound[stackround[i]];
                            audioSource.Play();
                            stext[stackround[i]].color = Color.red;
                            yield return new WaitForSeconds(0.25f);
                            stext[stackround[i]].color = Color.white;
                            yield return new WaitForSeconds(0.25f);
                            audioSource.Stop();
                        }
                    }

                    for (int i = 0; i < 1; i++)
                    {  
                        int randomIndex = Random.Range(0, stext.Length);
                        sequence.Add(randomIndex);

                        // Display the current step in the sequence
                        audioSource.clip = tonesound[randomIndex];
                        audioSource.Play();
                        stext[randomIndex].color = Color.red;
                        yield return new WaitForSeconds(0.25f);
                        stext[randomIndex].color = Color.white;          
                        yield return new WaitForSeconds(0.25f);
                        // กำหนด AudioClip ที่ต้องการให้ AudioSource เล่น
                        audioSource.clip = clocksheck;
                        audioSource.volume = 0.5f;
                        // เล่นเสียง
                        audioSource.Play();
                        stackround.Add(randomIndex);
                    }   
                    currentIndex = 0;
                    
                    // เริ่ม Coroutine และเก็บ reference ไว้ใน countdownCoroutine
                    countdownCoroutine = StartCoroutine(Countdown(7));
                    openclock.SetActive(false);
                    }
            }           
    }
    void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            int clickedIndex = GetClickedIndexByKey();
            if (clickedIndex != -1)
            {
                if (clickedIndex == sequence[currentIndex])
                {
                    stext[clickedIndex].color = Color.green;              
                    currentIndex++;

                    if (currentIndex == sequence.Count)
                    {
                        audioSource.PlayOneShot(doubtsound[clickedIndex]);
                        // checkscore += savetime;
                        // เมื่อต้องการหยุด Coroutine
                        StopCoroutine(countdownCoroutine);
                        openclock.SetActive(true);
                        StartCoroutine(ResetColorAfterDelay(stext[clickedIndex]));
                        
                        checkscore += 1;
                        //รีเซ็ต text
                        countdownText.text = "READY";

                        // รีเซ็ตเวลา
                        countdownTime = 7f; // หรือค่าที่คุณต้องการให้เวลาเริ่มต้น
                        texttime = checkscore;
                        score = checkscore;

                        if (stagestatus == 0){
                            tscore.text = score.ToString() +" / 6";
                        }else if (stagestatus == 1){
                            tscore.text = score.ToString() +" / 10";
                        }else if (stagestatus == 2){
                            tscore.text = score.ToString() +" / 15";
                        }else if (stagestatus == 3){
                            tscore.text = score.ToString() +" / 21";
                        }else if (stagestatus == 4){
                            tscore.text = score.ToString() +" / 28";
                        }else if (stagestatus == 5){
                            tscore.text = score.ToString() +" / 6";
                        }else if (stagestatus == 6){
                            tscore.text = score.ToString() +" / 10";
                        }else if (stagestatus == 7){
                            tscore.text = score.ToString() +" / 15";
                        }else if (stagestatus == 8){
                            tscore.text = score.ToString() +" / 21";
                        }else if (stagestatus == 9){
                            tscore.text = score.ToString() +" / 28";
                        }

                        

                        StartCoroutine(PlaySequence());
                    }else{
                        audioSource.PlayOneShot(doubtsound[clickedIndex]);
                        checkscore += 1;
                        StartCoroutine(ResetColorAfterDelay(stext[clickedIndex]));                   
                    }
                }else{
                    // Debug.Log("Game Over");
                    // EndGame();
                    stext[clickedIndex].color = Color.cyan;              
                    currentIndex++;

                    if (currentIndex == sequence.Count)
                    {
                        audioSource.PlayOneShot(doubtsound[clickedIndex]);
                        // checkscore += savetime;
                        // เมื่อต้องการหยุด Coroutine
                        StopCoroutine(countdownCoroutine);
                        openclock.SetActive(true);
                        StartCoroutine(ResetColorAfterDelay(stext[clickedIndex]));
                        

                        //รีเซ็ต text
                        countdownText.text = "READY";

                        // รีเซ็ตเวลา
                        countdownTime = 7f; // หรือค่าที่คุณต้องการให้เวลาเริ่มต้น
                        texttime = checkscore;
                        score = checkscore;
                        if (stagestatus == 0){
                            tscore.text = score.ToString() +" / 6";
                        }else if (stagestatus == 1){
                            tscore.text = score.ToString() +" / 10";
                        }else if (stagestatus == 2){
                            tscore.text = score.ToString() +" / 15";
                        }else if (stagestatus == 3){
                            tscore.text = score.ToString() +" / 21";
                        }else if (stagestatus == 4){
                            tscore.text = score.ToString() +" / 28";
                        }else if (stagestatus == 5){
                            tscore.text = score.ToString() +" / 6";
                        }else if (stagestatus == 6){
                            tscore.text = score.ToString() +" / 10";
                        }else if (stagestatus == 7){
                            tscore.text = score.ToString() +" / 15";
                        }else if (stagestatus == 8){
                            tscore.text = score.ToString() +" / 21";
                        }else if (stagestatus == 9){
                            tscore.text = score.ToString() +" / 28";
                        }

                        StartCoroutine(PlaySequence());
                    }else{
                        audioSource.PlayOneShot(doubtsound[clickedIndex]);
                        checkscore += 0;
                        StartCoroutine(ResetColorAfterDelay(stext[clickedIndex]));                   
                    }
                }
            }
        }
    }

    int GetClickedIndexByKey()
    {
        if (gameEnded) // ตรวจสอบว่าเกมจบแล้วหรือไม่
        {
            return -1; // ถ้าเกมจบแล้วให้ไม่ทำงานฟังก์ชันนี้
        }

        // Check which key is pressed and return the corresponding index
        if (Input.GetKeyDown(KeyCode.H)||Input.GetKey(KeyCode.JoystickButton6)){ //L2
           return 0;
        }
        else if (Input.GetKeyDown(KeyCode.J)||Input.GetKey(KeyCode.JoystickButton4)){ //L1
            return 1;
        }
        else if (Input.GetKeyDown(KeyCode.K)||Input.GetKey(KeyCode.JoystickButton5)){ //R1
            return 2;
        }
        else if (Input.GetKeyDown(KeyCode.L)||Input.GetKey(KeyCode.JoystickButton7)){ //R2
            return 3;
        }

        return -1; // Return -1 if no matching key is pressed

    }

    IEnumerator ResetColorAfterDelay(Text text)
    {
        yield return new WaitForSeconds(0.6f);
        text.color = Color.white;
    }
    IEnumerator DelayedPlaySequence()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(PlaySequence());
    }

    private void EndGame()
    {
        FinalScore = score; // กำหนดค่า score ให้กับ static variable

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
        audioSource.PlayOneShot(endclock);

        StartCoroutine(WaitAndPlayRandomSound());
        StartCoroutine(afterscore(0));
        
        //ไปด่านเดิม
        explicit_script.checkscorenumber = stagestatus;
        explicit_script.checkstagenumber = stagestatus;

        exMenu.SetActive(true);
        gameEnded = true; // ตั้งค่าตัวแปรสถานะเมื่อเกมจบแล้ว
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
            explicit_script.speedstage = speedstage;
        }

        //เช็คคะแนนหลังเล่นจบ
        thousands = FinalScore / 1000;
        hundreds = (FinalScore % 1000) / 100;
        tens = (FinalScore % 100) / 10;
        units = FinalScore % 10;

        //หยุดทุกเสียง
        audioSource.Stop();
        audioSource.PlayOneShot(finalarm);
        audioSource.PlayOneShot(finstage);

        StartCoroutine(WaitAndPlayRandomSound());
        StartCoroutine(afterscore(1));

        stagestatus++;
        //ไปด่านต่อไป
        explicit_script.checkscorenumber = stagestatus;
        // explicit_script.totalscorestage += FinalScore;

        explicit_script.checkstagenumber = stagestatus;
        Debug.Log("Stage status: " + stagestatus);
        Debug.Log("Stage number: " + explicit_script.checkstagenumber);
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
            yield return new WaitForSeconds(6);
            PlaySoundfromStage(stagestatus-1);
            yield return new WaitForSeconds(2.5f);
            PlaySound(16);
        }
    }
    IEnumerator WaitAndPlayRandomSound()
    {
        yield return new WaitForSeconds(2.0f);
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
            }
            else{
                Debug.LogError("AudioClip is null");
                }
        }else
        {
            Debug.LogError("Invalid sound index");
        }
    }

    public void PlaySoundStage(int soundIndex){
        if (soundIndex >= 0 && soundIndex < audioClips.Count){
            if (audioStages[soundIndex] != null){
                audioSource.PlayOneShot(audioStages[soundIndex]);
            }
            else{
                Debug.LogError("AudioClip is null");
                }
        }else
        {
            Debug.LogError("Invalid sound index");
        }
    }

    public void PlaySoundfromStage(int soundIndex){
        if (soundIndex >= 0 && soundIndex < audioClips.Count){
            if (audiofromstages[soundIndex] != null){
                audioSource.PlayOneShot(audiofromstages[soundIndex]);
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
