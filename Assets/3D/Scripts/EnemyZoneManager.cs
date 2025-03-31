using System.Collections.Generic;
using _3D.Scripts.UI;
using UnityEngine;

namespace _3D.Scripts
{
    public class EnemyZoneManager : MonoBehaviour
    {
        [Header("Zones")]
        [SerializeField] private EnemyZone wolfZone;
        [SerializeField] private EnemyZone boarZone;
        [SerializeField] private EnemyZone spiderZone;
    
        [Header("Collectibles")]
        private static int _zonesMaxCount;
        private static int _zonesCleared;

        [Header("Totems")]
        [SerializeField] private TotemIndicator _totemIndicator;
    
        private static Dictionary<EnemyType, EnemyZone> _zones;

        private void Start()
        {
            _zonesMaxCount = 3;
            _zonesCleared = 0;
        
            _zones = new Dictionary<EnemyType, EnemyZone>()
            {
                {EnemyType.MonkeyType1, wolfZone},
                {EnemyType.MonkeyType2, boarZone},
                {EnemyType.MonkeyType3, spiderZone}
            };
        
            foreach (var pair in _zones)
            {
                var zone = pair.Value;
                zone.Initialize(this);
            }
        }

        public static void IncreaseFullFeedEnemyCount(EnemyType type)
        {
            _zones[type].AddScore();
        }

        public void GatherCollectible()
        {
            _zonesCleared++;
            _totemIndicator.UpdateImage(_zonesCleared);
        }

        public static bool AllZonesCleared()
        {
            return _zonesCleared == _zonesMaxCount;
        }
    }
}