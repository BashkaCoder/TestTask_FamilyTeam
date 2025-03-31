using UnityEngine;

namespace _2D.Scripts.Environment
{
    public class ParallaxBehaviour : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float _horizontalMovementMultiplier;
        [SerializeField, Range(0, 1)] private float _verticalMovementMultiplier;
        [SerializeField] private Transform _target;

        private Vector3 TargetPosition => _target.position;
        
        private Vector3 _lastTargetPosition;
        private float _textureWidth;

        private void Start()
        {
            _lastTargetPosition = TargetPosition;
        }

        private void Update()
        {
            Vector3 delta = TargetPosition - _lastTargetPosition;
            delta *= new Vector2(_horizontalMovementMultiplier, _verticalMovementMultiplier);
            transform.position += delta;
            _lastTargetPosition = TargetPosition;
        }
    }
}