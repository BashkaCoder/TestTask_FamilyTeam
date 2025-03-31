using System.Collections;
using _3D.Scripts.Attributes;
using UnityEngine;

namespace _3D.Scripts.Movement
{
    [RequireComponent(typeof(PlayerMover))]
    public class Dash : MonoBehaviour
    {
        [SerializeField] private KeyCode _dashKeyCode;
        [SerializeField] private float _dashTime = 0.25f;
        [SerializeField] private float _dashSpeed = 20;
        [SerializeField] private float _dashCooldown = 0.5f;
        [SerializeField] private TrailRenderer _trail;
        
        private PlayerMover _playerMover;
        private Health _health;
        
        private bool _canDash;

        private void Awake()
        {
            _playerMover = GetComponent<PlayerMover>();
            _health = GetComponent<Health>();

            _canDash = true;
            _trail.gameObject.SetActive(!_canDash);
        }

        private void Update()
        {
            if (!_health.IsDead && Input.GetKeyUp(_dashKeyCode))
            {
                if (!_canDash) return;
                StartCoroutine(DashCoroutine());
            }
        }

        private IEnumerator DashCoroutine()
        {
            SwitchDashParameters();

            float startTime = Time.time;
            while (Time.time < startTime + _dashTime)
            {
                _playerMover.Move(PlayerMover.Direction * _dashSpeed * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(_dashCooldown);

            SwitchDashParameters();
        }

        private void SwitchDashParameters()
        {
            _canDash = !_canDash;

            _trail.Clear();
            _trail.gameObject.SetActive(!_canDash);
            _health.IsInvulnerable = !_canDash;
        }
    }
}
