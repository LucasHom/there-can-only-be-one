using UnityEngine;
using UnityEngine.InputSystem;


public class TimeHub : MonoBehaviour
{
    public static TimeHub Instance;

    private int time;
    public int START_TIME = 1000000;
    public float DEFAULT_FIXED_TIME = .1f;


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
        Time.fixedDeltaTime = DEFAULT_FIXED_TIME;

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        time++;
        //print(time);
        printTime(time);
    }

    public int getTime(){
        return time;
    }

    public void timeForewards(int newTime){
        time += newTime;
    }

    public void timeBackwards(int newTime){
        time -= newTime;
    }

    void printTime(int time) {
        int sec, min, hour, day;
        time = (int) (time*DEFAULT_FIXED_TIME);
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

