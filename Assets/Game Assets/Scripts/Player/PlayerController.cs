using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float lookDuration = 10f;
    [SerializeField] private Animator _animator;
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
            _animator.SetBool("Walk", false);
            moveTweener.Kill();
        }

        float distance = Vector3.Distance(Pos, rb.transform.position);
        float duration = distance / moveSpeed;

        float angleDiff = Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, GetLookAtYRotation(transform.position, Pos)));
        float turnDuration = angleDiff / lookDuration;

        this.transform.DOLookAt(Pos, turnDuration).OnComplete(
            () =>
            {
                _animator.SetBool("Walk", true);
                moveTweener = rb.DOMove(Pos, duration).OnComplete(
                    () =>
                    {
                        _animator.SetBool("Walk", false);
                    }
                );
            }

        );
    }

    float GetLookAtYRotation(Vector3 from, Vector3 to)
    {
        Vector3 direction = to - from;
        direction.y = 0;

        return Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
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
