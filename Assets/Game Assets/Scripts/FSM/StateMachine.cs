using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State currentState;

    public StateMachine(State state)
    {
        currentState = state;
        currentState.Enter();
    }

    public void ChangeState(State state)
    {
        if (state != currentState)
        {
            currentState.Exit();
            currentState = state;
            currentState.Enter();
        }
    }
}
