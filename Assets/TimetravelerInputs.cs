using UnityEngine;
using UnityEngine.InputSystem;

public class TimetravelerInputs : MonoBehaviour
{

    [SerializeField] private InputActionReference TimetravelAction;
    public static 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChargeTimetravel(InputAction.CallbackContext context)
    {
        if(context.performed) // the key is pressed
        {
            TimeHub.Instance.timeForewards(100);
        }
        if(context.canceled) //the key has been released
        {
            TimeHub.Instance.timeBackwards(100);
        }
    }
}
