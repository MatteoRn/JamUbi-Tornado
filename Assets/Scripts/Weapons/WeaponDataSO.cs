using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Weapon")]
public class WeaponDataSO : ScriptableObject
{
    [Header("General")]
    public string weaponName = "Gun";
    public Sprite weaponSprite;

    [Header("Stats de Tir")]
    public float fireRate = 0.2f;
    public int bulletsPerShot = 1;
    public float spread = 0f;
    public float recoilForce = 0f;

    [Header("Projectile")]
    public GameObject bulletPrefab;
    public BulletDataSO bulletData;

    [Header("FX")]
    public GameObject muzzleFlash;
    public AudioClip fireSound;
}
