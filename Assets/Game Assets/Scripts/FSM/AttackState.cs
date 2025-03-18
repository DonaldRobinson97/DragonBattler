using UnityEngine;

public class AttackState : State
{
    public EnemyController enemy { get; }
    public PlayerController _playerController;

    public AttackState(EnemyController enemyController)
    {
        this.enemy = enemyController;
    }
    public void Enter()
    {
        Debug.Log("Entered AttackState");
    }

    public void Update()
    {
        AttackPlayer();
        FaceTarget();
    }

    public void Exit()
    {
        Debug.Log("Exit AttackState");
    }

    #region Private
    private void AttackPlayer()
    {
        if (Time.time >= enemy.attackTimer)
        {
            enemy.attackTimer = Time.time + enemy.attackCooldownInterval;
            _playerController = enemy.DetectPlayer();

            if (_playerController != null)
            {
                int randAttack = Random.Range(0, 2);

                _playerController.DealDamage(randAttack == 1 ? enemy.PrimaryAttackDamage : enemy.SecondaryAttackDamage);

                if (randAttack == 1)
                {
                    enemy.enemyAnimator.SetTrigger("Attack");
                }
                else
                {
                    enemy.enemyAnimator.SetTrigger("Attack2");
                }
            }
            else
            {
                enemy._stateMachine.ChangeState(enemy._idleState);
            }
        }
    }

    private void FaceTarget()
    {
        if (_playerController != null)
        {
            Vector3 direction = (_playerController.transform.position - enemy.transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            enemy.enemyRB.rotation = Quaternion.Slerp(enemy.transform.rotation, rotation, Time.deltaTime * enemy.rotationSpeed);
        }
    }
    #endregion
}
