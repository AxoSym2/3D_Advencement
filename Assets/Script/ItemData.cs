using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/New Item")]
public class ItemData : ScriptableObject
{
    [Header("Identity")]
    [SerializeField] private string _itemName = "New Item";
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _description = "";

    [Header("Effect")]
    [SerializeField] private int _healAmount = 10;

    [Header("Stacking")]
    [SerializeField] private bool _isStackable = true;
    [SerializeField] private int _maxStackCount = 5;

    public string ItemName
    {
        get { return _itemName; }
    }

    public Sprite Icon
    {
        get { return _icon; }
    }

    public string Description
    {
        get { return _description; }
    }

    public int HealAmount
    {
        get { return _healAmount; }
    }

    public bool IsStackable
    {
        get { return _isStackable; }
    }

    public int MaxStackCount
    {
        get { return _maxStackCount; }
    }

    public void Use(UnitHealth target)
    {
        if (target == null || target.IsDead) return;

        target.Heal(_healAmount);
    }
}
