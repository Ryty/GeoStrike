using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameMode : MonoBehaviour
{
    public virtual IEnumerator StartGame()
    {
        return null;
    }
}
