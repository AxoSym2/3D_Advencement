using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HealItem : MonoBehaviour
{
    [SerializeField] private int _healAmount = 30;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Health targetHealth = other.GetComponentInParent<Health>();
        if (targetHealth == null || targetHealth.IsDead) return;

        targetHealth.Heal(_healAmount);
        Destroy(gameObject);
    }
}
