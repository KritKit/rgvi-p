using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

    int nn = 0;
    int nh = 0;
    int nm = 0;

    public bool canBePressed;

    public KeyCode keyToPress;
    public KeyCode joyToPress;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyToPress) || Input.GetKeyDown(joyToPress)){
            HandleNoteInput();
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Activator"){
            canBePressed = true;
            nn++;
            
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Activator"){
            canBePressed = false;

            // GameManager.instance.NoteMissed();
            if(nn != (nh + nm)){
                Debug.Log("Hit");
                GameManager.instance.NoteMissed();
                nm++;
                Instantiate(missEffect,transform.position,missEffect.transform.rotation);

            }
        }
    }

    private void HandleNoteInput()
    {
        if (canBePressed)
        {
            nh++;
            gameObject.SetActive(false);

            // GameManager.instance.NoteHit();
            if (Mathf.Abs(transform.position.y) > 0.1)
            {
                Debug.Log("Hit");
                GameManager.instance.NormalHit();
                Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
            }
            else if (Mathf.Abs(transform.position.y) > 0.5f)
            {
                Debug.Log("Good");
                GameManager.instance.GoodHit();
                Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
            }
            else
            {
                Debug.Log("Perfect");
                GameManager.instance.PerfectHit();
                Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
            }
        }
    }
}

