using _3D.Scripts.Attributes;
using UnityEngine;

namespace _3D.Scripts.Movement
{
    [RequireComponent(typeof(Health))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private AudioSource _moveSound;
        [SerializeField] private float _soundDelay = 0.2f;
        [SerializeField] private GameObject _slices;
        
        [SerializeField] private float _speed = 10f;
        
        private Rigidbody _rigidbody;
        private Health _health;
        private Animator _animator;
        
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");

        public static Vector3 Direction => new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        private static bool IsStopped => Direction != Vector3.zero;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _health = GetComponent<Health>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (_health.IsDead) return;
            Move(_speed * Time.deltaTime * Direction);
        }

        public void Move(Vector3 direction)
        {
            _rigidbody.MovePosition(_rigidbody.position + direction);
            RotateSprite();
            PlayMoveSound();
            _animator.SetFloat(Horizontal, Mathf.Abs(direction.x));
        }

        private void RotateSprite()
        {
            var horisontal = Input.GetAxis("Horizontal");
            if (horisontal != 0 && horisontal < 0)
            {
                FlipScale(true);
                _sprite.flipX = true;
            }
            else if (horisontal != 0 && horisontal > 0)
            {
                FlipScale(false);
                _sprite.flipX = false;
            }
        }

        private void PlayMoveSound()
        {
            if (IsStopped && !_moveSound.isPlaying)
            {
                _moveSound.PlayDelayed(_soundDelay);
            }
        }
        
        private void FlipScale(bool flip)
        {
            Vector3 localScale = _slices.transform.localScale;
            localScale.x = flip ? -Mathf.Abs(localScale.x) : Mathf.Abs(localScale.x);
            _slices.transform.localScale = localScale;
        }
    }
}
