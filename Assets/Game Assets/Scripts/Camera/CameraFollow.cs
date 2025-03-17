using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;

    #region Unity

    private void LateUpdate()
    {
        followPlayer();
    }

    #endregion

    #region Private
    private void followPlayer()
    {
        Vector3 newPos = player.transform.position + offset;
        this.transform.position = Vector3.Lerp(transform.position, newPos, smoothSpeed * Time.deltaTime);
    }
    #endregion
}
