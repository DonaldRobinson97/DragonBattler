using UnityEngine;

public class DieState : State
{
    public EnemyController enemyController { get; }
    public DieState(EnemyController enemyController)
    {
        this.enemyController = enemyController;
    }
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
