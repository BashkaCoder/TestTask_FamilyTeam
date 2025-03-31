using _2D.Scripts.FSM;

namespace _2D.Scripts.Enemy.States
{
    public class IdleState : FsmState
    {
        private readonly PatrolEnemy _enemy;
        private readonly float _exitTime;
        private float _timer;

        public IdleState(Fsm fsm, PatrolEnemy enemy, float exitTime) : base(fsm)
        {
            _enemy = enemy;
            _exitTime = exitTime;
        }

        public override void Enter()
        {
            _timer = 0; 
        }

        public override void Update(float deltaTime)
        {
            _timer += deltaTime;
            if (_timer > _exitTime)
                Fsm.SetState<PatrolState>();

            if (_enemy is EnemyWithAttack e && e.PlayerInSight())
                Fsm.SetState<AttackState>();
        }
    }
}