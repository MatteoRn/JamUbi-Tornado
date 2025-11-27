using UnityEngine;
using UnityEngine.Events;
using static Tornado;

public class AffectedByTornadoComponent : MonoBehaviour, ITornadable
{
    public UnityEvent onFinishAspireByTornado = new UnityEvent();
    public UnityEvent onInTornadoRadius = new UnityEvent();
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
        if (tornado != null && Vector3.Distance(tornado.transform.position, transform.position) <= tornado.radius)
        {
            onInTornadoRadius.Invoke();
        }


        if (tornado != null && Vector3.Distance(tornado.transform.position, transform.position) <= tornado.radius / 3f)
        {
            onFinishAspireByTornado.Invoke();

            switch (tornado.tornadoMode)
            {
                case TornadoMode.Attraction:
                    body.AddForce(-(tornado.transform.position - transform.position).normalized * tornado.attractionForce, ForceMode2D.Impulse);
                    break;
                default: break;
            }
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
