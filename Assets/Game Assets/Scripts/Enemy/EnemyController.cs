using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    public StateMachine _stateMachine;
    public IdleState _idleState;
    public ChaseState _chaseState;
    public AttackState _attackState;
    public DieState _dieState;

    [Header("Movement Components")]
    public float moveSpeed = 8f;
    public float rotationSpeed = 10f;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform[] wayPoints;
    public Rigidbody enemyRB => rigidbody;
    public Animator enemyAnimator => _animator;

    [Header("Detection Components")]
    [SerializeField] private float detectRadius = 3f;
    [SerializeField] private float detectionInterval = 2f;
    [SerializeField] private LayerMask PlayerLayer;
    public PlayerController player;
    private float Timer = 0f;
    public bool detectMode = true;

    [Header("Attack Components")]
    public float attackCooldownInterval = 2f;
    public float attackTimer = 0f;
    public int PrimaryAttackDamage = 10;
    public int SecondaryAttackDamage = 15;

    [Header("Health Components")]
    private int currentHealth = 100;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private HealthHandler healthHandler;
    public bool isDead => currentHealth <= 0;

    #region Unity
    private void Start()
    {
        _idleState = new IdleState(this);
        _chaseState = new ChaseState(this);
        _attackState = new AttackState(this);
        _dieState = new DieState(this);
        _stateMachine = new StateMachine(_idleState);

        currentHealth = maxHealth;
        healthHandler.SetHealth(currentHealth, maxHealth);
    }

    private void Update()
    {
        _stateMachine.currentState.Update();

        if (detectMode)
        {
            ScanForPlayer();
        }
    }
    #endregion

    #region Public 
    public void TakeDamage(int damage)
    {
        if (isDead) return;
        _animator.SetTrigger("Hit");
        currentHealth -= damage;

        healthHandler.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            _stateMachine.ChangeState(_dieState);
        }
    }

    public PlayerController DetectPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectRadius, PlayerLayer);

        PlayerController newPlayer = null;

        if (colliders.Length > 0)
        {
            newPlayer = colliders[0].GetComponent<PlayerController>();
        }

        return newPlayer;
    }

    public Vector3 GetRandomPosition()
    {
        return wayPoints[Random.Range(0, wayPoints.Length)].position;
    }
    #endregion

    #region Private
    private void ScanForPlayer()
    {
        if (Time.time >= Timer)
        {
            player = DetectPlayer();

            if (player != null)
            {
                _stateMachine.ChangeState(_attackState);
            }

            Timer = Time.time + detectionInterval;
        }
    }
    #endregion

    #region Callbacks
    #endregion
}
