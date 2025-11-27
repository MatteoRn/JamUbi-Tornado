using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public Transform player;
    public GameObject[] terrainPrefabs;

    public int chunkSize = 16;
    public int viewDistance = 1;
    public float chunkLifespan = 5f;

    private Dictionary<Vector2Int, GameObject> chunks = new Dictionary<Vector2Int, GameObject>();
    private Dictionary<Vector2Int, float> chunkTimers = new Dictionary<Vector2Int, float>();

    void Update()
    {
        Vector2Int playerChunk = GetPlayerChunk();

        for (int x = -viewDistance; x <= viewDistance; x++)
        {
            for (int y = -viewDistance; y <= viewDistance; y++)
            {
                Vector2Int c = new Vector2Int(playerChunk.x + x, playerChunk.y + y);
                GenerateChunk(c);
            }
        }

        CleanupChunks(playerChunk);
    }

    void GenerateChunk(Vector2Int coord)
    {
        if (chunks.ContainsKey(coord))
        {
            chunkTimers[coord] = Time.time + chunkLifespan;
            return;
        }

        Vector3 pos = new Vector3(coord.x * chunkSize, coord.y * chunkSize, 5);
        GameObject prefab = terrainPrefabs[Random.Range(0, terrainPrefabs.Length)];
        GameObject chunk = Instantiate(prefab, pos, Quaternion.identity);

        chunk.name = $"Chunk_{coord.x}_{coord.y}";

        chunks.Add(coord, chunk);
        chunkTimers.Add(coord, Time.time + chunkLifespan);
    }

    void CleanupChunks(Vector2Int playerChunk)
    {
        List<Vector2Int> toRemove = new List<Vector2Int>();

        foreach (var kv in chunks)
        {
            Vector2Int coord = kv.Key;
            GameObject chunk = kv.Value;

            bool isOutsideView =
                Mathf.Abs(coord.x - playerChunk.x) > viewDistance ||
                Mathf.Abs(coord.y - playerChunk.y) > viewDistance;

            bool isExpired = Time.time >= chunkTimers[coord];

            if (isOutsideView && isExpired)
                toRemove.Add(coord);
        }

        foreach (var coord in toRemove)
        {
            Destroy(chunks[coord]);
            chunks.Remove(coord);
            chunkTimers.Remove(coord);
        }
    }

    Vector2Int GetPlayerChunk()
    {
        return new Vector2Int(
            Mathf.FloorToInt(player.position.x / chunkSize),
            Mathf.FloorToInt(player.position.y / chunkSize)
        );
    }
}
