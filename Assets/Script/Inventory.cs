using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryEntry
{
    public ItemData Item;
    public int Quantity;
}

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _maxSlotCount = 36;

    private List<InventoryEntry> _entries = new List<InventoryEntry>();
    private UnitHealth _health;

    public IReadOnlyList<InventoryEntry> Entries
    {
        get { return _entries; }
    }

    public Action OnInventoryChanged;

    private void Awake()
    {
        _health = GetComponent<UnitHealth>();
    }

    public bool AddItem(ItemData item)
    {
        if (item.IsStackable)
        {
            foreach (InventoryEntry entry in _entries)
            {
                if (entry.Item == item && entry.Quantity < item.MaxStackCount)
                {
                    entry.Quantity++;
                    OnInventoryChanged?.Invoke();
                    return true;
                }
            }
        }

        if (_entries.Count >= _maxSlotCount) return false;

        InventoryEntry newEntry = new InventoryEntry();
        newEntry.Item = item;
        newEntry.Quantity = 1;
        _entries.Add(newEntry);

        OnInventoryChanged?.Invoke();
        return true;
    }

    public void UseItem(ItemData item)
    {
        InventoryEntry entry = FindEntry(item);
        if (entry == null) return;

        item.Use(_health);

        entry.Quantity--;
        if (entry.Quantity <= 0)
        {
            _entries.Remove(entry);
        }

        OnInventoryChanged?.Invoke();
    }

    private InventoryEntry FindEntry(ItemData item)
    {
        foreach (InventoryEntry entry in _entries)
        {
            if (entry.Item == item)
            {
                return entry;
            }
        }
        return null;
    }
}
