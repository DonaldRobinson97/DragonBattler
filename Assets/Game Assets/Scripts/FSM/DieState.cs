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
        enemyController.enemyAnimator.SetBool("Die", true);
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}
