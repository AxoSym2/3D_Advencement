using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/New Item")]
public class ItemData : ScriptableObject
{
    [Header("Identity")]
    [SerializeField] private string _itemName = "New Item";
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _description = "";

    [Header("Effect")]
    [SerializeField] private int _healAmount = 20;

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

    public void Use(UnitHealth target)
    {
        if (target == null || target.IsDead) return;

        target.Heal(_healAmount);
    }
}
