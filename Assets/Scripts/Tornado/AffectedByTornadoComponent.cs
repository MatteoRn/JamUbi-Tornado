using UnityEngine;
using UnityEngine.Events;
using static Tornado;

public class AffectedByTornadoComponent : MonoBehaviour, ITornadable
{
    public UnityEvent onFinishAspireByTornado = new UnityEvent();
    public UnityEvent<int> onInTornadoRadius = new UnityEvent<int>();
    public UnityEvent onAffected = new UnityEvent();
    Rigidbody2D body;

    bool isAlreadyAffected = false;
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
            onInTornadoRadius.Invoke(tornado.damage);
        }


        if (tornado != null && Vector3.Distance(tornado.transform.position, transform.position) <= tornado.radius / 3f)
        {
            onFinishAspireByTornado.Invoke();

            switch (tornado.tornadoMode)
            {
                case TornadoMode.Attraction:
                    body.AddForce(-(tornado.transform.position - transform.position).normalized * tornado.attractionForce, ForceMode2D.Force);
                    break;
                default: break;
            }
        }
    }
    public void ResetAffectation()
    {
        isAlreadyAffected = false;
    }
    public void OnAffected(Tornado pTornado)
    {
        if (isAlreadyAffected) return;
        isAlreadyAffected = true;
        onAffected.Invoke();
        tornado = pTornado;
        switch (pTornado.tornadoMode)
        {
            case TornadoMode.Attraction:
                body.AddForce((pTornado.transform.position - transform.position).normalized * pTornado.attractionForce * pTornado.transform.localScale.x, ForceMode2D.Force);
                break;
            case TornadoMode.Repulsion:
                body.AddForce(-(pTornado.transform.position - transform.position).normalized * pTornado.attractionForce * pTornado.transform.localScale.x, ForceMode2D.Force);
                break;
            default: break;
        }
    }
}
