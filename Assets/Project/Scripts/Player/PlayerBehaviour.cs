using System;
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

        private void OnLook(InputValue value)
        {
            Debug.Log(value.Get<Vector2>());
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
                Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            
            rigidbody.MovePosition(rigidbody.position + movementDirection * (movementSpeed * Time.deltaTime));
        }
    }
}