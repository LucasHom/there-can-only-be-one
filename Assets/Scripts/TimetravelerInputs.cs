using UnityEngine;
using UnityEngine.InputSystem;


public class TimetravelerInputs : MonoBehaviour
{

    private InputAction chargeTT;
    private bool chargingTT;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chargingTT = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void FixedUpdate()
    {
        
    }

    void OnEnable()
    {
        chargeTT = InputSystem.actions.FindAction("Timetravel");

        chargeTT.Enable();
        chargeTT.started += OnTimetravelStarted;
    }

    void OnDissable()
    {
        chargeTT.Disable();
    }

    void OnTimetravelStarted(InputAction.CallbackContext context)
    {
        TimeHub.Instance.timeForewards(100);


        // if(context.performed) // the key is pressed
        // {
        //     TimeHub.Instance.timeForewards(100);
        // }
        // if(context.canceled) //the key has been released
        // {
        //     TimeHub.Instance.timeBackwards(100);
        // }
    }

    
}
