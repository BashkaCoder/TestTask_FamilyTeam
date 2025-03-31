using _3D.Scripts.Attributes;
using _3D.Scripts.UI;
using System.Linq;
using UnityEngine;

namespace _3D.Scripts
{
    public class EnemyZone : MonoBehaviour
    {
        private EnemyZoneManager _zoneManager;

        [Header("Enemy Settings")] 
        [SerializeField] private EnemyType enemyType;

        [Header("Zone Statistic")] 
        [SerializeField] private GameObject totemObject;
        [SerializeField] private ZoneView _zoneView;

        private int _enemyCount;
        private BoxCollider _collider;
        private Totem _totem;
        private int _fedEnemyCount;

        private void Awake()
        {
            _totem = totemObject.GetComponentInChildren<Totem>();
        }
    
        private int CalculateEnemyCount()
        {
            return FindObjectsOfType<Satiety>().Count(enemy => enemy.Type == enemyType);
        }

        public void Initialize(EnemyZoneManager zoneManager)
        {
            _enemyCount = CalculateEnemyCount();

            _zoneManager = zoneManager;
            _totem.Initialize(this);
            _totem.UpdateText(0, _enemyCount);
            _zoneView.UpdateText(0, _enemyCount);
        }

        public void AddScore()
        {
            if (++_fedEnemyCount == _enemyCount)
            {
                _totem.SetInteractionState(true);
            }
            _totem.UpdateText(_fedEnemyCount, _enemyCount);
            _zoneView.UpdateText(_fedEnemyCount, _enemyCount);
        }

        public void UpdateZoneView()
        {
            _zoneView.UpdateText(0, _enemyCount);
        }

        public void GatherKey()
        {
            _zoneManager.GatherCollectible();
        }
    }
}