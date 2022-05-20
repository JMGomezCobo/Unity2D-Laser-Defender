using UnityEngine;
using UnityEngine.InputSystem;

namespace LaserDefender.Player
{
    [RequireComponent(typeof(Shooter))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float _moveSpeed = 5f;

        [Header("Bounds Settings")]
        [SerializeField] private float _paddingBottom = 1f;
        [SerializeField] private float _paddingTop    = 1f;
        [SerializeField] private float _paddingLeft   = 5f;
        [SerializeField] private float _paddingRight  = 5f;

        private Vector2 _minBounds;
        private Vector2 _maxBounds;
        private Vector2 _rawInput;

        private Camera _mainCamera;
        private Shooter _shooter;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _shooter = GetComponent<Shooter>();
        }

        private void Start()
        {
            SetBound();
        }

        private void Update()
        {
            HandleMovement();
        }

        private void SetBound()
        {
            _minBounds = _mainCamera.ViewportToWorldPoint(new Vector2(0,0));
            _maxBounds = _mainCamera.ViewportToWorldPoint(new Vector2(1,1));
        }

        private void HandleMovement()
        {
            var delta = _rawInput * (_moveSpeed * Time.deltaTime);
            var currentPosition = transform.position;
        
            var newPosition = new Vector2
            {
                x = Mathf.Clamp(currentPosition.x + delta.x, _minBounds.x + _paddingLeft,   _maxBounds.x - _paddingRight),
                y = Mathf.Clamp(currentPosition.y + delta.y, _minBounds.y + _paddingBottom, _maxBounds.y - _paddingTop)
            };

            currentPosition = newPosition;
        
            transform.position = currentPosition;
        }

        private void OnMove(InputValue value)
        {
            _rawInput = value.Get<Vector2>();
        }

        private void OnFire(InputValue value)
        {
            if (_shooter != null)
            {
                _shooter.isFiring = value.isPressed;
            }
        }
    }
}