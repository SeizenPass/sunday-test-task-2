using System;
using UnityEngine;

namespace Project.Scripts
{
    public class AimConstrainer : MonoBehaviour
    {
        [SerializeField] private Transform aimCameraTransform;
        [SerializeField] private Transform targetPoint;
        [SerializeField] private LayerMask contactLayer;

        private void Update()
        {
            var position = aimCameraTransform.position;
            var diff = targetPoint.position - position;
            transform.position = Physics.Raycast(position, diff.normalized,
                out var hit, diff.magnitude, contactLayer) ? hit.point : targetPoint.position;
        }
    }
}