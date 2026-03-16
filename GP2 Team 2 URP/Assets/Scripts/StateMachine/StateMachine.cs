using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected List<State> _states = new List<State>();
    public List<State> States { get { return _states; } }
    [SerializeField]
    protected State _currentState = null;
    public State CurrentState { get { return _currentState; } }
    public virtual void SwitchState<T>() where T : State
    {
        foreach (State state in States)
        {
            if (state.GetType() == typeof(T))
            {
                _currentState?.ExitState();  
                _currentState = state;  
                _currentState.EnterState();  
                return;
            }
        }
        Debug.LogWarning("State does not exist");
    }
    public void switchState<T>() where T : State
    {
        SwitchState<T>();
    }
    public void UpdateStateMachine()
    {
        _currentState?.UpdateState();
    }

    public void FixedUpdateStateMachine() 
    {
        _currentState?.FixedUpdateState();
    }

    public T GetState<T>() where T : State
    {
        foreach (State state in States)
        {
            if (state.GetType() == typeof(T))
            {
                return (T)state;
            }
        }
        Debug.LogWarning("State does not exist in " + this.ToString());
        return null;
    }

}
