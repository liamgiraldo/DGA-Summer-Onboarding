using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    //maximum width jiggle distance
    [SerializeField] private float wiggleBounds;
    [SerializeField] private float maxXScale;
    [SerializeField] private float minXScale;
    [SerializeField] private float maxYScale;
    [SerializeField] private float minYScale;

    [SerializeField] private float scaleFactor;
    [SerializeField] private float wiggleFactor;

    //how high does the balloon go before it can't go any higher
    [SerializeField] private float wiggleHeight;
    [SerializeField] private float carrySpeed;

    //in seconds
    [SerializeField] private float carryTime;

    [SerializeField] private TMP_Text timeLeft;
    [SerializeField] private TMP_Text cooldownTime;

    private float time;

    private float cooldowntime;

    private enum BalloonState{inactive, active};
    private enum InflationState{deflating, inflating}
    private BalloonState state = BalloonState.inactive;
    private InflationState inflation = InflationState.deflating;

    //in seconds
    [SerializeField] private float cooldown;
    public bool isBalloonActive {get; set;} = false;
    private Rigidbody2D rb;

    [SerializeField] private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.time = carryTime;
        this.cooldowntime = 0;
    }

    void FixedUpdate(){

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            if(state == BalloonState.active){
                state = BalloonState.inactive;
                inflation = InflationState.deflating;
            }
            else if(state == BalloonState.inactive){
                state = BalloonState.active;
                inflation = InflationState.inflating;
            }
        }

        if(inflation == InflationState.deflating){
            deflate();
        }
        if(inflation == InflationState.inflating && cooldowntime <= 0){
            inflate();
        }
        if(state == BalloonState.active && cooldowntime <= 0){
            BalloonUpdate();
            player.GetComponent<Rigidbody2D>().gravityScale = carrySpeed * -1;
        }
        else if(state == BalloonState.inactive){
            player.GetComponent<Rigidbody2D>().gravityScale = 4;
        }

        if(cooldowntime > 0){
            cooldowntime -= 1 * Time.deltaTime;
            cooldownTime.SetText(((int)cooldowntime).ToString());
        }
    }

    void BalloonUpdate(){
        if(time > 0){
            time -= 1 * Time.deltaTime;
            timeLeft.SetText(((int)time).ToString());
        }
        if(time <= 0){
            inflation = InflationState.deflating;
            state = BalloonState.inactive;
            time = carryTime;
            timeLeft.SetText(((int)time).ToString());
            cooldowntime = cooldown;
            cooldownTime.SetText(((int)cooldowntime).ToString());
        }
    }

    void setLocalScaleX(float x){
        this.transform.localScale = new Vector3(x, this.transform.localScale.y, this.transform.localScale.z);
    }

    void setLocalScaleY(float y){
        this.transform.localScale = new Vector3(this.transform.localScale.x, y, this.transform.localScale.z);
    }

    void deflate(){
        if(this.transform.localScale.x > minXScale){
            setLocalScaleX(this.transform.localScale.x - scaleFactor * Time.deltaTime);
        }
        if(this.transform.localScale.y > minYScale){
            setLocalScaleY(this.transform.localScale.y - scaleFactor * Time.deltaTime);
        }
    }

    void inflate(){
        if(this.transform.localScale.x < maxXScale){
            setLocalScaleX(this.transform.localScale.x + scaleFactor * Time.deltaTime);
        }
        if(this.transform.localScale.y < maxYScale){
            setLocalScaleY(this.transform.localScale.y + scaleFactor * Time.deltaTime);
        }
    }
}
