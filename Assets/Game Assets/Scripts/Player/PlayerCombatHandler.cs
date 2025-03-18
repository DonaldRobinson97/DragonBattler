using System.Collections;
using UnityEngine;

public class PlayerCombatHandler : MonoBehaviour, IDamageable
{
    [SerializeField] private bool isAlive = true;
    [SerializeField] private Animator _animator;
    private bool canAttack = true;

    [Header("Health Handler")]
    [SerializeField] private HealthHandler healthHandler;
    [SerializeField] private int maxHealth;
    private int currentHealth;

    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int FireAttackDamage = 20;
    [SerializeField] private int MeleeAttackDamage = 10;

    #region Unity
    private void OnEnable()
    {
        EventController.StartListening(GameEvent.EVENT_ATTACK_FIRE, OnFireAttackPressed);
        EventController.StartListening(GameEvent.EVENT_ATTACK_MELEE, OnMeleeAttackPressed);
    }

    private void OnDisable()
    {
        EventController.StopListening(GameEvent.EVENT_ATTACK_FIRE, OnFireAttackPressed);
        EventController.StopListening(GameEvent.EVENT_ATTACK_MELEE, OnMeleeAttackPressed);
    }

    private void Start()
    {
        Initialise();
        healthHandler.SetHealth(currentHealth, maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(20);
        }
    }
    #endregion

    #region Public
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthHandler.SetHealth(currentHealth, maxHealth);
        
        if (currentHealth <= 0)
        {
            isAlive = false;
            Debug.Log("Player is dead");
        }
    }
    #endregion

    #region Private
    private void Initialise()
    {
        currentHealth = maxHealth;
        isAlive = true;
        canAttack = true;
    }

    private void LockAtack(float lockDownDuration)
    {
        StartCoroutine(AttackCoolDown(lockDownDuration));
    }

    private IEnumerator AttackCoolDown(float duration)
    {
        canAttack = false;
        yield return new WaitForSeconds(duration);
        canAttack = true;
    }
    #endregion

    #region Callbacks
    private void OnFireAttackPressed(object Args)
    {
        if (canAttack)
        {
            Debug.Log("Fire attack");
            _animator.SetTrigger("FireAttack");

            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

            foreach (Collider enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyController>()?.TakeDamage(FireAttackDamage);
            }

            LockAtack(2f);
        }
    }

    private void OnMeleeAttackPressed(object Args)
    {
        if (canAttack)
        {
            Debug.Log("Melee Attack");
            _animator.SetTrigger("MeleeAttack");
            LockAtack(1f);
        }
    }
    #endregion
}
