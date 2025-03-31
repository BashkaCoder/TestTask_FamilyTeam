using _2D.Scripts.Checks;
using UnityEngine;

namespace _2D.Scripts.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class CharacterSound : MonoBehaviour
    {
        [SerializeField] private AudioClip _jumpSound;
        [SerializeField] private AudioClip _attackSound;
        [SerializeField] private AudioClip _hurtSound;
        [SerializeField] private AudioClip _runSound;
        
        private AudioSource _audioSource;
        private Ground _ground;
        private float _inputX;
        private bool _isRunning;

        private void Awake()
        {
            _ground = GetComponent<Ground>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            //just started running
            if (_isRunning == false  && _ground.OnGround && _inputX != 0)
            {
                _isRunning = true;
                _audioSource.clip = _runSound;
                _audioSource.Play(0);
            }
            
            //finish running or jump
            if (_isRunning && (_ground.OnGround == false || _inputX == 0))
            {
                _isRunning = false;
                _audioSource.Stop();
            }
        }

        public void PlayJumpSound()
        {
            if (_jumpSound != null)
                _audioSource.PlayOneShot(_jumpSound);
        }
        
        public void PlayAttackSound()
        {
            if (_attackSound != null)
                _audioSource.PlayOneShot(_attackSound);
        }
        
        public void PlayHurtSound()
        {
            if (_hurtSound != null)
                _audioSource.PlayOneShot(_hurtSound);
        }

        public void SetInputX(float inputX)
        {
            _inputX = inputX;
        }
    }
}