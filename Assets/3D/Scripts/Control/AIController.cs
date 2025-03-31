using _3D.Scripts.Attributes;
using _3D.Scripts.Combat;
using _3D.Scripts.Movement;
using UnityEngine;

namespace _3D.Scripts.Control
{
    public class AIController : MonoBehaviour
    {
        [Header("Attack Behaviour")]
        [SerializeField, Range(1f, 100f), Tooltip("Радиус обнаружения врага.")]
        private float _detectionRadius = 5f;
        [SerializeField] private float _allowedDistanceDeparture = 30f;

        private AIFighter _fighter;
        private GameObject _player;
        [Header("Suspition Behaviour")]
        [SerializeField] private float _suspicionTime = 5f;
        private float _timeSinceLastSawPlayer = Mathf.Infinity;
        private Satiety _satiety;

        [Header("Patrol Behaviour")]
        [SerializeField] private PatrolPath.PatrolPath _patrolPath;
        [SerializeField] private float _waypointTolerance = 1f;
        [SerializeField] private float _waypointDwellTime = 3f;
        [SerializeField, Range(0, 1)] private float _patrolSpeedFraction = 0.2f;
        private float _timeSinceArrivedAtWaypoint = Mathf.Infinity;
        private int _currentWaypointIndex = 0;
        private AIMover _mover;
        private Vector3 _guardPosition;

        [SerializeField] private float _growthMultiplier = 1.2f;

        private void Awake()
        {
            _fighter = GetComponent<AIFighter>();
            _mover = GetComponent<AIMover>();
            _player = GameObject.FindWithTag("Player");
            _satiety = GetComponent<Satiety>();
        }

        private void Start()
        {
            _guardPosition = transform.position;
        }

        private void Update()
        {
            if (!_satiety.IsHungry)
            {
                PatrolBehaviour();
                UpdateTimers();
                return;
            }

            if (InAttackRangeOfPlayer() &&
                _fighter.CanAttack(_player) &&
                !IsFarFromStartPosition(_guardPosition))
            {
                AttackBehaviour();
            }
            else if (_timeSinceLastSawPlayer < _suspicionTime)
            {
                SuspitionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            UpdateTimers();
        }

        #region AttackBehaviour
        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
            return distanceToPlayer < _detectionRadius;
        }

        private bool IsFarFromStartPosition(Vector3 startPosition)
        {
            return Vector3.Distance(startPosition, transform.position) > _allowedDistanceDeparture;
        }

        private void AttackBehaviour()
        {
            _timeSinceLastSawPlayer = 0;
            _fighter.Attack(_player);
        }
        #endregion

        #region SuspitionBehaviour
        private void SuspitionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
        #endregion

        #region PatrolBehaviour
        private void PatrolBehaviour()
        {
            Vector3 nextPosition = _guardPosition;

            if (_patrolPath && _patrolPath.Waypoints.Length > 0)
            {
                if (AtWaypoint())
                {
                    _timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            if (_timeSinceArrivedAtWaypoint > _waypointDwellTime)
            {
                _mover.StartMoveAction(nextPosition, _patrolSpeedFraction);
            }
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < _waypointTolerance;
        }

        private void CycleWaypoint()
        {
            _currentWaypointIndex = _patrolPath.GetNextIndex(_currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint() => _patrolPath.GetWaypoint(_currentWaypointIndex);
        #endregion

        private void UpdateTimers()
        {
            _timeSinceLastSawPlayer += Time.deltaTime;
            _timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        public void UpdateParameters()
        {
            _detectionRadius *= _growthMultiplier;
        }

        #region Debug
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _detectionRadius);
        }
        #endregion
    }
}
