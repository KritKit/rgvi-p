using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class tt_train_sc7 : MonoBehaviour
{
    public GameObject TrainMenu;
    public GameObject stopMenu;

    public AudioClip exdeath; // เสียงที่จะเล่นเมื่อเกมส์จบ
    public AudioClip hornsound; // เสียงแตร
    public AudioClip carsound; // เสียงรถผ่าน
    public AudioClip wrongsound;
    public AudioClip passsound;
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

    public bool checkpass = false; // ตรวจสอบการผ่าน

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
        if (timerText.text == "0" && keystatus == 0){
            if (carclear == 0 && (isKeyPressedA == false || isKeyPressedA == false)){
                audioSource.clip = wrongsound;
                audioSource.Play();
                StartCoroutine(restage());
                keystatus = 1;
            }else if (carclear == 1 && keystatus == 0){
                audioSource.clip = passsound;
                audioSource.Play();
                StartCoroutine(nextstage2());
                keystatus = 1;
                // checkpass = true;
                // int randomIndex = Random.Range(0, 2);
                // if (randomIndex == 0){
                //     SceneManager.LoadScene("Scenes/tr1_1");
                // }else if (randomIndex == 1){
                //     SceneManager.LoadScene("Scenes/tr1_2");               
                // }
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

    IEnumerator nextstage2()
    {            
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("Scenes/tutorial/train/tt_train8");

    }

    IEnumerator restage()
    {
        // audioSource.clip = wrongsound;
        // audioSource.Play();
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Scenes/tutorial/train/tt_train7");

    }


}
