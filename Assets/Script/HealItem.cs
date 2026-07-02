using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HealItem : MonoBehaviour
{
    [SerializeField] private int _healAmount = 30;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        UnitHealth targetHealth = other.GetComponentInParent<UnitHealth>();
        if (targetHealth == null || targetHealth.IsDead) return;

        targetHealth.Heal(_healAmount);
        Destroy(gameObject);
    }
}
