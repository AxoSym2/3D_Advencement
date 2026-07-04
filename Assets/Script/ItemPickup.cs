using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemData ItemData_Item;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Inventory inventory = other.GetComponentInParent<Inventory>();
        if (inventory == null) return;

        bool wasAdded = inventory.AddItem(ItemData_Item);
        if (wasAdded)
        {
            Destroy(gameObject);
        }
    }
}
