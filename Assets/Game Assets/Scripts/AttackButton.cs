using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    [SerializeField] private Image _fillerImage;
    [SerializeField] private Button _button;
    [SerializeField] private float cooldownDuration = 5f;
    [SerializeField] private GameEvent AttackType;

    private bool isInactive = false;

    #region Public
    public void OnButtonClicked()
    {
        if (isInactive)
        {
            return;
        }
        else
        {
            isInactive = true;
            EventController.TriggerEvent(AttackType, null);
            FillTween();
        }
    }
    #endregion

    #region Private
    private void FillTween()
    {
        _fillerImage.fillAmount = 1;

        _fillerImage.DOFillAmount(0, cooldownDuration).OnComplete(
            () =>
            {
                isInactive = false;
            }
        );
    }
    #endregion
}
