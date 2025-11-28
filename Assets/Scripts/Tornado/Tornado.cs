using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tornado : MonoBehaviour
{
    public enum TornadoMode
    {
        None, Attraction, Repulsion
    }

    public float life = 100f;
    public int damage = 1;

    public float radius = 1.5f;
    public float attractionForce = 5f;
    public TornadoMode tornadoMode = TornadoMode.None;

    public float addSize = 0.5f;
    public float maxSize = 10f;
    public float timeBetweenChangingSize = 10f;
    private CircleCollider2D _CircleCollider;

    public float rotationSpeed = 180f; 
    public float baseRadius = 3f;     
    public float radiusVariation = 1f;
    public float variationSpeed = 1f; 

    private float angle = 0f;
    private float noiseOffset;

    public float moveSpeed = 3f;

    Vector3 startPosition;

    public UnityEvent onDeath = new UnityEvent();

    List<ITornadable> affectedElement = new List<ITornadable>();

    private void Awake()
    {
        _CircleCollider = GetComponent<CircleCollider2D>();
        _CircleCollider.radius = radius;
    }
    void Start()
    {
        noiseOffset = Random.Range(0f, 100f);
        startPosition = transform.position;
        StartCoroutine(UpdateSize());
    }

    void Update()
    {
        angle += rotationSpeed * Time.deltaTime;
        float rad = angle * Mathf.Deg2Rad;

        float noise = Mathf.PerlinNoise(noiseOffset, Time.time * variationSpeed);
        float radius = baseRadius + (noise - 0.5f) * 2f * radiusVariation;

        Vector3 tornadoMove = new Vector3(
            Mathf.Cos(rad) * radius,
            Mathf.Sin(rad) * radius,
            0f
        );

        transform.position = startPosition + tornadoMove;
        startPosition += ((CarController.Instance.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime);
    }

    private void UpdateLife(float damage)
    {
        life -= damage;

        if (life < 0)
        {
            onDeath.Invoke();
            Destroy(gameObject);
        }
    }

    IEnumerator UpdateSize()
    {
        while (transform.localScale.x < maxSize)
        {
            yield return new WaitForSeconds(timeBetweenChangingSize);
            Vector3 a = transform.localScale;
            transform.localScale = new Vector3(a.x + addSize, a.y + addSize, a.z + addSize);
        }
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet)
        {
            UpdateLife(bullet.damage);
            return;
        }

        ITornadable lTornadable = collision.gameObject.GetComponent<ITornadable>();
        if (lTornadable != null)
        {
            lTornadable.OnAffected(this);
            affectedElement.Add(lTornadable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ITornadable lTornadable = collision.gameObject.GetComponent<ITornadable>();
        if (lTornadable != null)
        {
            affectedElement.Remove(lTornadable);
        }
    }
}
