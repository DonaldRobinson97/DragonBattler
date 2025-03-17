using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    private StateMachine _stateMachine;
    private IdleState _idleState;
    private ChaseState _chaseState;
    private AttackState _attackState;
    private DieState _dieState;
    [SerializeField] private Rigidbody rigidbody;

    #region Unity
    private void Start()
    {
        _idleState = new IdleState();
        _chaseState = new ChaseState();
        _attackState = new AttackState();
        _dieState = new DieState();

        _stateMachine = new StateMachine(_idleState);
    }

    private void Update()
    {
        _stateMachine.currentState.Update();
    }
    #endregion

    #region Public 
    public void TakeDamage(int damage)
    {

    }
    #endregion

    #region Private
    #endregion

    #region Callbacks
    #endregion
}
