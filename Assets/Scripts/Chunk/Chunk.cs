using UnityEngine;

public class Chunk : MonoBehaviour
{
    public float lifespan = 10f;

    void Start()
    {
        Destroy(gameObject, lifespan);
    }
}
