using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameMode))]
public class LevelInitializer : MonoBehaviour
{
    [Header("Settings for game initialization")]
    public float chunkSize;
    [Header("Arrays with prefabs")]
    public GameObject[] chunkPrefabsUL;
    public GameObject[] chunkPrefabsUC;
    public GameObject[] chunkPrefabsUR;
    public GameObject[] chunkPrefabsCL;
    public GameObject[] chunkPrefabsCC;
    public GameObject[] chunkPrefabsCR;
    public GameObject[] chunkPrefabsLL;
    public GameObject[] chunkPrefabsLC;
    public GameObject[] chunkPrefabsLR;

    private int chunkAmount = 9;
    private bool finishedArena = false;
    
    private void Awake()
    {
        StartCoroutine("InitLevel");
    }

    private IEnumerator InitLevel()
    {
        //Zacznimy od zbudowania areny
        InitArena();

        //Dopóki arena się buduje, upewnijmy się że nic dalej nie zajdzie
        while (!finishedArena)
            yield return new WaitForEndOfFrame();

        GetComponent<GameMode>().StartCoroutine("StartGame");

        yield return null;
    }

    private void InitArena()
    {
        Vector3 pos = new Vector3();
        GameObject go = new GameObject();
        //Ruszamy od lewego górnego rogu (czyli 1 to UL, 2 to UC itp)
        for(int i = 1; i <= chunkAmount; i++)
        {
            switch(i)
            {
                case 1:
                    pos = new Vector3(-chunkSize, chunkSize, 11);
                    go = RandomChunk(chunkPrefabsUL);
                    break;
                case 2:
                    pos = new Vector3(0, chunkSize, 11);
                    go = RandomChunk(chunkPrefabsUC);
                    break;
                case 3:
                    pos = new Vector3(chunkSize, chunkSize, 11);
                    go = RandomChunk(chunkPrefabsUR);
                    break;
                case 4:
                    pos = new Vector3(-chunkSize, 0, 11);
                    go = RandomChunk(chunkPrefabsCL);
                    break;
                case 5:
                    pos = new Vector3(0, 0, 11);
                    go = RandomChunk(chunkPrefabsCC);
                    break;
                case 6:
                    pos = new Vector3(chunkSize, 0, 11);
                    go = RandomChunk(chunkPrefabsCR);
                    break;
                case 7:
                    pos = new Vector3(-chunkSize, -chunkSize, 11);
                    go = RandomChunk(chunkPrefabsLL);
                    break;
                case 8:
                    pos = new Vector3(0, -chunkSize, 11);
                    go = RandomChunk(chunkPrefabsLC);
                    break;
                case 9:
                    pos = new Vector3(chunkSize, -chunkSize, 11);
                    go = RandomChunk(chunkPrefabsLR);
                    break;
                default:
                    Debug.LogError("Error while spawning chunks. Switch argument went out of expected bounds! i = " + i);
                    break;
            }

            Instantiate(go, pos, new Quaternion());
        }


        finishedArena = true;
    }

    private GameObject RandomChunk(GameObject[] array)
    {
        return array[Random.Range(0, array.Length)];
    }
}
