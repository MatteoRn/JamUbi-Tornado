using UnityEngine;

public class Cardmanager : MonoBehaviour
{
    public CarHealthSystems healthSystem;
    public WeaponSystems weaponSystem;

    public void ApplyCard(CardSO card)
    {
        switch (card.type)
        {
            case UpgradeType.MaxHealth:
                healthSystem.maxHealth += (int)card.value;
                healthSystem.currentHealth += (int)card.value;
                break;

            case UpgradeType.CurrentHealth:
                healthSystem.currentHealth = Mathf.Min(
                    healthSystem.currentHealth + (int)card.value,
                    healthSystem.maxHealth
                );
                break;

            case UpgradeType.Shield:
                healthSystem.maxShield += (int)card.value;
                healthSystem.currentShield += (int)card.value;
                break;

            case UpgradeType.FireRate:
                weaponSystem.currentWeapon.fireRate -= card.value;
                weaponSystem.currentWeapon.fireRate = Mathf.Max(0.05f, weaponSystem.currentWeapon.fireRate);
                break;

            case UpgradeType.Spread:
                weaponSystem.currentWeapon.spread += card.value;
                break;

            case UpgradeType.MagazineSize:
                weaponSystem.currentWeapon.magazineSize += (int)card.value;
                break;

            case UpgradeType.ReloadSpeed:
                weaponSystem.currentWeapon.reloadTime -= card.value;
                weaponSystem.currentWeapon.reloadTime = Mathf.Max(0.2f, weaponSystem.currentWeapon.reloadTime);
                break;

            case UpgradeType.BulletsPerShot:
                weaponSystem.currentWeapon.bulletsPerShot += (int)card.value;
                break;

            case UpgradeType.BulletDamage:
                weaponSystem.currentWeapon.bulletData.damage += (int)card.value;
                break;

            case UpgradeType.BulletPierce:
                weaponSystem.currentWeapon.bulletData.maxPierceCount += (int)card.value;
                break;

        }

        Debug.Log($"Cart applied: {card.cardName}");
    }
}
