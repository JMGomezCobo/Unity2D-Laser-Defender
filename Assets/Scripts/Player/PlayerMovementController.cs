using UnityEngine;
using UnityEngine.InputSystem;

namespace LaserDefender.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        private Vector2 rawInput;

        [SerializeField] private float paddingLeft;
        [SerializeField] private float paddingRight;
        [SerializeField] private float paddingTop;
        [SerializeField] private float paddingBottom;

        private Vector2 minBounds;
        private Vector2 maxBounds;

        private ShootController _shootController;

        private void Awake()
        {
            _shootController = GetComponent<ShootController>();
        }

        private void Start()
        {
            SetBounds();
        }

        private void Update()
        {
            Move();
        }

        private void SetBounds()
        {
            var mainCamera = Camera.main;

            if (mainCamera is null) return;
        
            minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
            maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
        }

        private void Move()
        {
            var delta = rawInput * (moveSpeed * Time.deltaTime);
            var newPosition = new Vector2();

            var currentPosition = transform.position;
        
            newPosition.x = Mathf.Clamp(currentPosition.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
            newPosition.y = Mathf.Clamp(currentPosition.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
            currentPosition = newPosition;
        
            transform.position = currentPosition;
        }

        private void OnMove(InputValue value)
        {
            rawInput = value.Get<Vector2>();
        }

        private void OnFire(InputValue value)
        {
            if (_shootController != null)
            {
                _shootController.isFiring = value.isPressed;
            }
        }
    }
}