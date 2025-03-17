using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float lookDuration = 0.1f;
    private float duration = 1f;
    private Tweener moveTweener;

    #region Unity
    private void OnEnable()
    {
        EventController.StartListening(GameEvent.EVENT_POSITION_MARKED, OnPositionMarked);
    }

    private void Start()
    {
        moveTweener.SetAutoKill();
    }

    private void OnDisable()
    {
        EventController.StopListening(GameEvent.EVENT_POSITION_MARKED, OnPositionMarked);
    }
    #endregion

    #region Public 

    #endregion

    #region Private
    private void MovePlayer(Vector3 Pos)
    {
        if (moveTweener != null)
        {
            moveTweener.Kill();
        }

        float distance = Vector3.Distance(Pos, rb.transform.position);

        float duration = distance / moveSpeed;

        this.transform.DOLookAt(Pos, duration * 0.25f).OnComplete(
            () =>
            {
                moveTweener = rb.DOMove(Pos, duration);
            }

        );

    }
    #endregion

    #region Callbacks
    private void OnPositionMarked(object Args)
    {
        if (Args != null)
        {
            MovePlayer((Vector3)Args);
        }
    }
    #endregion
}
