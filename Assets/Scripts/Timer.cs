using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool bIsFinished = true;
    public float time { get; private set; }

    private float timeMax;
    private Image scaledImage;
    private bool bTimingShoot = false;
    private VoidDelegate func = null;


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

                if (func != null) //jeśli mamy funkcję do użycia to ją zresetujmy i ustawmy nową
                {
                    func();
                    func = null; 
                }             

                if (bTimingShoot)
                {
                    scaledImage.fillAmount = 0f;
                    bTimingShoot = false;
                }         
            }
        }
	}

    public void SetTimer(float t, bool timingShoot = false, VoidDelegate funcToCall = null, bool forceNewTime = false)
    {
        if (bIsFinished || forceNewTime) //Nie ustawiamy nowego timera jeśli jest już ustawiony stary. Chyba, że forsujemy nowy czas
        {
            time = t;
            timeMax = time;

            bIsFinished = false;

            if(timingShoot)
            {
                bTimingShoot = true;
                scaledImage.fillAmount = time / timeMax;
            }

            if (funcToCall != null)
                func = funcToCall;
        }
    }

    public delegate void VoidDelegate();
}
