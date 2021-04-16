using System;
using System.Collections.Generic;
using MyProject.Player;
using UnityEngine;

namespace MyProject.Character
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] private float health = 3;
        [SerializeField] private float maxHealth = 3;
        protected float Health
        {
            get => health;
            set => health = value;
        }
        
        public float MaxHealth
        {
            get => maxHealth;
            protected set => maxHealth = value;
        }
        protected Action<float, float> OnHPChange { get; set; }
        
        private readonly List<IHPChangeListener> _hpChangeListeners = new List<IHPChangeListener>();
        
        public abstract void TakeDamage(float amount);
    
        private void Awake()
        {
            OnHPChange += (f, f1) =>
            {
                foreach (var hpChange in _hpChangeListeners)
                {
                    hpChange.UpdateHP(f,f1);
                }
            };
        }

        public void AddListener(IHPChangeListener hpChangeListeners)
        {
            if (!_hpChangeListeners.Contains(hpChangeListeners))
            {
                _hpChangeListeners.Add(hpChangeListeners);
            }
        }
    
        public void RemoveListener(IHPChangeListener hpChangeListeners)
        {
            if (_hpChangeListeners.Contains(hpChangeListeners))
            {
                _hpChangeListeners.Remove(hpChangeListeners);
            }
        }
    }
}