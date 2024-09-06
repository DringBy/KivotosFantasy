using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState {  get; private set; }

    public void initialize(PlayerState _startState)
    {
        currentState = _startState;
        currentState.enter();
    }
    
    public void changeState(PlayerState _newState)
    {
        currentState.exit();
        currentState = _newState;
        currentState.enter();
    }
}
