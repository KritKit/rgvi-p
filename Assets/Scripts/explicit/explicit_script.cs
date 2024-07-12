using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class explicit_script : MonoBehaviour
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
                if( prescore < explicit_game1_easy.FinalScore){
                    totalscorestage += explicit_game1_easy.FinalScore;
                }else if (prescore >= explicit_game1_easy.FinalScore){
                    totalscorestage += prescore;
                }               
                explicit_game1_easy.firststage_e++;

                if (explicit_game1_easy.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/explicit/easy/ep1_2_e");
                }else if(explicit_game1_easy.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/explicit/normal/ep1_2_n");
                }else if(explicit_game1_easy.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/explicit/hard/ep1_2_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 2) {
                if( prescore < explicit_game1_easy.FinalScore){
                    totalscorestage += explicit_game1_easy.FinalScore;
                }else if (prescore >= explicit_game1_easy.FinalScore){
                    totalscorestage += prescore;
                } 
                explicit_game1_easy.firststage_e++;
                if (explicit_game1_easy.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/explicit/easy/ep1_3_e");
                }else if(explicit_game1_easy.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/explicit/normal/ep1_3_n");
                }else if(explicit_game1_easy.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/explicit/hard/ep1_3_h");
                }
                checkstagenumber = 0; 
            }else if (checkstagenumber == 3) {
                if( prescore < explicit_game1_easy.FinalScore){
                    totalscorestage += explicit_game1_easy.FinalScore;
                }else if (prescore >= explicit_game1_easy.FinalScore){
                    totalscorestage += prescore;
                } 
                explicit_game1_easy.firststage_e++;
                if (explicit_game1_easy.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/explicit/easy/ep1_4_e");
                }else if(explicit_game1_easy.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/explicit/normal/ep1_4_n");
                }else if(explicit_game1_easy.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/explicit/hard/ep1_4_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 4) {
                if( prescore < explicit_game1_easy.FinalScore){
                    totalscorestage += explicit_game1_easy.FinalScore;
                }else if (prescore >= explicit_game1_easy.FinalScore){
                    totalscorestage += prescore;
                } 
                explicit_game1_easy.firststage_e++;
                if (explicit_game1_easy.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/explicit/easy/ep1_5_e");
                }else if(explicit_game1_easy.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/explicit/normal/ep1_5_n");
                }else if(explicit_game1_easy.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/explicit/hard/ep1_5_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 5) {
                if( prescore < explicit_game1_easy.FinalScore){
                    totalscorestage += explicit_game1_easy.FinalScore;
                }else if (prescore >= explicit_game1_easy.FinalScore){
                    totalscorestage += prescore;
                } 
                explicit_game1_easy.firststage_e-=4;
                if (explicit_game1_easy.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/explicit/easy/ep1_6_e");
                }else if(explicit_game1_easy.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/explicit/normal/ep1_6_n");
                }else if(explicit_game1_easy.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/explicit/hard/ep1_6_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 6) {
                if( prescore < explicit_game1_easy.FinalScore){
                    totalscorestage += explicit_game1_easy.FinalScore;
                }else if (prescore >= explicit_game1_easy.FinalScore){
                    totalscorestage += prescore;
                } 
                explicit_game1_easy.firststage_e++;
                if (explicit_game1_easy.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/explicit/easy/ep1_7_e");
                }else if(explicit_game1_easy.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/explicit/normal/ep1_7_n");
                }else if(explicit_game1_easy.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/explicit/hard/ep1_7_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 7) {
                if( prescore < explicit_game1_easy.FinalScore){
                    totalscorestage += explicit_game1_easy.FinalScore;
                }else if (prescore >= explicit_game1_easy.FinalScore){
                    totalscorestage += prescore;
                } 
                explicit_game1_easy.firststage_e++;
                if (explicit_game1_easy.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/explicit/easy/ep1_8_e");
                }else if(explicit_game1_easy.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/explicit/normal/ep1_8_n");
                }else if(explicit_game1_easy.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/explicit/hard/ep1_8_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 8) {
                if( prescore < explicit_game1_easy.FinalScore){
                    totalscorestage += explicit_game1_easy.FinalScore;
                }else if (prescore >= explicit_game1_easy.FinalScore){
                    totalscorestage += prescore;
                } 
                explicit_game1_easy.firststage_e++;
                if (explicit_game1_easy.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/explicit/easy/ep1_9_e");
                }else if(explicit_game1_easy.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/explicit/normal/ep1_9_n");
                }else if(explicit_game1_easy.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/explicit/hard/ep1_9_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 9) {
                if( prescore < explicit_game1_easy.FinalScore){
                    totalscorestage += explicit_game1_easy.FinalScore;
                }else if (prescore >= explicit_game1_easy.FinalScore){
                    totalscorestage += prescore;
                } 
                explicit_game1_easy.firststage_e++;
                if (explicit_game1_easy.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/explicit/easy/ep1_10_e");
                }else if(explicit_game1_easy.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/explicit/normal/ep1_10_n");
                }else if(explicit_game1_easy.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/explicit/hard/ep1_10_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 10) {
                if( prescore < explicit_game1_easy.FinalScore){
                    totalscorestage += explicit_game1_easy.FinalScore;
                }else if (prescore >= explicit_game1_easy.FinalScore){
                    totalscorestage += prescore;
                } 
                explicit_game_all.resultscore = totalscorestage;             
                if (explicit_game1_easy.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/explicit/easy/ep1_11_e");
                }else if(explicit_game1_easy.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/explicit/normal/ep1_11_n");
                }else if(explicit_game1_easy.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/explicit/hard/ep1_11_h");
                }
                checkstagenumber = 0;
            }
        }

        //เล่นด่านเดิม
        if(Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.JoystickButton0)){ //R1
            if (checkstagenumber == 1) {
                prescore = explicit_game1_easy.FinalScore;
                explicit_game1_easy.stagestatus--;
                if (explicit_game1_easy.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/explicit/easy/ep1_1_e");
                }else if(explicit_game1_easy.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/explicit/normal/ep1_1_n");
                }else if(explicit_game1_easy.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/explicit/hard/ep1_1_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 2) {
                explicit_game1_easy.stagestatus--;
                if (explicit_game1_easy.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/explicit/easy/ep1_2_e");
                }else if(explicit_game1_easy.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/explicit/normal/ep1_2_n");
                }else if(explicit_game1_easy.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/explicit/hard/ep1_2_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 3) {
                explicit_game1_easy.stagestatus--;
                if (explicit_game1_easy.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/explicit/easy/ep1_3_e");
                }else if(explicit_game1_easy.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/explicit/normal/ep1_3_n");
                }else if(explicit_game1_easy.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/explicit/hard/ep1_3_h");
                }
                checkstagenumber = 0; 
            }else if (checkstagenumber == 4) {
                explicit_game1_easy.stagestatus--;
                if (explicit_game1_easy.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/explicit/easy/ep1_4_e");
                }else if(explicit_game1_easy.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/explicit/normal/ep1_4_n");
                }else if(explicit_game1_easy.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/explicit/hard/ep1_4_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 5) {
                explicit_game1_easy.stagestatus--;
                if (explicit_game1_easy.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/explicit/easy/ep1_5_e");
                }else if(explicit_game1_easy.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/explicit/normal/ep1_5_n");
                }else if(explicit_game1_easy.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/explicit/hard/ep1_5_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 6) {
                explicit_game1_easy.stagestatus--;
                if (explicit_game1_easy.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/explicit/easy/ep1_6_e");
                }else if(explicit_game1_easy.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/explicit/normal/ep1_6_n");
                }else if(explicit_game1_easy.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/explicit/hard/ep1_6_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 7) {
                explicit_game1_easy.stagestatus--;
                if (explicit_game1_easy.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/explicit/easy/ep1_7_e");
                }else if(explicit_game1_easy.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/explicit/normal/ep1_7_n");
                }else if(explicit_game1_easy.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/explicit/hard/ep1_7_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 8) {
                explicit_game1_easy.stagestatus--;
                if (explicit_game1_easy.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/explicit/easy/ep1_8_e");
                }else if(explicit_game1_easy.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/explicit/normal/ep1_8_n");
                }else if(explicit_game1_easy.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/explicit/hard/ep1_8_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 9) {
                explicit_game1_easy.stagestatus--;
                if (explicit_game1_easy.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/explicit/easy/ep1_9_e");
                }else if(explicit_game1_easy.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/explicit/normal/ep1_9_n");
                }else if(explicit_game1_easy.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/explicit/hard/ep1_9_h");
                }
                checkstagenumber = 0;
            }else if (checkstagenumber == 10) {
                explicit_game1_easy.stagestatus--;
                if (explicit_game1_easy.difficultstage == 0){
                    SceneManager.LoadScene("Scenes/explicit/easy/ep1_10_e");
                }else if(explicit_game1_easy.difficultstage == 1){
                    SceneManager.LoadScene("Scenes/explicit/normal/ep1_10_n");
                }else if(explicit_game1_easy.difficultstage == 2){
                    SceneManager.LoadScene("Scenes/explicit/hard/ep1_10_h");
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
            scoreff = explicit_game1_easy.FinalScore;
            pointsText.text = scoreff.ToString() + " Points";
        }else if (checkscorenumber == 2) {
            scoreff = explicit_game1_easy.FinalScore;
            pointsText.text = scoreff.ToString() + " Points";
        }else if (checkscorenumber == 3) {
            scoreff = explicit_game1_easy.FinalScore;
            pointsText.text = scoreff.ToString() + " Points";
        }else if (checkscorenumber == 4) {
            scoreff = explicit_game1_easy.FinalScore;
            pointsText.text = scoreff.ToString() + " Points";
        }else if (checkscorenumber == 5) {
            scoreff = explicit_game1_easy.FinalScore;
            pointsText.text = scoreff.ToString() + " Points";
        }else if (checkscorenumber == 6) {
            scoreff = explicit_game1_easy.FinalScore;
            pointsText.text = scoreff.ToString() + " Points";
        }else if (checkscorenumber == 7) {
            scoreff = explicit_game1_easy.FinalScore;
            pointsText.text = scoreff.ToString() + " Points";
        }else if (checkscorenumber == 8) {
            scoreff = explicit_game1_easy.FinalScore;
            pointsText.text = scoreff.ToString() + " Points";
        }else if (checkscorenumber == 9) {
            scoreff = explicit_game1_easy.FinalScore;
            pointsText.text = scoreff.ToString() + " Points";
        }else if (checkscorenumber == 10) {
            scoreff = explicit_game1_easy.FinalScore;
            pointsText.text = scoreff.ToString() + " Points";
        }
    }
}
