using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [Header("Map dimensions")]
    public float mapScaleMin;
    public float mapScaleMax;
    [Header("Obstacle settings")]
    public LayerMask obstacleLayerMask;
    public int obstacleCountMin;
    public int obstacleCountMax;
    public float obstacleDistMin;
    [Header("Arrays with prefabs")]
    public GameObject[] arenaPrefabs;
    public GameObject[] obstaclePrefabs;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    private bool finishedArena = false;
    private float mapScale;
    
    private void Awake()
    {
        StartCoroutine("InitLevel");
    }

    private IEnumerator InitLevel()
    {
        InitArena();

        while (!finishedArena)
            yield return new WaitForEndOfFrame();

        InitObstacles();

        yield return null;
    }

    private void InitArena()
    {
        //Wybieramy arene i rozmiary
        GameObject arena = arenaPrefabs[(int)Random.Range(0, arenaPrefabs.Length - 1)];
        mapScale = Random.Range(mapScaleMin, mapScaleMax);
        Debug.Log("Scale: " + mapScale + "!");
        //Tworzymy arenę
        arena = Instantiate(arena, new Vector3(0f, 0f, 10f), new Quaternion(0f, 0f, 0f, 0f));
        //Dokańczamy arenę
        for (int i = 0; i < arena.transform.childCount; i++)
        {
            //Ustawmy prawidłowo ściany
            Transform trans = arena.transform.GetChild(i).transform;
            trans.position = new Vector3(trans.position.x * mapScale, trans.position.y * mapScale, trans.position.z);

            //Powiększmy prawidłowo sprajty
            if (trans.GetComponent<SpriteRenderer>() && trans.GetComponent<SpriteRenderer>().drawMode == SpriteDrawMode.Tiled)
            {
                trans.GetComponent<SpriteRenderer>().size *= mapScale;
            }

            //Powiększmy również collidery
            if (trans.GetComponent<BoxCollider2D>())
            {
                BoxCollider2D boxComp = trans.GetComponent<BoxCollider2D>();
                boxComp.offset = new Vector2(0f, 0f - ((trans.GetComponent<SpriteRenderer>().size.y / 2f) - 0.5f));
                boxComp.size = new Vector2(trans.GetComponent<SpriteRenderer>().size.x, 1f);
            }

            //Powiększmy też dzieci, jeśli są
            if (trans.childCount > 0)
            {
                Debug.Log("Jest dziecko! //LevelInitializer - 51");
                for (int j = 0; j < trans.childCount; j++)
                {
                    if (trans.GetChild(j).GetComponent<SpriteRenderer>().drawMode == SpriteDrawMode.Tiled)
                    {
                        SpriteRenderer sr = trans.GetChild(j).GetComponent<SpriteRenderer>();
                        sr.size *= mapScale;
                    }
                }
            }
        }
        //I gotowe
        finishedArena = true;
    }

    private void InitObstacles()
    {
        //Znajdujemy gdzie w ogóle jest arena
        Bounds arenaBounds = GameObject.FindGameObjectWithTag("ArenaFloor").GetComponent<SpriteRenderer>().bounds;
        //Losujemy ilość przeszkód
        int obstacleCount = (int)(Random.Range(obstacleCountMin, obstacleCountMax) * mapScale);
        //Tworzymy przeszkody
        for(int i = 0; i < obstacleCount; i++)
        {
            Vector3 pos;
            bool redo = false;
            //Tak długo znajdujemy nowe miejsce aż nie będzie ono zbyt blisko poprzednich
            do
            {
                redo = false;
                pos = new Vector3(MathLibrary.RandomPointInBounds(arenaBounds).x, MathLibrary.RandomPointInBounds(arenaBounds).y, 10f);

                Collider2D[] neighbours = Physics2D.OverlapCircleAll(pos, obstacleDistMin, obstacleLayerMask);
                redo = (neighbours.Length > 0 ? true : false);

            } while (redo);

            //Spawnujemy przeszkodę
            GameObject go = Instantiate(obstaclePrefabs[0], pos, new Quaternion(0f, 0f, 0f, 0f));
            if(go != null)
                spawnedObjects.Add(go);
            //Skalujemy przeszkodę
            if (go.GetComponent<SpriteRenderer>())
            {
                go.GetComponent<SpriteRenderer>().size *= Random.Range(1f, mapScale);
            }

            Debug.DrawLine(arenaBounds.center, pos, Color.white, 20f);
        }
    }
}
