using UnityEngine;
public class IdleState : State
{
    private EnemyController enemy;
    private Vector3 targetPosition;
    private bool reachedTarget = false;

    #region Public
    public IdleState(EnemyController enemyController)
    {
        enemy = enemyController;
    }
    public void Enter()
    {
        Debug.Log("Entered IdleState");
        targetPosition = enemy.GetRandomPosition();
        enemy.detectMode = true;
    }

    public void Update()
    {
        if (!reachedTarget)
        {
            MoveEnemy();
            LookRotation();
        }
        else
        {
            enemy.enemyAnimator.SetBool("Walk", false);
            targetPosition = enemy.GetRandomPosition();
            reachedTarget = false;
        }
    }

    public void Exit()
    {
        enemy.detectMode = false;
        enemy.enemyAnimator.SetBool("Walk", false);
        reachedTarget = false;
    }
    #endregion

    #region Private 

    private void MoveEnemy()
    {
        Vector3 direction = (targetPosition - enemy.transform.position).normalized;
        Vector3 newPosition = enemy.transform.position + direction * enemy.moveSpeed * Time.fixedDeltaTime;

        enemy.enemyRB.MovePosition(newPosition);
        enemy.enemyAnimator.SetBool("Walk", true);

        if (Vector3.Distance(enemy.transform.position, targetPosition) < 0.5f)
        {
            reachedTarget = true;
        }
    }

    private void LookRotation()
    {
        if (enemy.transform.position != targetPosition)
        {
            Vector3 direction = (targetPosition - enemy.transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            enemy.enemyRB.rotation = Quaternion.Slerp(enemy.transform.rotation, rotation, Time.deltaTime * enemy.rotationSpeed);
        }
    }
    #endregion
}
