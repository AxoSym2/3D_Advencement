using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject GameObject_InventoryPanel;
    [SerializeField] private Inventory Inventory_Target;
    [SerializeField] private List<ItemSlotUI> _slots;

    private ItemSlotUI _selectedSlot;
    private bool _isOpen;

    private void Awake()
    {
        foreach (ItemSlotUI slot in _slots)
        {
            slot.OnSlotClicked += HandleSlotClicked;
        }
    }

    private void OnEnable()
    {
        if (Inventory_Target != null)
        {
            Inventory_Target.OnInventoryChanged += RefreshSlots;
        }
    }

    private void OnDisable()
    {
        if (Inventory_Target != null)
        {
            Inventory_Target.OnInventoryChanged -= RefreshSlots;
        }
    }

    private void Start()
    {
        RefreshSlots();
        SetOpen(false);
    }

    private void Update()
    {
        if (InputManager.Instance.InventoryTogglePressed)
        {
            SetOpen(!_isOpen);
        }
    }

    private void SetOpen(bool isOpen)
    {
        _isOpen = isOpen;

        if (GameObject_InventoryPanel != null)
        {
            GameObject_InventoryPanel.SetActive(isOpen);
        }
    }

    private void RefreshSlots()
    {
        IReadOnlyList<InventoryEntry> entries = Inventory_Target.Entries;

        for (int i = 0; i < _slots.Count; i++)
        {
            if (i < entries.Count)
            {
                _slots[i].SetItem(entries[i].Item, entries[i].Quantity);
            }
            else
            {
                _slots[i].Clear();
            }
        }

        if (_selectedSlot != null && _selectedSlot.CurrentItem == null)
        {
            _selectedSlot = null;
        }
    }

    private void HandleSlotClicked(ItemSlotUI clickedSlot)
    {
        if (clickedSlot.CurrentItem == null) return;

        if (_selectedSlot == clickedSlot)
        {
            UseSelectedItem();
            return;
        }

        if (_selectedSlot != null)
        {
            _selectedSlot.SetSelected(false);
        }

        _selectedSlot = clickedSlot;
        _selectedSlot.SetSelected(true);
    }

    private void UseSelectedItem()
    {
        if (_selectedSlot == null || _selectedSlot.CurrentItem == null) return;

        Inventory_Target.UseItem(_selectedSlot.CurrentItem);
        _selectedSlot = null;
    }
}
