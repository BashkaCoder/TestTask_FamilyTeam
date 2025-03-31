using _2D.Scripts.Checks;
using _2D.Scripts.Combat;
using UnityEngine;

namespace _2D.Scripts.Player
{
    [RequireComponent(typeof(Health), typeof(Ground), typeof(Rigidbody2D))]
    public class PlayerAnimation : MonoBehaviour
    {
        private Rigidbody2D _body;
        private Ground _ground;
        private Animator _animator;
        private Health _healthComponent;
        
        private static readonly int VelocityX = Animator.StringToHash("VelocityX");
        private static readonly int VelocityY = Animator.StringToHash("VelocityY");
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        private static readonly int Hurt = Animator.StringToHash("Hurt");

        private void OnEnable()
        {
            _healthComponent.OnDamageTaken += EnableHurtParameter;
            _healthComponent.OnDeath += EnableIsDeadParameter;
        }

        private void OnDisable()
        {
            _healthComponent.OnDamageTaken -= EnableHurtParameter;
            _healthComponent.OnDeath -= EnableIsDeadParameter;
        }

        private void Awake()
        {
            _healthComponent = GetComponent<Health>();
            _ground = GetComponent<Ground>();
            _body = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _animator.SetFloat(VelocityX, Mathf.Abs(_body.linearVelocity.x));
            _animator.SetFloat(VelocityY, _body.linearVelocity.y);
            _animator.SetBool(IsJumping, !_ground.OnGround);
        }

        public void SetInputX(float inputX)
        {
            if (inputX == 0) return;
            transform.localScale = new Vector2(
                Mathf.Sign(inputX) * Mathf.Abs(transform.localScale.x),
                transform.localScale.y
            );
        }

        private void EnableIsDeadParameter()
        {
            _animator.SetBool(IsDead, true);
        }

        private void EnableHurtParameter(int damage)
        {
            _animator.SetBool(Hurt, true);
        }

        public void DisableHurtParameter(float time)
        {
            Invoke(nameof(DisableHurtParameter), time);
        }

        private void DisableHurtParameter()
        {
            _animator.SetBool(Hurt, false);
        }
    }
}