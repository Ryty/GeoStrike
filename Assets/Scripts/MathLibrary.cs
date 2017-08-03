using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathLibrary
{
    public static Vector2 RandomPointInBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector2(x, y);
    }
}
