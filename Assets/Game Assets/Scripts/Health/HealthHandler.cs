using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    [SerializeField] private Image mainBar;
    [SerializeField] private Image EaseBar;
    [SerializeField] private float EaseDuration = 0.15f;

    #region Unity
    private void Start()
    {

    }

    private void Update()
    {
        this.transform.LookAt(this.transform.position + Camera.main.transform.forward);
    }
    #endregion

    #region Public 
    public void SetHealth(float currentHealth, float maxHealth)
    {
        float normalizedHealth = currentHealth / maxHealth;
        mainBar.fillAmount = normalizedHealth;
        EaseBar.DOFillAmount(normalizedHealth, EaseDuration);
    }

    #endregion
}
