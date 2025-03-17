using UnityEngine;

public class DieState : State
{
    public void Enter()
    {
        Debug.Log("Entered Diestate");
    }

    public void Update()
    {
        Debug.Log("Update Diestate");
    }

    public void Exit()
    {
        Debug.Log("Exit Diestate");

    }
}
