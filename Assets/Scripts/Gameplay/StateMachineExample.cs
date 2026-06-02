using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Events;

public class StateMachineExample : ReusableBehaviour
{
    public static event UnityAction OnEvent1;

    private enum State
    {
        Still,
        Wander,
        Escape
    }

    private State _myState;

    private State MyState
    {
        get => _myState;
        set
        {
            _myState = value;
            Debug.Log("set " + value);
        }
    }

    [SerializeField] float _stillToWanderTimeout = 2;

    void Start()
    {
        MyState = State.Still;
    }

    private void HandlePlayerFar()
    {
        if (MyState == State.Escape)
        {
            MyState = State.Wander;
            EscapeToWanderActions();
        }
    }

    private void EscapeToWanderActions()
    {
        SetWanderSpeed();
    }

    private void SetWanderSpeed()
    {
        
    }

    private void StillToWanderActions()
    {

    }

    private void WanderToStillActions()
    {
        CallDelayed(HandleStillToWanderTimeout, _stillToWanderTimeout);
    }

    private void HandleStillToWanderTimeout()
    {
        if (MyState == State.Still)
        {
            MyState = State.Wander;
            StillToWanderActions();
        }
    }
}
