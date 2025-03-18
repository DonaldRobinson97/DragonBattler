using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Animator _animator;
    private Tweener moveTweener;
    [SerializeField] private string EnemyTag;

    [Header("Position Marker")]
    public LayerMask groundLayer;
    [SerializeField] private ParticleSystem markerObject;
    private Vector3 targetPosition;
    private bool isMoving = false;



    #region Unity
    private void OnEnable()
    {
    }

    private void Start()
    {
        moveTweener.SetAutoKill();
        markerObject.transform.position = Vector3.zero;
        targetPosition = transform.position;
    }

    private void Update()
    {
        ClickTarget();
        FaceTarget();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void OnDisable()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(EnemyTag))
        {
            stopWalk();
        }
    }
    #endregion

    #region Public 

    #endregion

    #region Private
    // private void MovePlayer(Vector3 Pos)
    // {
    //     stopWalk();

    //     float distance = Vector3.Distance(Pos, rb.transform.position);
    //     float duration = distance / moveSpeed;

    //     _animator.SetBool("Walk", true);
    //     moveTweener = rb.DOMove(Pos, duration).OnComplete(
    //         () =>
    //         {
    //             _animator.SetBool("Walk", false);
    //         }
    //     );
    // }

    private void MovePlayer()
    {
        if (!isMoving) return;

        Vector3 direction = (targetPosition - rb.position).normalized;
        Vector3 newPosition = rb.position + direction * moveSpeed * Time.fixedDeltaTime;

        if (Vector3.Distance(rb.position, targetPosition) < 0.5f)
        {
            StopMovement();
            return;
        }

        rb.MovePosition(newPosition);
        _animator.SetBool("Walk", true);
    }

    private void StopMovement()
    {
        isMoving = false;
        _animator.SetBool("Walk", false);
        rb.velocity = Vector3.zero;
    }

    private void FaceTarget()
    {
        if (rb.transform.position != markerObject.transform.position)
        {
            Vector3 direction = (markerObject.transform.position - rb.transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            rb.rotation = Quaternion.Slerp(rb.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void ClickTarget()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 500f, groundLayer))
            {
                markerObject.transform.position = hit.point;
                markerObject.Play();
                targetPosition = hit.point;
                isMoving = true;
            }
        }
    }

    private void stopWalk()
    {
        if (moveTweener != null)
        {
            moveTweener.Kill();
            _animator.SetBool("Walk", false);
        }
    }

    float GetLookAtYRotation(Vector3 from, Vector3 to)
    {
        Vector3 direction = to - from;
        direction.y = 0;

        return Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
    }
    #endregion
}
