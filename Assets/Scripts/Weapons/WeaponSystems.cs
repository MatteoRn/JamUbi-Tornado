using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSystems : MonoBehaviour
{
    public WeaponDataSO currentWeapon;
    public Transform firePoint;
    public Transform weaponPivot;
    public SpriteRenderer weaponSpriteRenderer;

    private PlayerInputActions _input;
    private float nextFireTime;

    private Vector2 direction;

    void Awake()
    {
        _input = new PlayerInputActions();
    }

    void OnEnable()
    {
        _input.Enable();
        _input.Combat.Fire.performed += ctx => TryShoot();
    }

    void OnDisable()
    {
        _input.Disable();
    }

    void Update() => AimAtMouse();

    void AimAtMouse()
    {
        Vector2 mousePos = _input.Movement.MousePosition.ReadValue<Vector2>();
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorld.z = 0f;

        direction = mouseWorld - weaponPivot.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        weaponPivot.rotation = Quaternion.Euler(0, 0, angle);
    }

    void TryShoot()
    {
        if (Time.time < nextFireTime) return;

        Shoot();
        nextFireTime = Time.time + currentWeapon.fireRate;
    }

    void Shoot()
    {
        if (currentWeapon.muzzleFlash)
            Instantiate(currentWeapon.muzzleFlash, firePoint.position, firePoint.rotation);

        for (int i = 0; i < currentWeapon.bulletsPerShot; i++)
        {
            GameObject go = Instantiate(currentWeapon.bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bullet = go.GetComponent<Bullet>();

            Vector2 shootDir = (firePoint.position - weaponPivot.position).normalized;
            float spreadAngle = Random.Range(-currentWeapon.spread, currentWeapon.spread);
            shootDir = Quaternion.Euler(0, 0, spreadAngle) * shootDir;

            bullet.Init(currentWeapon.bulletData, shootDir);

        }
    }
}
