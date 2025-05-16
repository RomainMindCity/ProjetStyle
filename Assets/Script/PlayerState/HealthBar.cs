using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IHealthBar
{
    [SerializeField] private Slider slider;

    public void Initialize(int maxHealth)
    {
        if (slider == null)
        {
            Debug.LogError("Slider not assigned in HealthBar.");
            return;
        }
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void UpdateHealth(int currentHealth)
    {
        if (slider != null)
        {
            slider.value = currentHealth;
        }
    }
}
