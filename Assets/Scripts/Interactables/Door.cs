using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Door : StatefulInteractable
{
    public enum DoorState
    {
        Open,
        Closed,
        Locked
    }

    private static readonly string StateKey = "DoorState";

    public UnityEvent OnOpen;
    public UnityEvent OnClose;
    public UnityEvent OnLock;
    public UnityEvent OnUnlock;

    public DoorState State { get; private set; }

    [SerializeField] private DoorState initialState = DoorState.Closed;

    void Awake()
    {
        OnOpen ??= new UnityEvent();
        OnClose ??= new UnityEvent();
        OnLock ??= new UnityEvent();
        OnUnlock ??= new UnityEvent();

        State = initialState;

        StateRegistry.Instance.Register(this);
    }

    public override Dictionary<string, object> GetState()
    {
        base.SetValue(StateKey, State.ToString());
        return base.GetState();
    }

    public override void SetState(Dictionary<string, object> newState)
    {
        base.SetState(newState);
        if (System.Enum.TryParse(base.GetValue<string>(StateKey), out DoorState parsedState))
        {
            State = parsedState;
        }
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
