using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool bIsFinished = true;

    private float time;
    private float timeMax;
    private Image scaledImage;
    private bool bTimingShoot = false;


    private void Awake()
    {
        scaledImage = GameObject.FindGameObjectWithTag("ShootButtonBlocker").GetComponent<Image>();
    }

    void Update ()
    {
		if(!bIsFinished)
        {
            time -= Time.deltaTime;
            
            if(bTimingShoot)
                scaledImage.fillAmount = time / timeMax;

            if (time <= 0f)
            {
                bIsFinished = true;

                if (bTimingShoot)
                {
                    scaledImage.fillAmount = 0f;
                    bTimingShoot = false;
                }         
            }
        }
	}

    public void SetTimer(float t, bool timingShoot = false)
    {
        if (bIsFinished) //Nie ustawiamy nowego timera jeśli jest już ustawiony stary
        {
            time = t;
            timeMax = time;

            bIsFinished = false;

            if(timingShoot)
            {
                bTimingShoot = true;
                scaledImage.fillAmount = time / timeMax;
            }
        }
    }
}
