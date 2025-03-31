using System;
using JetBrains.Annotations;
using UnityEngine;

namespace _2D.Scripts.Combat
{
    public class Health : MonoBehaviour, IDamageable, IHealable
    {
        [field: SerializeField] public int DefaultMaxHealth { get; private set; }
        
        public event Action OnDeath; //событие, вызываемое при смерти
        public event Action<int> OnDamageTaken; //событие, вызываемое при получении урона
        public event Action<int> OnHealed; //событие, вызываемое при восстановлении здоровья
        
        public int CurrentHealth { get; private set; }

        [SerializeField, CanBeNull]
        private GameObject _hitAnimation;
        
        private void Start()
        {
            CurrentHealth = DefaultMaxHealth;
        }

        public void TakeDamage(int amount)
        {
            if (amount < 0) //нельзя нанести отрицательный урон
                throw new ArgumentOutOfRangeException($"Damage amount can't be negative!: {gameObject.name}");

            CurrentHealth -= amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, DefaultMaxHealth); //здоровье не может быть < 0

            OnDamageTaken?.Invoke(amount);

            if (_hitAnimation != null)
            {
                _hitAnimation.SetActive(true);
                Invoke(nameof(DisableHitAnimation), 0.2f);
            }
            
            if (CurrentHealth == 0) 
                OnDeath?.Invoke();
        }

        public void Heal(int amount)
        {
            if (amount < 0) // нельзя захилить отрицательное кол-во хп
                throw new ArgumentOutOfRangeException($"amount should be positive: {gameObject.name}");
            CurrentHealth += amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, DefaultMaxHealth);
            OnHealed?.Invoke(amount);
        }

        private void DisableHitAnimation() => _hitAnimation?.SetActive(false);
    }
}