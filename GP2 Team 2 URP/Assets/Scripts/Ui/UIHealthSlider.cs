using UnityEngine;
using UnityEngine.UI;

public class UIHealthSlider : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Image healthFill;
    [SerializeField] private Gradient gradient;

    public void SetInitialHealth(int maxHealth)
    {
        _healthBar.value = maxHealth;
        _healthBar.maxValue = maxHealth;

        healthFill.color = gradient.Evaluate(1f);
    }

    public void UpdateHealth(int currentHealth)
    {
        _healthBar.value = currentHealth;

        healthFill.color = gradient.Evaluate(_healthBar.normalizedValue);
    }
}
