using _3D.Scripts.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace _3D.Scripts.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private GameObject _hitEffect;
        [SerializeField] private GameObject[] _destroyOnHit;
        [SerializeField] private UnityEvent _onHit;
        [SerializeField] private float _lifeTime = 4f;
        private Vector3 _direction;
        private float _foodValue;
        private Satiety _target;
        [SerializeField] private AudioSource _shootSound; 

        public void SetTarget(Vector3 direction, float foodValue)
        {
            _direction = direction;
            _foodValue = foodValue;

            Destroy(gameObject, _lifeTime);
        }

        private void Start()
        {
            transform.LookAt(_direction);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            _shootSound.Play();
        }

        private void Update()
        {
            if (_direction == null) return;

            transform.Translate(_speed * Time.deltaTime * Vector3.forward);
        }

        private void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponent<Satiety>();
            if (!target || !target.IsHungry) return;

            _target = target;
            _target.Feed(_foodValue);
            _onHit.Invoke();
            if (_hitEffect) SpawnHitEffect();
            DestroyOnHit();
            _target = null;
        }

        private void SpawnHitEffect()
        {
            var hitEffect = Instantiate(_hitEffect, _target.transform.position, transform.rotation);
            hitEffect.transform.parent = _target.transform;
        }

        private void DestroyOnHit()
        {
            foreach (GameObject toDestroy in _destroyOnHit)
            {
                toDestroy.SetActive(false);
            }
        }
    }
}
