using _2D.Scripts.Combat;
using _2D.Scripts.FSM;
using UnityEngine;

namespace _2D.Scripts.Enemy.States
{
    public class DeadState : FsmState
    {
        private readonly PatrolEnemy _enemy;
        private readonly Animator _animator;
        private readonly bool _respawn;
        private readonly float _respawnTime;
        private float _timer;
        
        private static readonly int IsDead = Animator.StringToHash("IsDead");

        public DeadState(Fsm fsm, PatrolEnemy enemy, bool respawn, float respawnTime) : base(fsm)
        {
            _respawn = respawn;
            _respawnTime = respawnTime;
            _animator = enemy.GetComponent<Animator>();
            _enemy = enemy;
        }

        public override void Enter()
        {
            _animator.SetBool(IsDead, true);
            _timer = 0;
        }

        public override void Update(float deltaTime)
        {
            if (!_respawn) return;
            _timer += deltaTime;
            if (_timer > _respawnTime)
                Fsm.SetState<PatrolState>();
        }

        public override void Exit()
        {
            _animator.SetBool(IsDead, false);
            Health health = _enemy.GetComponent<Health>();
            health.Heal(health.DefaultMaxHealth);
        }
    }
}