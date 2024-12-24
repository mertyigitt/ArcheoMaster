using System;
using UnityEngine;

public class StateTransformer : MonoBehaviour
{
    public IState To { get; private set; }
    public IState From { get; private set; }
    public Func<bool> Condition { get; private set; }

    public StateTransformer(IState from, IState to, Func<bool> condition)
    {
        From = from;
        To = to;
        Condition = condition;
    }
}
