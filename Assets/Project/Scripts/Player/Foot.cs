using System;
using Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Scripts.Player
{
    public class Foot : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayer;

        public UnityEvent<Vector3> onContact;

        private void OnTriggerEnter(Collider other)
        {
            if (LayerMaskUtils.CompareLayerMasks(groundLayer, other.gameObject.layer))
            {
                onContact.Invoke(other.ClosestPointOnBounds(transform.position));
            }
        }
    }
}