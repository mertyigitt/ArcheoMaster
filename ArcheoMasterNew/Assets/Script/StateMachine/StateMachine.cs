using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private List<StateTransformer> _stateTransformers = new List<StateTransformer>();
    private List<StateTransformer> _anyStateTransformer = new List<StateTransformer>();

    private IState _currentState;
    
    public void SetState(IState state)
    {
        if (_currentState == state) return;
        
        if (_currentState is not null) _currentState.OnExit();
        
        _currentState = state;
        
        if (_currentState is not null) _currentState.OnEnter();
    }
    
    public void Tick()
    {
        StateTransformer stateTransformer = CheckForTransformer();
        
        if(stateTransformer is not null)
        {
            SetState(stateTransformer.To);
        }
        
        _currentState.Tick();
    }

    private StateTransformer CheckForTransformer()
    {
        foreach (var stateTransformer in _anyStateTransformer)
        {
            if(stateTransformer.Condition.Invoke()) return stateTransformer;
        }

        foreach (var stateTransformer in _stateTransformers)
        {
            if(stateTransformer.Condition.Invoke() && _currentState == stateTransformer.From) return stateTransformer;
        }
        
        return null;
    }

    public void AddState(IState from, IState to, Func<bool> condition)
    {
        StateTransformer stateTransformer = new StateTransformer(from, to, condition);
        _stateTransformers.Add(stateTransformer);
    }
    
    public void AddAnyState(IState to, Func<bool> condition)
    {
        StateTransformer stateTransformer = new StateTransformer(null, to, condition);
        _anyStateTransformer.Add(stateTransformer);
    }
}
