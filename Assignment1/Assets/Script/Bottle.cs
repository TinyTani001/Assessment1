using UnityEngine;

/// <summary>
/// This class handles the movement of bottle and trigger events
/// </summary>
public class Bottle : MonoBehaviour
{
    public Rigidbody BottleRigidbody;
    public float BottleShotForce = 12f, BottleDragMaxDistance, BottleDragMaxAngle, DirectionArrowOffset, BottleResetTime;
    public LayerMask MouseDetectionLayers;
    public GameObject DirectionArrowObject;

    private bool _canShootBottle, _isDragging, _resetTimerStarted;
    private Vector3 _startPosition, _dragStartPositon, _shootDirection, _offsetFromDragPosition, _bottlleDragLimit;
    private Plane _dragPlane;
    private float _forceModifier, _currentBottleResetTime;

    private void Start()
    {
        DirectionArrowObject.SetActive(false);
        _startPosition = BottleRigidbody.transform.position;
        _bottlleDragLimit = _startPosition + Vector3.forward * BottleDragMaxDistance;
        _dragPlane = new Plane(Vector3.up, 0f);
    }

    private void Update()
    {
        if (!_resetTimerStarted)
        {
            DetectMouseDown();

            DetectMouseDragging();

            DetectMouseUp();
        }
        else if (Time.time > _currentBottleResetTime + BottleResetTime)
        {
            _resetTimerStarted = false;
            BottleRigidbody.transform.position = _startPosition;
        }
    }

    private void FixedUpdate()
    {
        if (_canShootBottle)
        {
            _canShootBottle = false;
            BottleRigidbody.AddForce(_shootDirection * Mathf.Lerp(0f, BottleShotForce, _forceModifier), ForceMode.Impulse);
        }
    }

    private void DetectMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(r, 100f, MouseDetectionLayers))
            {
                if (_dragPlane.Raycast(r, out float hitDistance))
                {
                    _dragStartPositon = r.GetPoint(hitDistance);
                    Vector3 flatBottlePos = _startPosition;
                    flatBottlePos.y = _dragStartPositon.y;
                    _offsetFromDragPosition = flatBottlePos - _dragStartPositon;
                    _isDragging = true;
                    DirectionArrowObject.SetActive(true);
                }
            }
        }
    }

    private void DetectMouseDragging()
    {
        if (_isDragging)
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (_dragPlane.Raycast(r, out float hitDistance))
            {
                Vector3 hitPoint = r.GetPoint(hitDistance);
                if (Vector3.Angle(Vector3.back, hitPoint - _dragStartPositon) < BottleDragMaxAngle * 0.5f)
                {
                    float dragDistance = (hitPoint - _dragStartPositon).sqrMagnitude;
                    float maxDistance = (_startPosition - _bottlleDragLimit).sqrMagnitude;

                    _forceModifier = Mathf.InverseLerp(0f, maxDistance, dragDistance);

                    if (dragDistance > maxDistance)
                    {
                        hitPoint = _dragStartPositon + (hitPoint - _dragStartPositon).normalized * BottleDragMaxDistance;
                    }

                    _shootDirection = (_dragStartPositon - hitPoint).normalized;
                    Vector3 arrowPos = BottleRigidbody.position + _shootDirection * DirectionArrowOffset;
                    DirectionArrowObject.transform.position = arrowPos + Vector3.up * 0.01f;
                    DirectionArrowObject.transform.rotation = Quaternion.LookRotation(_shootDirection);

                    Vector3 bottlePos;
                    bottlePos = hitPoint + _offsetFromDragPosition;
                    bottlePos.y = _startPosition.y;
                    BottleRigidbody.MovePosition(bottlePos);
                }
            }
        }
    }

    private void DetectMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (_isDragging)
            {
                _isDragging = false;
                Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (_dragPlane.Raycast(r, out float hitDistance))
                {
                    DirectionArrowObject.SetActive(false);
                    _canShootBottle = true;
                    BottleRigidbody.velocity = Vector3.zero;
                    _resetTimerStarted = true;
                    _currentBottleResetTime = Time.time;
                }
            }
        }
    }
}
