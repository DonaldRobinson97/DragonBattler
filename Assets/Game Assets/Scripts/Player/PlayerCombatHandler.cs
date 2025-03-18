using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlayerCombatHandler : MonoBehaviour, IDamageable
{
    public bool isAlive = true;
    [SerializeField] private Animator _animator;
    private bool canAttack = true;

    [Header("Health Handler")]
    [SerializeField] private HealthHandler healthHandler;
    [SerializeField] private int maxHealth;
    [SerializeField] private ParticleSystem MetoerParticleSystem;
    private int currentHealth;

    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int FireAttackDamage = 20;
    [SerializeField] private int MeleeAttackDamage = 10;
    [SerializeField] private ParticleSystem FireParticleSystem;
    [SerializeField] private ParticleSystem SlashParticleSystem;
    [SerializeField] private PlayerController controller;

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

    #endregion

    #region Public
    public void TakeDamage(int damage)
    {
        if (!isAlive)
        {
            return;
        }

        currentHealth -= damage;
        _animator.SetTrigger("Hit");

        healthHandler.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            isAlive = false;
            _animator.SetTrigger("Die");
            Debug.Log("Player is dead");
            EventController.TriggerEvent(GameEvent.EVENT_GAME_ENDED, false);
        }
    }

    public void PlayeMeteorParticle()
    {
        MetoerParticleSystem.Play();
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
            FireParticleSystem.Play();
            _animator.SetTrigger("FireAttack");

            controller.StopMovement();

            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

            foreach (Collider enemy in hitEnemies)
            {
                controller.MoveMarkedObject(enemy.transform.position);
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
            SlashParticleSystem.Play();
            _animator.SetTrigger("MeleeAttack");

            controller.StopMovement();

            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

            foreach (Collider enemy in hitEnemies)
            {
                controller.MoveMarkedObject(enemy.transform.position);
                enemy.GetComponent<EnemyController>()?.TakeDamage(FireAttackDamage);
            }

            LockAtack(1f);
        }
    }
    #endregion
}
