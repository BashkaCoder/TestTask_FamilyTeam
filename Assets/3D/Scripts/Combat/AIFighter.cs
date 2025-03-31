using _3D.Scripts.Attributes;
using _3D.Scripts.Control;
using _3D.Scripts.Movement;
using UnityEngine;

namespace _3D.Scripts.Combat
{
    public class AIFighter : MonoBehaviour, IAction
    {
        [SerializeField] private float _timeBetweenAttacks = 1f;
        [SerializeField, Range(0, 20f)] private float _attackRange = 2f;
        [SerializeField] private float _hitDamage = 2f;
        [SerializeField] private float _growthMultiplier = 1.2f;

        private float _timeSinceLastAttack = Mathf.Infinity;
        private AIMover _mover;
        private Health _target;
        
        private void Awake()
        {
            _mover = GetComponent<AIMover>();
        }

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;

            if (!_target || _target.IsDead) return;

            if (GetIsInAttackRange(_target.transform))
            {
                _mover.Cancel();
                AttackBehaviour();
            }
            else
            {
                _mover.MoveTo(_target.transform.position);
            }
        }

        private bool GetIsInAttackRange(Transform target) =>
            Vector3.Distance(transform.position, target.position) < _attackRange;

        private void AttackBehaviour()
        {
            transform.LookAt(_target.transform);

            if (_timeSinceLastAttack > _timeBetweenAttacks)
            {
                Hit();
                _timeSinceLastAttack = 0;
            }
        }

        private void Hit()
        {
            _target.TakeDamage(_hitDamage);
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (!combatTarget ||
                (!_mover.CanMoveTo(combatTarget.transform.position) &&
                !GetIsInAttackRange(combatTarget.transform)))
                return false;

            var target = combatTarget.GetComponent<Health>();
            return target && !target.IsDead;
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            _target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            _target = null;
            _mover.Cancel();
        }

        public void UpdateParameters()
        {
            _hitDamage *= _growthMultiplier;
        }

        #region Debug
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
        #endregion
    }
}
