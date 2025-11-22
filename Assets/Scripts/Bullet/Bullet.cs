using UnityEngine;

public class Bullet : MonoBehaviour
{
    private BulletDataSO data;
    private Vector2 direction;
    private int pierceRemaining;

    public void Init(BulletDataSO bulletData, Vector2 dir)
    {
        data = bulletData;
        direction = dir.normalized;
        pierceRemaining = data.maxPierceCount;

        if (data.sprite != null) GetComponent<SpriteRenderer>().sprite = data.sprite;
        Destroy(gameObject, data.lifetime);
    }

    void Update()
    {
        transform.Translate(direction * data.speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D hit)
    {

        pierceRemaining--;
        if (pierceRemaining <= 0) Destroy(gameObject);
    }
}
