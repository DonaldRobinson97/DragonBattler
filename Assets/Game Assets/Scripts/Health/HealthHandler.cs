using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    [SerializeField] private Image mainBar;
    [SerializeField] private Image EaseBar;
    [SerializeField] private float EaseDuration = 0.15f;
    [SerializeField] private float originalScale = 0.05f;

    #region Unity
    private void Start()
    {
        mainBar.fillAmount = 1;
        EaseBar.fillAmount = 1;
        this.transform.localScale = Vector3.one * originalScale;
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

        if (normalizedHealth > 0)
        {
            mainBar.fillAmount = normalizedHealth;
            EaseBar.DOFillAmount(normalizedHealth, EaseDuration);
        }
        else
        {
            this.transform.DOScale(Vector3.zero, EaseDuration);
        }
    }

    #endregion
}
