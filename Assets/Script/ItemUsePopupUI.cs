using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUsePopupUI : MonoBehaviour
{
    [SerializeField] private GameObject GameObject_PopupPanel;
    [SerializeField] private TMP_Text Text_ItemName;
    [SerializeField] private TMP_Text Text_ItemDescription;
    [SerializeField] private Button Button_Confirm;
    [SerializeField] private Button Button_Cancel;

    private Action _onConfirm;

    private void Awake()
    {
        if (Button_Confirm != null)
        {
            Button_Confirm.onClick.AddListener(HandleConfirm);
        }

        if (Button_Cancel != null)
        {
            Button_Cancel.onClick.AddListener(HandleCancel);
        }

        Hide();
    }

    public void Show(ItemData item, Action onConfirm)
    {
        if (item == null) return;

        _onConfirm = onConfirm;

        if (Text_ItemName != null)
        {
            Text_ItemName.text = item.ItemName;
        }

        if (Text_ItemDescription != null)
        {
            Text_ItemDescription.text = item.Description;
        }

        if (GameObject_PopupPanel != null)
        {
            GameObject_PopupPanel.SetActive(true);
        }
    }

    public void Hide()
    {
        if (GameObject_PopupPanel != null)
        {
            GameObject_PopupPanel.SetActive(false);
        }

        _onConfirm = null;
    }

    private void HandleConfirm()
    {
        _onConfirm?.Invoke();
        Hide();
    }

    private void HandleCancel()
    {
        Hide();
    }
}
