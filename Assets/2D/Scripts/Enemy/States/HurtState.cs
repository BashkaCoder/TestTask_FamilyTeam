using _2D.Scripts.Combat;
using _2D.Scripts.FSM;
using UnityEngine;

namespace _2D.Scripts.Enemy.States
{
    public class HurtState : FsmState
    {
        private readonly Animator _animator;
        private readonly float _exitTime;
        private readonly Health _health;
        private float _timer;
        private static readonly int Hurt = Animator.StringToHash("Hurt");

        public HurtState(Fsm fsm, PatrolEnemy enemy, float exitTime) : base(fsm)
        {
            _exitTime = exitTime;
            _animator = enemy.GetComponent<Animator>();
            _health = enemy.GetComponent<Health>();
        }

        public override void Enter()
        {
            _animator.SetTrigger(Hurt);
            _timer = 0;
            
            if (0 == _health.CurrentHealth)
                Fsm.SetState<DeadState>();
        }

        public override void Update(float deltaTime)
        {
            _timer += deltaTime;
            if (_timer >= _exitTime)
                Fsm.SetState<PatrolState>();
        }
    }
}