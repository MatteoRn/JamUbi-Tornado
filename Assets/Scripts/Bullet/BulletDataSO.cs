using UnityEngine;

[CreateAssetMenu(fileName = "NewBullet", menuName = "Weapons/Bullet")]
public class BulletDataSO : ScriptableObject
{
    [Header("Stats")]
    public float speed = 20f;
    public float lifetime = 2f;
    public int damage = 1;
    public int maxPierceCount = 1;

    [Header("Visual & FX")]
    public GameObject impactEffect;
    public Sprite sprite;
}
