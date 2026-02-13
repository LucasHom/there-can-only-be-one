using UnityEngine;
using UnityEngine.InputSystem;


public class TimeHub : MonoBehaviour
{
    public static TimeHub Instance;

    private int time;
    public int START_TIME = 1000000;
    public int FIXED_UPDATE_RATE = 10;


    void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = START_TIME;
        Time.fixedDeltaTime = (1/ (float) FIXED_UPDATE_RATE);

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //time++;
        //print(time);
        //printTime(time);
    }

    public int getTime(){
        return time;
    }

    public void timeForewards(int newTime){
        newTime*=FIXED_UPDATE_RATE;
        time += newTime;
    }

    public void timeBackwards(int newTime){
        newTime*=FIXED_UPDATE_RATE;
        time -= newTime;
    }

    public void printTime(int time) {
        int sec, min, hour, day;
        time = (int) (time/FIXED_UPDATE_RATE);
        //print(time);

        day = time/(60*60*24);
        time -= day*60*60*24;

        hour = time/(60*60);
        time -= hour*60*60;

        min = time/(60);
        time -= min*60;

        sec = time;

        print(day + ":" + hour + ":" + min + ":" + sec);
    }
}

