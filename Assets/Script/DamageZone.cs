using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageZone : MonoBehaviour
{
    [SerializeField] private bool _isHealZone = false;
    [SerializeField] private int _tickAmount = 10;
    [SerializeField] private float _tickInterval = 1f;

    private Dictionary<Collider, float> _nextTickTime = new Dictionary<Collider, float>();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        _nextTickTime[other] = Time.time;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_nextTickTime.ContainsKey(other)) return;
        if (Time.time < _nextTickTime[other]) return;

        ApplyTickEffect(other);
        _nextTickTime[other] = Time.time + _tickInterval;
    }

    private void OnTriggerExit(Collider other)
    {
        if (_nextTickTime.ContainsKey(other))
        {
            _nextTickTime.Remove(other);
        }
    }

    private void ApplyTickEffect(Collider target)
    {
        UnitHealth targetHealth = target.GetComponentInParent<UnitHealth>();
        if (targetHealth == null || targetHealth.IsDead) return;

        if (_isHealZone)
        {
            targetHealth.Heal(_tickAmount);
        }
        else
        {
            targetHealth.TakeDamage(_tickAmount);
        }
    }
}
