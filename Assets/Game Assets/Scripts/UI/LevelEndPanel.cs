using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelEndPanel : MonoBehaviour
{
    [SerializeField] private GameObject mainContainer;
    [SerializeField] private Sprite WinImage;
    [SerializeField] private Sprite LoseImage;
    [SerializeField] private TMP_Text EndText;
    [SerializeField] private Image TextBG;
    [SerializeField] private Image DragonImagePanel;
    [SerializeField] private float TweenDuration;
    [SerializeField] private Ease TweenEase;

    private bool panelTrigerred = false;

    #region Unity
    private void Start()
    {
        mainContainer.SetActive(false);
    }

    private void OnEnable()
    {
        EventController.StartListening(GameEvent.EVENT_GAME_ENDED, OnGameEnded);
    }

    private void OnDisable()
    {
        EventController.StopListening(GameEvent.EVENT_GAME_ENDED, OnGameEnded);
    }
    #endregion

    #region Public
    public void OnReloadClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion

    #region Private 
    private void ShowPanel(bool Won)
    {
        if (Won)
        {
            DragonImagePanel.sprite = WinImage;
            EndText.text = "You Won!";
        }
        else
        {
            DragonImagePanel.sprite = LoseImage;
            EndText.text = "You Lost!";
        }

        mainContainer.transform.localScale = Vector3.zero;
        mainContainer.SetActive(true);
        mainContainer.transform.DOScale(Vector3.one, TweenDuration).SetEase(TweenEase);
    }
    #endregion

    #region Callbacks
    private void OnGameEnded(object Args)
    {
        bool isWon = (bool)Args;
        ShowPanel(isWon);
    }
    #endregion
}
