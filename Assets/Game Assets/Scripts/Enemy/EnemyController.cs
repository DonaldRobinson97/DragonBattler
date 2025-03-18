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
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform[] wayPoints;

    public Rigidbody enemyRB => rigidbody;
    public float moveSpeed = 8f;
    public float rotationSpeed = 10f;
    public Animator enemyAnimator => _animator;

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
        _stateMachine.currentState.Update();
    }
    #endregion

    #region Public 
    public void TakeDamage(int damage)
    {
        _animator.SetTrigger("Hit");
        Health -= damage;

        if (Health <= 0)
        {
            _stateMachine.ChangeState(_dieState);
        }
    }

    public Vector3 GetRandomPosition()
    {
        return wayPoints[Random.Range(0, wayPoints.Length)].position;
    }
    #endregion

    #region Private
    #endregion

    #region Callbacks
    #endregion
}
