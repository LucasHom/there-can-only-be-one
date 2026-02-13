using UnityEngine;
using UnityEngine.InputSystem;


public class TimetravelerInputs : MonoBehaviour
{

    public float chargeMod = 250f;

    private InputAction chargeTTAction;
    private bool chargingTT;
    private float scrollCharge;
    
    private float currCharge;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chargingTT = false;
    }

    // Update is called once per frame
    void Update()
    {
        scrollCharge = Mouse.current.scroll.ReadValue().y;
        
    }

    void FixedUpdate()
    {
        if (chargingTT)
        {
            
            currCharge += (chargeMod * scrollCharge);
            print(currCharge);
            
        } else
        {
            TimeHub.Instance.printTime(TimeHub.Instance.getTime());
        }
    }

    void OnEnable()
    {
        chargeTTAction = InputSystem.actions.FindAction("Timetravel");
        chargeTTAction.Enable();
        chargeTTAction.started += OnTimetravelStarted;
        chargeTTAction.canceled += OnTimetravelCanceled;

        // chargeTTScroll = InputSystem.actions.FindAction("ChargeTimetravel");
        // chargeTTScroll.Enable();

    }

    void OnDisable()
    {
        chargeTTAction.Disable();
        // chargeTTScroll.Disable();
    }

    void OnTimetravelStarted(InputAction.CallbackContext context)
    {
        chargingTT = true;

    }

    void OnTimetravelCanceled(InputAction.CallbackContext context)
    {
        
        TimeHub.Instance.timeForewards((int) currCharge);
        chargingTT = false;
        currCharge = 0;
        
    }

    
}
