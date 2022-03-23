using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGen : MonoBehaviour
{
    [SerializeField] int size;
    [SerializeField] Transform plrPos;
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject WallPrefab;
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

    float RNG(float min, float max)
    {
        return Random.Range(min, max);
    }

    void CreateChunk(Vector3 Pos)
    {
        if (CheckChunk(Pos))
        {
            Chunks.Add(Instantiate(groundPrefab, Pos, Quaternion.identity, this.transform));
            Instantiate(WallPrefab, Chunks[Chunks.Count - 1].transform.position + new Vector3(RNG(-10, 10), 0, RNG(-10, 10)), Quaternion.identity, Chunks[Chunks.Count - 1].transform);
        }
    }


    float Round(float Value)
    {
        return Mathf.Round(Value/10)*10;
    }

    void GenChunks()
    {
        //Creates/Activates chunks
        for (int i = (-size * 10); i <= (size * 10); i += 10)
        {
            for (int ii = (-size * 10); ii <= (size * 10); ii += 10)
            {
                CreateChunk(new Vector3(Round(plrPos.position.x) + ii, 0, Round(plrPos.position.z) + i));
            }
        }

        //Deactivates outer surrounding chunks
        for (int i = (-size * 10 - 10); i <= (size * 10 + 10); i += 10)
        {
            deActiveChunk(new Vector3(Round(plrPos.position.x) + (-size * 10 - 10), 0, Round(plrPos.position.z) + i));
            deActiveChunk(new Vector3(Round(plrPos.position.x) - (-size * 10 - 10), 0, Round(plrPos.position.z) + i));
            deActiveChunk(new Vector3(Round(plrPos.position.x) + i, 0, Round(plrPos.position.z) + (size * 10 + 10)));
            deActiveChunk(new Vector3(Round(plrPos.position.x) + i, 0, Round(plrPos.position.z) - (size * 10 + 10)));
        }
        /*  20-20  20-10  20-0  20+10  20+20
         *  10-20  10-10  10-0  10+10  10+20
         *   0-20   0-10   0-0   0+10  0+20
         * -10-20 -10-10 -10-0 -10+10 -10+20
         * -20-20 -20-10 -20-0 -20+10 -20+20
         */
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
            GenChunks();
            oldPos = new Vector3(Round(plrPos.position.x), 0, Round(plrPos.position.z));
        }
    }
}
