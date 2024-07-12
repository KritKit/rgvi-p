using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class tt_explicit_sc3 : MonoBehaviour
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
    public AudioClip tutor1;
    public AudioClip oksound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        // StartCoroutine(PlaySequence());
        CheckInput();
        audioSource.clip = tutor1;
        audioSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKey(KeyCode.JoystickButton0)) //X
        {
            audioSource.PlayOneShot(oksound); // Fix: Change Play to PlayOneShot
            StartCoroutine(nextstage());
        }
    }


               
    void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            int clickedIndex = GetClickedIndexByKey();
            if (clickedIndex != -1)
            {
                
                    stext[clickedIndex].color = Color.green;              
                    currentIndex++;


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
                    // }else{
                    //     audioSource.PlayOneShot(doubtsound[clickedIndex]);
                    //     checkscore += 1;
                    //     StartCoroutine(ResetColorAfterDelay(stext[clickedIndex]));                   
                    // }
                
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
        // StartCoroutine(PlaySequence());
    }

    IEnumerator nextstage()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Scenes/tutorial/explicit/tt_exp4");

    }
}
