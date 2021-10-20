using DefaultNamespace;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private const float MINDistance = 5f;
    private const float MAXDistance = 30f;
    private const float MINMagnitude = 1f;

    [SerializeField] public Transform objectToWatch;
    [SerializeField, Range(MINDistance, MAXDistance)] public float distanceFromObject;
    [SerializeField] public MouseButtons mouseButton;
    [SerializeField, Range(5, 30)] public float scrollSensitivity;
    [SerializeField, Range(0f, 1f)] public float momentumDelay = 0.5f;

    private Vector3 _previousPosition;
    private Vector3 _previousDirection;
    private Camera _camera;

    private void Start()
    {
        _camera = this.GetComponent<Camera>();
    }

    private void Update()
    {
        #region Rotate Camera

        // Pressed down (not held down, only the start)
        if (Input.GetMouseButtonDown((int)mouseButton) || Input.GetMouseButtonDown(0))
        {
            _previousPosition = _camera.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton((int)mouseButton) || Input.GetMouseButton(0))
        {
            _previousDirection = _previousPosition - _camera.ScreenToViewportPoint(Input.mousePosition);

            MoveCamera(_previousDirection);
        }
        else
        {
            // Decay Direction
            _previousDirection *= momentumDelay;
            MoveCamera(_previousDirection);
        }

        #endregion

        #region Scroll Zoom

        _camera.fieldOfView += Input.GetAxis("Mouse ScrollWheel") * -scrollSensitivity;
        _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, 10, 120);

        #endregion
    }

    private void MoveCamera(Vector3 direction)
    {
        _camera.transform.position = objectToWatch.position;
            
        _camera.transform.Rotate(new Vector3(1, 0, 0), direction.y * 100);
        _camera.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 100, Space.World);
        _camera.transform.Translate(new Vector3(0, 0, -distanceFromObject));
        _previousPosition = _camera.ScreenToViewportPoint(Input.mousePosition);
    }
}
