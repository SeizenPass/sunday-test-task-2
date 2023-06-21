using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts.Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 1;
        [SerializeField] private float rotationSpeed = 1;
        [SerializeField] private float jumpForce = 1;
        
        [SerializeField] private Animator animator;
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private ZoneChecker groundChecker;
        [SerializeField] private CombatSystem combatSystem;
        
        
        
        
        
        private Transform _gameCameraTransform;
        private Vector2 _movementVector;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Running = Animator.StringToHash("Running");
        private static readonly int Jump = Animator.StringToHash("Jump");

        private void Start()
        {
            if (Camera.main != null) _gameCameraTransform = Camera.main.transform;
        }

        private void OnMove(InputValue value)
        {
            _movementVector = value.Get<Vector2>();
        }
        
        private void OnJump(InputValue value)
        {
            if (!IsOnGround()) return;
            animator.SetTrigger(Jump);
            rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }


        private bool IsOnGround()
        {
            return groundChecker.Check(groundLayer);
        }

        private void FixedUpdate()
        {
            var forward = _gameCameraTransform.forward;
            forward.y = 0;
            forward.Normalize();
            
            var right = _gameCameraTransform.right;
            right.y = 0;
            right.Normalize();
            
            var movementDirection = forward * _movementVector.y + right * _movementVector.x;

            movementDirection.Normalize();

            if (movementDirection.magnitude > 0.05f)
            {
                animator.SetBool(Running, true);
            }
            else
            {
                animator.SetBool(Running, false);
            }
            
            if (movementDirection != Vector3.zero)
            {
                if (!combatSystem.Aiming)
                {
                    var targetRotation = Quaternion.LookRotation(movementDirection);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed
                        * Time.fixedDeltaTime);
                }
                
            }
            
            rigidbody.MovePosition(rigidbody.position + movementDirection * (movementSpeed * Time.fixedDeltaTime));
        }

        private Vector3 MultipleVectorValues(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }
    }
}