using System;
using UnityEngine;

namespace Project.Scripts
{
    public class ZoneChecker : MonoBehaviour
    {
        [SerializeField] private float radius = 1;

        public bool Check(LayerMask layerMask)
        {
            return Physics.CheckSphere(transform.position, radius, layerMask);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}