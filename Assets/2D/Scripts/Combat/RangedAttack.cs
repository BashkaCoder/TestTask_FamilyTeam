using UnityEngine;

namespace _2D.Scripts.Combat
{
    public class RangedAttack : AttackBase
    {
        [SerializeField] private Projectile _projectilePrefab; // префаб проджектайла
        [SerializeField] private float _projectileSpawnOffset; // отступ, на котором будет спавниться проджектайл
        [SerializeField] private Transform _projectileParent;
        
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

            Vector3 toTarget = (Target - transform.position).normalized;
            Projectile projectile = Instantiate(
                _projectilePrefab,
                transform.position + toTarget*_projectileSpawnOffset,
                Quaternion.identity,
                _projectileParent
            );
            projectile.SetDirection(toTarget);
            projectile.SetDamageMultiplier(1f);
        }
    }
}
