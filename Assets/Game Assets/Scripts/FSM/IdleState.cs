using UnityEngine;
public class IdleState : State
{
    private EnemyController enemy;

    public IdleState(EnemyController enemyController)
    {
        enemy = enemyController;
    }
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
