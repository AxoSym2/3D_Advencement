using System;
using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;

    private Animator _animator;
    private string Anim_Hit = "Hit";
    private string Anim_Death = "Death";

    public int CurrentHealth { get; private set; }

    public int MaxHealth
    {
        get { return _maxHealth; }
    }

    public bool IsDead { get; private set; }

    public Action<int, int> OnHealthChanged;

    public Action OnDeath;

    private void Awake()
    {
        CurrentHealth = _maxHealth;
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(int amount)
    {
        if (IsDead) return;

        CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);
        OnHealthChanged?.Invoke(CurrentHealth, _maxHealth);

        if (_animator != null)
        {
            _animator.SetTrigger(Anim_Hit);
        }

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (IsDead) return;

        CurrentHealth = Mathf.Min(CurrentHealth + amount, _maxHealth);
        OnHealthChanged?.Invoke(CurrentHealth, _maxHealth);
    }

    private void Die()
    {
        IsDead = true;
        OnDeath?.Invoke();

        if (_animator != null)
        {
            _animator.SetTrigger(Anim_Death);
        }

        
    }
}
