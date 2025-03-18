using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBridge : MonoBehaviour
{
    [SerializeField] private PlayerCombatHandler playerCombatHandler;
    public void TriggerHitRelease()
    {
        Debug.Log("Hit Released");
        playerCombatHandler.ToggleGetHit(true);
    }

    public void TriggerHitLock()
    {
        Debug.Log("Hit Locked");
        playerCombatHandler.ToggleGetHit(false);
    }
}
