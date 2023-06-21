using UnityEngine;

namespace Project.Scripts.Player
{
    public class Bullet : MonoBehaviour
    {
        private bool _set;
        private float _speed, _lifetime, _activatedAt;
        private Vector3 _direction;

        public void Setup(float speed, Vector3 direction, float lifetime)
        {
            _speed = speed;
            _direction = direction;
            _lifetime = lifetime;
            _activatedAt = Time.time;
            
            _set = true;
        }

        private void Update()
        {
            if (!_set) return;
            if (_activatedAt + _lifetime < Time.time)
            {
                Destroy(gameObject);
            }

            
            
            transform.position += _direction * (_speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            
        }
    }
}