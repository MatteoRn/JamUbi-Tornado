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
    private bool isReloading = false;

    private int currentAmmo;
    private Vector2 direction;

    void Awake()
    {
        _input = new PlayerInputActions();
    }

    void OnEnable()
    {
        _input.Enable();
        _input.Combat.Fire.performed += ctx => TryShoot();
        _input.Combat.Reload.performed += ctx => StartReload();
    }

    void OnDisable()
    {
        _input.Disable();
    }

    void Start()
    {
        currentAmmo = currentWeapon.magazineSize;
    }

    void Update()
    {
        AimAtMouse();
    }

    void AimAtMouse()
    {
        Vector2 mousePos = _input.Movement.MousePosition.ReadValue<Vector2>();
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorld.z = 0f;

        direction = mouseWorld - weaponPivot.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weaponPivot.rotation = Quaternion.Euler(0, 0, angle);
    }

    // === TRY SHOOT ===
    void TryShoot()
    {
        if (isReloading) return;
        if (currentAmmo <= 0)
        {
            StartReload();
            return;
        }
        if (Time.time < nextFireTime) return;

        Shoot();
    }

    // === RELOAD ===
    public void StartReload()
    {
        if (isReloading) return;
        isReloading = true;
        Debug.Log("Reloading...");
        Invoke(nameof(FinishReload), currentWeapon.reloadTime);
    }

    void FinishReload()
    {
        isReloading = false;
        currentAmmo = currentWeapon.magazineSize;
        Debug.Log("Reload complete!");
    }

    void Shoot()
    {
        currentAmmo--;

        nextFireTime = Time.time + currentWeapon.fireRate;

        if (currentWeapon.muzzleFlash)
            Instantiate(currentWeapon.muzzleFlash, firePoint.position, firePoint.rotation);

        int count = currentWeapon.bulletsPerShot;
        float totalSpread = currentWeapon.spread;

        for (int i = 0; i < count; i++)
        {
            float t = (count == 1) ? 0f : (float)i / (count - 1);
            float angle = Mathf.Lerp(-totalSpread, totalSpread, t);

            Vector2 shootDir = Quaternion.Euler(0, 0, angle) * firePoint.right;

            GameObject go = Instantiate(currentWeapon.bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bullet = go.GetComponent<Bullet>();
            bullet.Init(currentWeapon.bulletData, shootDir);
        }
    }
}
