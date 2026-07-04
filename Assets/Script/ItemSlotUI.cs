using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image Image_Icon;
    [SerializeField] private TMP_Text Text_Quantity;
    [SerializeField] private Image Image_SelectedHighlight;
    [SerializeField] private Button Button_Slot;

    public Action<ItemSlotUI> OnSlotClicked;

    public ItemData CurrentItem { get; private set; }

    private void Awake()
    {
        if (Button_Slot != null)
        {
            Button_Slot.onClick.AddListener(HandleClick);
        }

        Clear();
    }

    public void SetItem(ItemData item, int quantity)
    {
        CurrentItem = item;

        if (Image_Icon != null)
        {
            Image_Icon.sprite = item.Icon;
            Image_Icon.enabled = true;
        }

        if (Text_Quantity != null)
        {
            Text_Quantity.text = quantity > 1 ? "x" + quantity.ToString() : "";
            Text_Quantity.enabled = true;
        }
    }

    public void Clear()
    {
        CurrentItem = null;

        if (Image_Icon != null)
        {
            Image_Icon.sprite = null;
            Image_Icon.enabled = false;
        }

        if (Text_Quantity != null)
        {
            Text_Quantity.text = "";
            Text_Quantity.enabled = false;
        }

        SetSelected(false);
    }

    public void SetSelected(bool isSelected)
    {
        if (Image_SelectedHighlight != null)
        {
            Image_SelectedHighlight.enabled = isSelected;
        }
    }

    private void HandleClick()
    {
        OnSlotClicked?.Invoke(this);
    }
}
