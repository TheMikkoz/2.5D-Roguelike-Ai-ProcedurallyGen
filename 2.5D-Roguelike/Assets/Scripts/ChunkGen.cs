using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGen : MonoBehaviour
{
    [SerializeField] Transform plrPos;
    [SerializeField] private GameObject groundPrefab;
    List<GameObject> Chunks = new List<GameObject>();
    private Vector3 oldPos = new Vector3(1,0,1);

    bool CheckChunk(Vector3 Pos)
    {
        foreach (GameObject Chunk in Chunks)
        {
            if (Pos == Chunk.transform.position)
            {
                Chunk.SetActive(true);
                return false;
            }
        }
        return true;
    }
    void deActiveChunk(Vector3 Pos)
    {
        foreach (GameObject Chunk in Chunks)
        {
            if (Pos == Chunk.transform.position)
            {
                Chunk.SetActive(false);
            }
        }
    }

    void CreateChunk(Vector3 Pos)
    {
        if (CheckChunk(Pos))
        {
            Chunks.Add(Instantiate(groundPrefab, Pos, Quaternion.identity));
        }
    }

    float Round(float Value)
    {
        return Mathf.Round(Value/10)*10;
    }

    void TreXTre()
    {
        // 0 0 0 0 0
        // 0 x x x 0
        // 0 x + x 0
        // 0 x x x 0
        // 0 0 0 0 0
        Vector3[] array = new Vector3[8];

        for (int i = 0; i < array.Length; i++)
        {
            CreateChunk(array[i]);
        }
        CreateChunk(new Vector3(Round(plrPos.position.x) + 10, 0, Round(plrPos.position.z) + 10));
        CreateChunk(new Vector3(Round(plrPos.position.x), 0, Round(plrPos.position.z) + 10));
        CreateChunk(new Vector3(Round(plrPos.position.x) - 10, 0, Round(plrPos.position.z) + 10));
        CreateChunk(new Vector3(Round(plrPos.position.x) - 10, 0, Round(plrPos.position.z)));
        CreateChunk(new Vector3(Round(plrPos.position.x) - 10, 0, Round(plrPos.position.z) - 10));
        CreateChunk(new Vector3(Round(plrPos.position.x), 0, Round(plrPos.position.z) - 10));
        CreateChunk(new Vector3(Round(plrPos.position.x) + 10, 0, Round(plrPos.position.z) - 10));
        CreateChunk(new Vector3(Round(plrPos.position.x) + 10, 0, Round(plrPos.position.z)));
        deActiveChunk(new Vector3(Round(plrPos.position.x) + 20, 0, Round(plrPos.position.z) + 20));
        deActiveChunk(new Vector3(Round(plrPos.position.x) + 10, 0, Round(plrPos.position.z) + 20));
        deActiveChunk(new Vector3(Round(plrPos.position.x) + 20, 0, Round(plrPos.position.z) + 10));
        deActiveChunk(new Vector3(Round(plrPos.position.x), 0, Round(plrPos.position.z) + 20));
        deActiveChunk(new Vector3(Round(plrPos.position.x) - 20, 0, Round(plrPos.position.z) + 20));
        deActiveChunk(new Vector3(Round(plrPos.position.x) - 20, 0, Round(plrPos.position.z) + 10));
        deActiveChunk(new Vector3(Round(plrPos.position.x) - 10, 0, Round(plrPos.position.z) + 20));
        deActiveChunk(new Vector3(Round(plrPos.position.x) - 20, 0, Round(plrPos.position.z)));
        deActiveChunk(new Vector3(Round(plrPos.position.x) - 20, 0, Round(plrPos.position.z) - 20));
        deActiveChunk(new Vector3(Round(plrPos.position.x) - 20, 0, Round(plrPos.position.z) - 10));
        deActiveChunk(new Vector3(Round(plrPos.position.x) - 10, 0, Round(plrPos.position.z) - 20));
        deActiveChunk(new Vector3(Round(plrPos.position.x), 0, Round(plrPos.position.z) - 20));
        deActiveChunk(new Vector3(Round(plrPos.position.x) + 20, 0, Round(plrPos.position.z) - 20));
        deActiveChunk(new Vector3(Round(plrPos.position.x) + 20, 0, Round(plrPos.position.z) - 10));
        deActiveChunk(new Vector3(Round(plrPos.position.x) + 10, 0, Round(plrPos.position.z) - 20));
        deActiveChunk(new Vector3(Round(plrPos.position.x) + 20, 0, Round(plrPos.position.z)));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (oldPos != new Vector3(Round(plrPos.position.x), 0, Round(plrPos.position.z)))
        {
            TreXTre();
            oldPos = new Vector3(Round(plrPos.position.x), 0, Round(plrPos.position.z));
        }
    }
}
