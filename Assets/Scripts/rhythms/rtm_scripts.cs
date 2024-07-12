using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class rtm_scripts : MonoBehaviour
{
    public Text pointsText;

    public static int checkstagenumber = 0; //รับค่ามาเช็คด่าน
    public static int checkscorenumber = 0; //รับค่ามาเช็คคะแนน
    public static int totalscorestage = 0; //รวมคะแนนทุกด่าน
    int scoreff;

    public static int prescore = 0; //คะแนนก่อนหน้า
    public static int speedstage = 0; //ความเร็วของด่าน
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //ไปด่านต่อไป
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton5)){ //R1
            if (checkstagenumber == 1) {
                if( prescore < rtm_game1.FinalScore){
                    totalscorestage += rtm_game1.FinalScore;
                }else if (prescore >= rtm_game1.FinalScore){
                    totalscorestage += prescore;
                }               
                rtm_game1.passstage+=3;

                if (rtm_game1.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/rhythm/easy/rtm2_e");
                }else if(rtm_game1.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/rhythm/normal/rtm2_n");
                }else if(rtm_game1.difficultstage == 2){
                    rtm_game1.passstage+=1;
                    SceneManager.LoadScene("Scenes/rhythm/hard/rtm2_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 2) {
                if( prescore < rtm_game1.FinalScore){
                    totalscorestage += rtm_game1.FinalScore;
                }else if (prescore >= rtm_game1.FinalScore){
                    totalscorestage += prescore;
                } 
                rtm_game1.passstage+=3;
                if (rtm_game1.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/rhythm/easy/rtm3_e");
                }else if(rtm_game1.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/rhythm/normal/rtm3_n");
                }else if(rtm_game1.difficultstage == 2){
                    rtm_game1.passstage+=1;
                    SceneManager.LoadScene("Scenes/rhythm/hard/rtm3_h");
                }
                checkstagenumber = 0; 
            }else if (checkstagenumber == 3) {
                if( prescore < rtm_game1.FinalScore){
                    totalscorestage += rtm_game1.FinalScore;
                }else if (prescore >= rtm_game1.FinalScore){
                    totalscorestage += prescore;
                } 
                rtm_game1.passstage+=3;
                if (rtm_game1.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/rhythm/easy/rtm4_e");
                }else if(rtm_game1.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/rhythm/normal/rtm4_n");
                }else if(rtm_game1.difficultstage == 2){
                    rtm_game1.passstage+=1;
                    SceneManager.LoadScene("Scenes/rhythm/hard/rtm4_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 4) {
                if( prescore < rtm_game1.FinalScore){
                    totalscorestage += rtm_game1.FinalScore;
                }else if (prescore >= rtm_game1.FinalScore){
                    totalscorestage += prescore;
                } 
                rtm_game1.passstage+=3;
                if (rtm_game1.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/rhythm/easy/rtm5_e");
                }else if(rtm_game1.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/rhythm/normal/rtm5_n");
                }else if(rtm_game1.difficultstage == 2){
                    rtm_game1.passstage+=1;
                    SceneManager.LoadScene("Scenes/rhythm/hard/rtm5_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 5) {
                if( prescore < rtm_game1.FinalScore){
                    totalscorestage += rtm_game1.FinalScore;
                }else if (prescore >= rtm_game1.FinalScore){
                    totalscorestage += prescore;
                } 
                rtm_game1.passstage-=12;
                if (rtm_game1.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/rhythm/easy/rtm6_e");
                }else if(rtm_game1.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/rhythm/normal/rtm6_n");
                }else if(rtm_game1.difficultstage == 2){
                    rtm_game1.passstage-=4;
                    SceneManager.LoadScene("Scenes/rhythm/hard/rtm6_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 6) {
                if( prescore < rtm_game1.FinalScore){
                    totalscorestage += rtm_game1.FinalScore;
                }else if (prescore >= rtm_game1.FinalScore){
                    totalscorestage += prescore;
                }
                rtm_game1.passstage+=3;
                if (rtm_game1.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/rhythm/easy/rtm7_e");
                }else if(rtm_game1.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/rhythm/normal/rtm7_n");
                }else if(rtm_game1.difficultstage == 2){
                    rtm_game1.passstage+=1;
                    SceneManager.LoadScene("Scenes/rhythm/hard/rtm7_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 7) {
                if( prescore < rtm_game1.FinalScore){
                    totalscorestage += rtm_game1.FinalScore;
                }else if (prescore >= rtm_game1.FinalScore){
                    totalscorestage += prescore;
                } 
                rtm_game1.passstage+=3;
                if (rtm_game1.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/rhythm/easy/rtm8_e");
                }else if(rtm_game1.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/rhythm/normal/rtm8_n");
                }else if(rtm_game1.difficultstage == 2){
                    rtm_game1.passstage+=1;
                    SceneManager.LoadScene("Scenes/rhythm/hard/rtm8_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 8) {
                if( prescore < rtm_game1.FinalScore){
                    totalscorestage += rtm_game1.FinalScore;
                }else if (prescore >= rtm_game1.FinalScore){
                    totalscorestage += prescore;
                } 
                rtm_game1.passstage+=3;
                if (rtm_game1.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/rhythm/easy/rtm9_e");
                }else if(rtm_game1.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/rhythm/normal/rtm9_n");
                }else if(rtm_game1.difficultstage == 2){
                    rtm_game1.passstage+=1;
                    SceneManager.LoadScene("Scenes/rhythm/hard/rtm9_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 9) {
                if( prescore < rtm_game1.FinalScore){
                    totalscorestage += rtm_game1.FinalScore;
                }else if (prescore >= rtm_game1.FinalScore){
                    totalscorestage += prescore;
                } 
                rtm_game1.passstage+=3;
                if (rtm_game1.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/rhythm/easy/rtm10_e");
                }else if(rtm_game1.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/rhythm/normal/rtm10_n");
                }else if(rtm_game1.difficultstage == 2){
                    rtm_game1.passstage+=1;
                    SceneManager.LoadScene("Scenes/rhythm/hard/rtm10_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 10) {
                if( prescore < rtm_game1.FinalScore){
                    totalscorestage += rtm_game1.FinalScore;
                }else if (prescore >= rtm_game1.FinalScore){
                    totalscorestage += prescore;
                } 
                rtm_game_all.resultscore = totalscorestage;             
                if (rtm_game1.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/rhythm/normal/rtm11_n");
                }else if(rtm_game1.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/rhythm/normal/rtm11_n");
                }else if(rtm_game1.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/rhythm/normal/rtm11_n");
                }
                checkstagenumber = 0;
            }
        }

        //เล่นด่านเดิม
        if(Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.JoystickButton0)){ //X
            if (checkstagenumber == 1) {
                prescore = rtm_game1.FinalScore;
                rtm_game1.stagestatus--;
                if (rtm_game1.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/rhythm/easy/rtm1_e");
                }else if(rtm_game1.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/rhythm/normal/rtm1_n");
                }else if(rtm_game1.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/rhythm/hard/rtm1_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 2) {
                rtm_game1.stagestatus--;
                if (rtm_game1.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/rhythm/easy/rtm2_e");
                }else if(rtm_game1.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/rhythm/normal/rtm2_n");
                }else if(rtm_game1.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/rhythm/hard/rtm2_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 3) {
                rtm_game1.stagestatus--;
                if (rtm_game1.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/rhythm/easy/rtm3_e");
                }else if(rtm_game1.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/rhythm/normal/rtm3_n");
                }else if(rtm_game1.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/rhythm/hard/rtm3_h");
                }
                checkstagenumber = 0; 
            }else if (checkstagenumber == 4) {
                rtm_game1.stagestatus--;
                if (rtm_game1.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/rhythm/easy/rtm4_e");
                }else if(rtm_game1.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/rhythm/normal/rtm4_n");
                }else if(rtm_game1.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/rhythm/hard/rtm4_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 5) {
                rtm_game1.stagestatus--;
                if (rtm_game1.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/rhythm/easy/rtm5_e");
                }else if(rtm_game1.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/rhythm/normal/rtm5_n");
                }else if(rtm_game1.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/rhythm/hard/rtm5_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 6) {
                rtm_game1.stagestatus--;
                if (rtm_game1.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/rhythm/easy/rtm6_e");
                }else if(rtm_game1.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/rhythm/normal/rtm6_n");
                }else if(rtm_game1.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/rhythm/hard/rtm6_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 7) {
                rtm_game1.stagestatus--;
                if (rtm_game1.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/rhythm/easy/rtm7_e");
                }else if(rtm_game1.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/rhythm/normal/rtm7_n");
                }else if(rtm_game1.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/rhythm/hard/rtm7_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 8) {
                rtm_game1.stagestatus--;
                if (rtm_game1.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/rhythm/easy/rtm8_e");
                }else if(rtm_game1.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/rhythm/normal/rtm8_n");
                }else if(rtm_game1.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/rhythm/hard/rtm8_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 9) {
                rtm_game1.stagestatus--;
                if (rtm_game1.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/rhythm/easy/rtm9_e");
                }else if(rtm_game1.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/rhythm/normal/rtm9_n");
                }else if(rtm_game1.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/rhythm/hard/rtm9_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 10) {
                rtm_game1.stagestatus--;
                if (rtm_game1.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/rhythm/easy/rtm10_e");
                }else if(rtm_game1.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/rhythm/normal/rtm10_n");
                }else if(rtm_game1.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/rhythm/hard/rtm10_h");
                }
                checkstagenumber = 0;
            }
        }
        if(Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.JoystickButton4)){ //L1
            SceneManager.LoadScene("Scenes/MainMenu");
        }
    }
    // Use Awake or Start for initialization
    void Awake()
    {
        if (checkscorenumber == 1) {
            scoreff = rtm_game1.FinalScore;
            pointsText.text = scoreff.ToString() + " Pts";
        }else if (checkscorenumber == 2) {
            scoreff = rtm_game1.FinalScore;
            pointsText.text = scoreff.ToString() + " Pts";
        }else if (checkscorenumber == 3) {
            scoreff = rtm_game1.FinalScore;
            pointsText.text = scoreff.ToString() + " Pts";
        }else if (checkscorenumber == 4) {
            scoreff = rtm_game1.FinalScore;
            pointsText.text = scoreff.ToString() + " Pts";
        }else if (checkscorenumber == 5) {
            scoreff = rtm_game1.FinalScore;
            pointsText.text = scoreff.ToString() + " Pts";
        }else if (checkscorenumber == 6) {
            scoreff = rtm_game1.FinalScore;
            pointsText.text = scoreff.ToString() + " Pts";
        }else if (checkscorenumber == 7) {
            scoreff = rtm_game1.FinalScore;
            pointsText.text = scoreff.ToString() + " Pts";
        }else if (checkscorenumber == 8) {
            scoreff = rtm_game1.FinalScore;
            pointsText.text = scoreff.ToString() + " Pts";
        }else if (checkscorenumber == 9) {
            scoreff = rtm_game1.FinalScore;
            pointsText.text = scoreff.ToString() + " Pts";
        }else if (checkscorenumber == 10) {
            scoreff = rtm_game1.FinalScore;
            pointsText.text = scoreff.ToString() + " Pts";
        }
    }
}
