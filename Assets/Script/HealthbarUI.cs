using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Health Health_Target;
    [SerializeField] private Slider Slider_HealthBar;

    private void OnEnable()
    {
        if (Health_Target == null) return;

        Health_Target.OnHealthChanged += UpdateHealthBar;
    }

    private void Start()
    {
        { 
            if (Health_Target == null) return;

            UpdateHealthBar(Health_Target.CurrentHealth, Health_Target.MaxHealth);
        }
    }

    private void OnDisable()
    {
        if (Health_Target == null) return;

        Health_Target.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(int current, int max)
    {
        if (Slider_HealthBar == null) return;

        Slider_HealthBar.maxValue = max;
        Slider_HealthBar.value = current;
    }
}
