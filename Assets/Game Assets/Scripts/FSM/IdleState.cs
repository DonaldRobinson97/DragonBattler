using UnityEngine;
public class IdleState : State
{
    public void Enter()
    {
        Debug.Log("Entered IdleState");
    }

    public void Update()
    {
        Debug.Log("Update IdleState");
    }
    
    public void Exit()
    {
        Debug.Log("Exit IdleState");

    }
}
