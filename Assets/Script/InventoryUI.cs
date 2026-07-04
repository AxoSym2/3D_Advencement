using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject GameObject_InventoryPanel;
    [SerializeField] private Inventory Inventory_Target;
    [SerializeField] private List<ItemSlotUI> _slots;
    [SerializeField] private ItemUsePopupUI ItemUsePopupUI_Popup;

    private ItemSlotUI _selectedSlot;
    private ItemSlotUI _pendingUseSlot;
    private bool _isOpen;

    private void Awake()
    {
        foreach (ItemSlotUI slot in _slots)
        {
            slot.OnSlotClicked += HandleSlotClicked;
            slot.OnSlotRightClicked += HandleSlotRightClicked;
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

        if (_selectedSlot != null)
        {
            _selectedSlot.SetSelected(false);
        }

        if (_selectedSlot == clickedSlot)
        {
            _selectedSlot = null;
            return;
        }

        _selectedSlot = clickedSlot;
        _selectedSlot.SetSelected(true);
    }

    private void HandleSlotRightClicked(ItemSlotUI slot)
    {
        if (slot.CurrentItem == null) return;
        if (ItemUsePopupUI_Popup == null) return;

        _pendingUseSlot = slot;
        ItemUsePopupUI_Popup.Show(slot.CurrentItem, ConfirmUseItem);
    }

    private void ConfirmUseItem()
    {
        if (_pendingUseSlot == null || _pendingUseSlot.CurrentItem == null) return;

        Inventory_Target.UseItem(_pendingUseSlot.CurrentItem);
        _pendingUseSlot = null;
    }
}
