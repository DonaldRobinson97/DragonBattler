using UnityEngine;

public class ChaseState : State
{
    public void Enter()
    {
        Debug.Log("Entered Chasestate");
    }

    public void Update()
    {
        Debug.Log("Update Chasestate");
    }

    public void Exit()
    {
        Debug.Log("Exit Chasestate");

    }
}
