using UnityEngine;

namespace _2D.Scripts.Combat
{
    //базовый класс для компонента атаки
    public abstract class AttackBase : MonoBehaviour
    {
        public bool IsAttacking { get; private set; }
        [SerializeField] protected AnimationClip _attackAnimationClip; //анимация атаки
        [SerializeField] protected float _attackCd; //кулдаун атаки
        protected Animator Animator;
        protected Vector3 Target;

        protected virtual void Awake()
        {
            Animator = GetComponent<Animator>();
        }
        
        private float GetAttackAnimationDuration()
        {
            return _attackAnimationClip.length;
        }

        public virtual void BeginAttack()
        {
            IsAttacking = true;
            Invoke(nameof(EndAttack), GetAttackAnimationDuration());
        }

        public virtual void EndAttack()
        {
            IsAttacking = false;
        }

        public float GetAttackCD()
        {
            return _attackCd;
        }

        public void SetTarget(Vector3 target)
        {
            Target = target;
        }
    }
}