using _3D.Scripts.Control;
using UnityEngine;
using UnityEngine.AI;

namespace _3D.Scripts.Movement
{
    public class AIMover : MonoBehaviour, IAction
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _maxSpeed = 6f;
        [SerializeField] private float _growthMultiplier = 0.9f;
        
        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction = 1f)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public bool CanMoveTo(Vector3 destination)
        {
            var path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
            if (!hasPath) return false;

            if (path.status != NavMeshPathStatus.PathComplete) return false;

            return true;
        }

        public void MoveTo(Vector3 destination, float speedFraction = 1f)
        {
            _navMeshAgent.destination = destination;
            _navMeshAgent.speed = _maxSpeed * Mathf.Clamp01(speedFraction);
            _navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            _navMeshAgent.isStopped = true;
        }

        public void UpdateParameters()
        {
            _maxSpeed *= _growthMultiplier;
        }
    }
}
