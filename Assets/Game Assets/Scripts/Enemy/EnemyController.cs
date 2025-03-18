using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    private StateMachine _stateMachine;
    private IdleState _idleState;
    private ChaseState _chaseState;
    private AttackState _attackState;
    private DieState _dieState;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private int Health = 100;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float TurnSpeed = 10f;

    #region Unity
    private void Start()
    {
        _idleState = new IdleState(this);
        _chaseState = new ChaseState(this);
        _attackState = new AttackState(this);
        _dieState = new DieState(this);

        _stateMachine = new StateMachine(_idleState);
    }

    private void Update()
    {
        // _stateMachine.currentState.Update();
    }
    #endregion

    #region Public 
    public void TakeDamage(int damage)
    {
        Health -= damage;
        
        if (Health <= 0)
        {
            _stateMachine.ChangeState(_dieState);
        }
    }
    #endregion

    #region Private
    #endregion

    #region Callbacks
    #endregion
}
