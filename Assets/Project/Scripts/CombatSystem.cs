using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Project.Scripts
{
    public class CombatSystem : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject aimCamera;
        [SerializeField] private string aimLayerName = "Upper Body Layer";
        [SerializeField] private Transform aimPoint;
        [SerializeField] private float aimRadius = 5;
        [SerializeField] private float aimMoveSpeed = 2;
        
        
        public UnityEvent<bool> onAimToggle;
        
        private bool _aiming;
        private int _aimLayerIndex;

        private Vector2 _lookVector;
        
        private Vector3 _initialRelativePos;

        public bool Aiming => _aiming;

        private void Start()
        {
            _initialRelativePos = aimPoint.localPosition;
            
            aimCamera.SetActive(_aiming);
            _aimLayerIndex = animator.GetLayerIndex(aimLayerName);
        }

        private void OnToggleAim(InputValue value)
        {
            _aiming = !_aiming;
            
            onAimToggle.Invoke(_aiming);
            
            aimCamera.SetActive(_aiming);
            animator.SetLayerWeight(_aimLayerIndex, _aiming ? 1 : 0);
            if (!_aiming)
            {
                aimPoint.localPosition = _initialRelativePos;
            }
        }

        private void OnLook(InputValue value)
        {
            _lookVector = value.Get<Vector2>();
        }

        private void Update()
        {
            var dir = new Vector3
            {
                y = _lookVector.y,
                x = _lookVector.x,
                z = _lookVector.x
            };
            
            dir.Normalize();

            var curPos = aimPoint.localPosition;
            curPos += dir * (aimMoveSpeed * Time.deltaTime);

            curPos.x = AimClamp(curPos.x, _initialRelativePos.x);
            curPos.y = AimClamp(curPos.y, _initialRelativePos.y);
            curPos.z = AimClamp(curPos.z, _initialRelativePos.z);

            aimPoint.localPosition = curPos;
        }

        private float AimClamp(float pos, float initial)
        {
            return Mathf.Clamp(pos, initial - aimRadius, initial + aimRadius);
        }

        private void OnShoot(InputValue val)
        {
            Debug.Log(val.isPressed);
        }

        private void OnDrawGizmosSelected()
        {
            if (aimPoint) Gizmos.DrawWireSphere(aimPoint.position, aimRadius);
        }
    }
}