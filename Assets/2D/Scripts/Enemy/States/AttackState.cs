using _2D.Scripts.Combat;
using _2D.Scripts.FSM;
using UnityEngine;

namespace _2D.Scripts.Enemy.States
{
    public class AttackState : FsmState
    {
        private readonly EnemyWithAttack _enemy;
        private readonly AttackBase _attack;

        public AttackState(
            Fsm fsm,
            EnemyWithAttack enemy,
            AttackBase attack
        ) : base(fsm)
        {
            _enemy = enemy;
            _attack = attack;
        }

        public override void Update(float deltaTime)
        {
            if (_enemy.CanAttack && _enemy.PlayerInSight())
            {
                RaycastHit2D hit = _enemy.CheckPlayerHit();
                if (hit) _attack.SetTarget(hit.transform.position);
                _attack.BeginAttack();
                _enemy.StartCoroutine(_enemy.HandleAttackCD());
            }

            if (_enemy.PlayerInSight() == false && _attack.IsAttacking == false)
            {
                Fsm.SetState<PatrolState>();
            }
        }
    }
}