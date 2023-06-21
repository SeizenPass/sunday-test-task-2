using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.Player
{
    public class Bullet : MonoBehaviour
    {
        private bool _set;
        private float _speed, _lifetime, _activatedAt;
        private Vector3 _direction;
        private LayerMask _contactLayer;

        public void Setup(float speed, Vector3 direction, float lifetime, LayerMask contactLayer)
        {
            _speed = speed;
            _direction = direction;
            _lifetime = lifetime;
            _activatedAt = Time.time;
            _contactLayer = contactLayer;
            
            _set = true;
        }

        private void Update()
        {
            if (!_set) return;
            if (_activatedAt + _lifetime < Time.time)
            {
                Death();
            }
            
            transform.position += _direction * (_speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (LayerMaskUtils.CompareLayerMasks(_contactLayer,
                    other.gameObject.layer))
            {
                Death();    
            }
        }

        private void Death()
        {
            Destroy(gameObject);
        }
    }
}