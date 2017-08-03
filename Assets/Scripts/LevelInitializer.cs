using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [Header("Settings of the map")]
    public float mapScaleMin;
    public float mapScaleMax;

    [Header("Arrays with prefabs")]
    public GameObject[] arenasArray;

    private bool finishedArena;

    private void Awake()
    {
        StartCoroutine("InitLevel");
    }

    private IEnumerator InitLevel()
    {
        InitArena();

        yield return null;
    }

    private void InitArena()
    {
        GameObject arena = arenasArray[(int)Random.Range(0, arenasArray.Length - 1)];
        float scale = Random.Range(mapScaleMin, mapScaleMax);
        Debug.Log("Scale: " + scale + "!");

        arena = Instantiate(arena, new Vector3(0f, 0f, 10f), new Quaternion(0f, 0f, 0f, 0f));
        
        for(int i = 0; i < arena.transform.childCount; i++)
        {
            //Ustawmy prawidłowo ściany
            Transform trans = arena.transform.GetChild(i).transform;
            trans.position = new Vector3(trans.position.x * scale, trans.position.y * scale, trans.position.z);

            //Powiększmy prawidłowo sprajty
            if(trans.GetComponent<SpriteRenderer>() && trans.GetComponent<SpriteRenderer>().drawMode == SpriteDrawMode.Tiled)
            {
                trans.GetComponent<SpriteRenderer>().size *= scale;
            }

            //Powiększmy również collidery
            if(trans.GetComponent<BoxCollider2D>())
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
                        sr.size *= scale;
                    }
                }
            }
        }
    }
}
