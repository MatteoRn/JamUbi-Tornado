using UnityEngine;

public class CarHealthSystems : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;
    public SliderBar HealthBar;

    [Header("Shield (Optional)")]
    public int maxShield = 0;
    public int currentShield = 0;
    float lastDamageTime;

    void Start()
    {
        HealthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        currentShield = maxShield;
    }


    public void TakeDamage(int damage)
    {
        lastDamageTime = Time.time;

        if (currentShield > 0)
        {
            int absorbed = Mathf.Min(currentShield, damage);
            currentShield -= absorbed;
            damage -= absorbed;
        }

        currentHealth -= damage;
        HealthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
                currentHealth = 0;
                Die();
         }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        HealthBar.SetHealth(currentHealth);
    }


    void Die()
    {
        Debug.Log("mort");
    }
}
