using UnityEngine;

public class AttackState : State
{
    public void Enter()
    {
        Debug.Log("Entered AttackState");
    }

    public void Update()
    {
        Debug.Log("Update AttackState");
    }
    
    public void Exit()
    {
        Debug.Log("Exit AttackState");

    }
}
