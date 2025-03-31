using UnityEngine;

namespace _3D.Scripts
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (_particleSystem && !_particleSystem.IsAlive()) Destroy(gameObject);
        }
    }
}
