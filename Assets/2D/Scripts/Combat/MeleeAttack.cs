using UnityEngine;

namespace _2D.Scripts.Combat
{
    public class MeleeAttack : AttackBase
    {
        [SerializeField] InstantDamageDealer _damageDealer;
        
        private static readonly int Attacking = Animator.StringToHash("IsAttacking");

        public override void BeginAttack()
        {
            if (IsAttacking) return;

            base.BeginAttack();
            Animator.SetBool(Attacking, true);
        }

        public override void EndAttack()
        {
            base.EndAttack();
            Animator.SetBool(Attacking, false);
        }
    }
}