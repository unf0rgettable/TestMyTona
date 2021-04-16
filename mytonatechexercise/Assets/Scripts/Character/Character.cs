using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    private List<IHPChangeListener> HpChangeListeners = new List<IHPChangeListener>();

    [SerializeField] private float health = 3;

    public float Health
    {
        get => health;
        protected set => health = value;
    }

    [SerializeField] private float maxHealth = 3;

    public float MaxHealth
    {
        get => maxHealth;
        protected set => maxHealth = value;
    }
    

    protected Action<float, float> OnHPChange { get; set; }

    public abstract void TakeDamage(float amount);
    
    private void Awake()
    {
        OnHPChange += (f, f1) =>
        {
            Debug.Log("hpChange!!! " + HpChangeListeners.Count + " name " + gameObject.name);
            foreach (var hpChange in HpChangeListeners)
            {
                hpChange.UpdateHP(f,f1);
            }
        };
    }

    public void AddListener(IHPChangeListener hpChangeListeners)
    {
        if (!HpChangeListeners.Contains(hpChangeListeners))
        {
            HpChangeListeners.Add(hpChangeListeners);
        }
    }
    
    public void RemoveListener(IHPChangeListener hpChangeListeners)
    {
        if (HpChangeListeners.Contains(hpChangeListeners))
        {
            HpChangeListeners.Remove(hpChangeListeners);
        }
    }
}