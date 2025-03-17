using UnityEngine;
using UnityEngine.EventSystems;

public class PositionMarker : MonoBehaviour
{
    public LayerMask groundLayer;
    [SerializeField] private Transform markerObject;

    #region Unity
    private void Start()
    {
        markerObject.transform.position = Vector3.up * -10f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 500f, groundLayer))
            {
                markerObject.transform.position = hit.point;
                EventController.TriggerEvent(GameEvent.EVENT_POSITION_MARKED, markerObject.transform.position);
            }
        }
    }
    #endregion
}
