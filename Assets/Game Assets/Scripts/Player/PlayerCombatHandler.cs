using System.Collections;
using UnityEngine;

public class PlayerCombatHandler : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    [SerializeField] private bool isAlive = true;
    [SerializeField] private Animator _animator;
    private bool canAttack = true;

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
    }
    #endregion

    #region Public
    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
        }
        else
        {
            isAlive = false;
            Debug.Log("Player is dead");
        }
    }
    #endregion

    #region Private
    private void Initialise()
    {
        health = 150;
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
