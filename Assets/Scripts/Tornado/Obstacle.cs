using UnityEngine;
using UnityEngine.Events;
using static Tornado;

public class Obstacle : MonoBehaviour, ITornadable
{
    public UnityEvent onFinishAspireByTornado = new UnityEvent();
    Rigidbody2D body;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }
    Tornado tornado;
    private void Update()
    {
        if (tornado != null && Vector3.Distance(tornado.transform.position, transform.position) <= tornado.radius / 3f)
        {
            onFinishAspireByTornado.Invoke();
        }
    }
    public void OnAffected(Tornado pTornado)
    {
        tornado = pTornado;
        switch (pTornado.tornadoMode)
        {
            case TornadoMode.Attraction:
                body.AddForce((pTornado.transform.position - transform.position).normalized * pTornado.attractionForce, ForceMode2D.Impulse);
                break;
            case TornadoMode.Repulsion:
                body.AddForce(-(pTornado.transform.position - transform.position).normalized * pTornado.attractionForce, ForceMode2D.Impulse);
                break;
            default: break;
        }
    }
}
