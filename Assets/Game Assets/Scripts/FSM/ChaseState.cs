using UnityEngine;

public class ChaseState : State
{
    public EnemyController enemyController { get; }
    public ChaseState(EnemyController enemyController)
    {
        this.enemyController = enemyController;
    }
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
