using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public enum DoorState
    {
        Open,
        Closed,
        Locked
    }

    public UnityEvent OnOpen;
    public UnityEvent OnClose;
    public UnityEvent OnLock;
    public UnityEvent OnUnlock;

    public DoorState State { get; private set; }

    void Start()
    {
        OnOpen ??= new UnityEvent();
        OnClose ??= new UnityEvent();
        OnLock ??= new UnityEvent();
        OnUnlock ??= new UnityEvent();
    }

    public void TryOpen()
    {
        if (State != DoorState.Closed)
            return;
        State = DoorState.Open;
        OnOpen.Invoke();
    }

    public void TryClose()
    {
        if (State != DoorState.Open)
            return;
        State = DoorState.Closed;
        OnClose.Invoke();
    }

    public void TryLock()
    {
        if (State != DoorState.Closed)
            return;
        State = DoorState.Locked;
        OnLock.Invoke();
    }

    public void TryUnlock()
    {
        if (State != DoorState.Locked)
            return;
        State = DoorState.Closed;
        OnUnlock.Invoke();
    }

    public void TryToggle()
    {
        if (State == DoorState.Open)
        {
            TryClose();
        }
        else if (State == DoorState.Closed)
        {
            TryOpen();
        }
    }
}
