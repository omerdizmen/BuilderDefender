using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;
    public event EventHandler OnDied;
    public bool isFullHealth => _healthAmount == _healthAmountMax;
    public bool isDead => _healthAmount == 0;
    public float getHealthAmountNormalized => (float)_healthAmount / _healthAmountMax;
    public int getHealthAmount => _healthAmount;
    public int getHealthAmountMax => _healthAmountMax;

    private int _healthAmount;
    [SerializeField] private int _healthAmountMax;

    private void Awake()
    {        
        _healthAmount = _healthAmountMax;
    }

    public void Damage(int damageAmount)
    {
        _healthAmount -= damageAmount;
        _healthAmount = Mathf.Clamp(_healthAmount, 0, _healthAmountMax);
        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (isDead)
        {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }
    public void SetHealthAmountMax(int maxHealth)
    {
        _healthAmountMax = maxHealth;
        _healthAmount = _healthAmountMax;        
    }

    public void Heal(int healAmount)
    {
        _healthAmount += healAmount;
        _healthAmount = Mathf.Clamp(_healthAmount, 0, _healthAmountMax);
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void HealFull()
    {
        _healthAmount = _healthAmountMax;
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

}
