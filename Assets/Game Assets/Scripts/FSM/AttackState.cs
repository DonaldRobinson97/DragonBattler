using UnityEngine;

public class AttackState : State
{
    public EnemyController enemyController { get; }
    public AttackState(EnemyController enemyController)
    {
        this.enemyController = enemyController;
    }
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
