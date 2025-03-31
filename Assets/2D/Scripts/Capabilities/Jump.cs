using _2D.Scripts.Checks;
using UnityEngine;

namespace _2D.Scripts.Capabilities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Jump : MonoBehaviour, IJump
    {
        public bool CanJump => _jumpPhase < _maxAirJumps + 1;
        
        [SerializeField, Range(0f, 10f)] private float _jumpHeight = 3f;
        [SerializeField, Range(0, 5)] private int _maxAirJumps = 0;
        [SerializeField, Range(0f, 5f)] private float _downwardMovementMultiplier = 3f;
        [SerializeField, Range(0f, 5f)] private float _upwardMovementMultiplier = 1.7f;
        [SerializeField] private float _jumpWindowTime;

        private Rigidbody2D _body;
        private Ground _ground;
        private Vector2 _velocity;

        private int _jumpPhase;
        private float _defaultGravityScale, _jumpSpeed;
        private float _jumpHeightMultiplier;

        private bool _desiredJump, _onGround;

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _ground = GetComponent<Ground>();

            _defaultGravityScale = 1f;
        }

        private void Start()
        {
            _jumpHeightMultiplier = 1f;
        }

        public void Action()
        {
            _desiredJump = true;
            Invoke(nameof(ResetJump), _jumpWindowTime);
        }

        private void ResetJump()
        {
            _desiredJump = false;
        }
        
        private void FixedUpdate()
        {
            _onGround = _ground.OnGround;
            _velocity = _body.linearVelocity;

            if (_onGround)
            {
                _jumpPhase = 0;
            }

            if (_desiredJump)
            {
                JumpAction();
            }

            _body.gravityScale = _body.linearVelocity.y switch
            {
                > 0 => _upwardMovementMultiplier,
                < 0 => _downwardMovementMultiplier,
                0 => _defaultGravityScale,
                _ => _body.gravityScale
            };

            _body.linearVelocity = _velocity;
        }
        private void JumpAction()
        {
            if (_onGround || _jumpPhase < _maxAirJumps)
            {
                _desiredJump = false;

                _jumpPhase += 1;
                
                _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _jumpHeight * _jumpHeightMultiplier);
                
                switch (_velocity.y)
                {
                    case > 0f:
                        _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
                        break;
                    case < 0f:
                        _jumpSpeed += Mathf.Abs(_body.linearVelocity.y);
                        break;
                }
                _velocity.y += _jumpSpeed;
            }
        }
    }
}

