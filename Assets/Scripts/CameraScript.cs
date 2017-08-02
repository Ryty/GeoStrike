using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraScript : MonoBehaviour
{
    public PlayerController player;
    public float followMargin;
    public float followSpeedModifier;

    private Vector2 pos2D;
    private Vector2 tar2D;

    private void Update()
    {
        pos2D = transform.position;
        tar2D = player.transform.position;
    }

    private void FixedUpdate()
    {
        if ((tar2D - pos2D).magnitude > followMargin)
        {
            float distModifier = (tar2D - pos2D).magnitude / followMargin;
            transform.position = Vector2.Lerp(transform.position, player.transform.position, Time.deltaTime * followSpeedModifier * distModifier);
        }
    }
}
