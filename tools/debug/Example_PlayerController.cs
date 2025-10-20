using UnityEngine;
using YourProject.Core.Debug;

namespace YourProject.Game
{
    /// <summary>
    /// Ví dụ sử dụng IDebugLogger với DI pattern
    /// </summary>
    public class Example_PlayerController : MonoBehaviour
    {
        #region Fields
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private float health = 100f;
        
        [Header("References")]
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform groundCheck;
        
        // Debug
        private IDebugLogger debugLogger;
        private const string TAG = "[PlayerController]";
        
        // Private fields
        private bool isGrounded;
        private float horizontalInput;
        private float lastHealth;
        #endregion

        #region Unity Lifecycle
        private void Awake()
        {
            // Inject debug logger từ ServiceContainer
            // debugLogger = ServiceContainer.Resolve<IDebugLogger>();
            
            // Fallback: sử dụng static access nếu DI không available
            debugLogger = DebugLogger.Instance;
            
            debugLogger.Log(TAG, "PlayerController Awake called");
        }

        private void Start()
        {
            lastHealth = health;
            debugLogger.Log(TAG, $"PlayerController initialized with health: {health}");
        }

        private void Update()
        {
            HandleInput();
            CheckHealthChange();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void OnDestroy()
        {
            debugLogger.Log(TAG, "PlayerController destroyed");
        }
        #endregion

        #region Public Methods
        public void Jump()
        {
            if (isGrounded)
            {
                debugLogger.Log(TAG, $"Jumping with force: {jumpForce}");
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else
            {
                debugLogger.LogWarning(TAG, "Attempted to jump while not grounded");
            }
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            debugLogger.LogWarning(TAG, $"Player took {damage} damage. Health: {health}");
            
            if (health <= 0)
            {
                debugLogger.LogError(TAG, "Player died!");
                OnPlayerDied();
            }
        }

        public void SetMoveSpeed(float speed)
        {
            debugLogger.Log(TAG, $"Move speed changed from {moveSpeed} to {speed}");
            moveSpeed = speed;
        }
        #endregion

        #region Private Methods
        private void HandleInput()
        {
            horizontalInput = Input.GetAxis("Horizontal");
            
            if (Input.GetButtonDown("Jump"))
            {
                debugLogger.Log(TAG, "Jump input detected");
                Jump();
            }
            
            // Conditional logging cho expensive operations
            debugLogger.LogIf(horizontalInput != 0, TAG, $"Horizontal input: {horizontalInput:F2}");
        }

        private void HandleMovement()
        {
            Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(transform.position + movement);
        }

        private void CheckHealthChange()
        {
            // Log health change với conditional logging
            debugLogger.LogIf(health != lastHealth, TAG, 
                $"Health changed from {lastHealth} to {health}");
            lastHealth = health;
        }

        private void OnPlayerDied()
        {
            debugLogger.LogError(TAG, "Player death event triggered");
            // Death logic here
        }
        #endregion

        #region Collision Events
        private void OnTriggerEnter(Collider other)
        {
            debugLogger.Log(TAG, $"Trigger entered with: {other.name}");
            
            if (other.CompareTag("Enemy"))
            {
                debugLogger.LogWarning(TAG, "Player hit enemy!");
                TakeDamage(10f);
            }
            else if (other.CompareTag("PowerUp"))
            {
                debugLogger.Log(TAG, "Player collected power-up!");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            debugLogger.LogIf(other.CompareTag("SafeZone"), TAG, "Player left safe zone");
        }

        private void OnCollisionEnter(Collision collision)
        {
            debugLogger.LogIf(collision.relativeVelocity.magnitude > 5f, TAG, 
                $"Hard collision detected with velocity: {collision.relativeVelocity.magnitude:F2}");
        }
        #endregion

        #region Debug Methods
        /// <summary>
        /// Method để test debug logging từ Inspector
        /// </summary>
        [ContextMenu("Test Debug Logging")]
        public void TestDebugLogging()
        {
            debugLogger.Log(TAG, "Test log message");
            debugLogger.LogWarning(TAG, "Test warning message");
            debugLogger.LogError(TAG, "Test error message");
            
            try
            {
                throw new System.Exception("Test exception for debugging");
            }
            catch (System.Exception ex)
            {
                debugLogger.LogException(TAG, ex);
            }
        }

        /// <summary>
        /// Method để test conditional logging
        /// </summary>
        [ContextMenu("Test Conditional Logging")]
        public void TestConditionalLogging()
        {
            debugLogger.LogIf(true, TAG, "This should appear");
            debugLogger.LogIf(false, TAG, "This should NOT appear");
            debugLogger.LogWarningIf(health < 50, TAG, "Health is low!");
            debugLogger.LogErrorIf(health <= 0, TAG, "Player is dead!");
        }
        #endregion
    }
}
