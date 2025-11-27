using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    public enum TornadoMode
    {
        None, Attraction, Repulsion
    }

    public float radius = 1.5f;
    public float attractionForce = 5f;
    public TornadoMode tornadoMode = TornadoMode.None;
    private CircleCollider2D _CircleCollider;

    List<ITornadable> affectedElement = new List<ITornadable>();

    private void Awake()
    {
        _CircleCollider = GetComponent<CircleCollider2D>();
        _CircleCollider.radius = radius;
    }
    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
    private void OnTriggerStay2D(Collider2D collision)
    {/*
        if (tornadoMode == TornadoMode.None) return;
        GameObject lObj = collision.gameObject;
        print(Vector3.Distance(lObj.transform.position, transform.position));
        ITornadable lTornadable = lObj.GetComponent<ITornadable>();
        if (lTornadable != null)
        {
        }*/
    }
}
