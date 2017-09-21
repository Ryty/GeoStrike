using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool bIsFinished = true;

    private float time;

    void Update ()
    {
		if(!bIsFinished)
        {
            time -= Time.deltaTime;

            if (time <= 0f)
                bIsFinished = true;
        }
	}

    public void SetTimer(float t)
    {
        Debug.Log("SetTimer called.");
        if (bIsFinished) //Nie ustawiamy nowego timera jeśli jest już ustawiony stary
        {
            time = t;
            bIsFinished = false;
        }
    }
}
