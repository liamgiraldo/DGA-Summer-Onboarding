using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblePad : MonoBehaviour
{
    [SerializeField] private float totalTime;
    private float time;
    private bool timerActive = false;
    private bool crumbleTimerActive = false;

    [SerializeField] private float totalTimeUntilCrumble;

    private float timeUntilCrumble;
    // Start is called before the first frame update
    void Start()
    {
        time = totalTime;
        timeUntilCrumble = totalTimeUntilCrumble;
    }

    // Update is called once per frame
    void Update()
    {
        if(crumbleTimerActive){
            timeUntilCrumble -= 1 * Time.deltaTime;
            if(timeUntilCrumble < 0){
                timerActive = true;
            }
        }

        if(timerActive){
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            time -= 1 * Time.deltaTime;
            if(time <= 0){
                timerActive = false;
                crumbleTimerActive = false;

                time = totalTime;
                timeUntilCrumble = totalTimeUntilCrumble;
            }
        }
        if(!timerActive){
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.CompareTag("Player")){
            crumbleTimerActive = true;
        }
    }
}
