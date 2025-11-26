using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    public Slider slider;

    public void SetHealth(int Health)
    {
        slider.value = Health;
    }

    public void SetMaxHealth(int MaxHealth)
    {
        slider.maxValue = MaxHealth;
        slider.value = MaxHealth;
    }
}
