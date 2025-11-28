using UnityEngine;

public class TornadoGenerator : MonoBehaviour
{
    public static TornadoGenerator Instance {  get; private set; }

    public float startLife = 5f;
    public float addLife = 2f;
    public int startDamage = 1;
    public int addDamage = 1;
    public float startSize = 0.3f;
    public float addSize = 0.05f;
    public float startMoveSpeed = 3f;
    public float addMoveSpeed = 0.4f;

    public float minSpawnRadius = 25f;
    public float maxSpawnRadius = 100f;

    public Tornado template;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        SpawnTornado();
    }

    private Vector3 RandomDirection()
    {
        return new Vector2(Mathf.Sign(Random.Range(-1, 1)),
            Mathf.Sign(Random.Range(-1, 1)));
    }



    public void SpawnTornado()
    {
        Tornado tornado = Instantiate(template);
        tornado.life = startLife;
        tornado.damage = startDamage;
        tornado.transform.localScale = Vector3.one * startSize;
        tornado.moveSpeed = startMoveSpeed;
        tornado.transform.position = CarController.Instance.transform.position + (RandomDirection() * Random.Range(minSpawnRadius, maxSpawnRadius));
        tornado.tornadoMode = (Tornado.TornadoMode)((int)Random.Range(0, 3));
        tornado.onDeath.AddListener(SpawnTornado);

        startLife += addLife;
        startDamage += addDamage;
        startSize += addSize;
        startMoveSpeed += addMoveSpeed;

    }
}
