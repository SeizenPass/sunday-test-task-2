using System.Collections;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private new Rigidbody rigidbody;
        

        private bool _set;
        private float _speed, _lifetime, _activatedAt;
        private Vector3 _direction;
        private LayerMask _contactLayer;

        private bool _dead;

        public void Setup(float speed, Vector3 direction, float lifetime, LayerMask contactLayer, Vector3 pos)
        {
            _speed = speed;
            _direction = direction;
            _lifetime = lifetime;
            _activatedAt = Time.time;
            _contactLayer = contactLayer;
            _dead = false;
            transform.position = pos;
            meshRenderer.enabled = true;
            particle.gameObject.SetActive(true);
            
            _set = true;
            gameObject.SetActive(true);
        }

        private void FixedUpdate()
        {
            if (!_set || _dead) return;
            if (_activatedAt + _lifetime < Time.time)
            {
                Death();
            }
            
            rigidbody.MovePosition(rigidbody.position + _direction * (_speed * Time.fixedDeltaTime));
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
            if (_dead) return;
            _dead = true;
            
            meshRenderer.enabled = false;
            particle.Play();

            StartCoroutine(LastWait());
        }

        private IEnumerator LastWait()
        {
            yield return new WaitForSeconds(1f);

            _set = false;
            
            gameObject.SetActive(false);
        }
    }
}