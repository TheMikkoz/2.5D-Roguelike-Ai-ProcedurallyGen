using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGen : MonoBehaviour
{
    [SerializeField] int size;
    [SerializeField] Transform plrPos;
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject[] Prefab;
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
            Vector3[] instans = new Vector3[Prefab.Length];
            for (int i = 0; i < instans.Length; i++)
            {
                instans[i] = new Vector3(0, 0, 0);
            }
            Chunks.Add(Instantiate(groundPrefab, Pos, Quaternion.identity, this.transform));
            //Instantiate(Prefab[0], Chunks[Chunks.Count - 1].transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity, Chunks[Chunks.Count - 1].transform);

            for (int i = 0; i < Prefab.Length; i++)
            {

                Vector3 NewPos = new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(-4.5f, 4.5f));
                if (i == 0)
                {
                    GameObject a = Instantiate(Prefab[i], Chunks[Chunks.Count - 1].transform.position + NewPos, Quaternion.identity, Chunks[Chunks.Count - 1].transform);
                    instans[i] = a.transform.position;
                }
                else
                {
                    if (Vector3.Distance(instans[i], NewPos) > 2)
                    {
                        GameObject a = Instantiate(Prefab[i], Chunks[Chunks.Count - 1].transform.position + NewPos, Quaternion.identity, Chunks[Chunks.Count - 1].transform);
                        instans[i] = a.transform.position;
                    }
                }

            }
            /*
            for (int i = 0; i < Prefab.Length; i++)
            {
                Vector3 NewPos = new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(-4.5f, 4.5f));
                if (Chunks[Chunks.Count - 1].transform.childCount != 0)
                {
                    for (int ii = 0; ii < Chunks[Chunks.Count - 1].transform.childCount; ii++)
                    {
                        try
                        {
                            if (Vector3.Distance(instans[ii], NewPos) > 2)
                            {
                                Instantiate(Prefab[i], Chunks[Chunks.Count - 1].transform.position + NewPos, Quaternion.identity, Chunks[Chunks.Count - 1].transform);
                            }

                        }
                        catch (System.Exception e)
                        {
                            print(e);
                            throw;
                        }
                    }
                }
                else
                {
                    var a = Instantiate(Prefab[i], Chunks[Chunks.Count - 1].transform.position + NewPos, Quaternion.identity, Chunks[Chunks.Count - 1].transform);
                    instans[0] = a.transform.position;
                }
            }
            */
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
