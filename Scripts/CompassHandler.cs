using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class CompassHandler : MonoBehaviour
{
    public RawImage compass;
    // public Transform player;
    public float numberOfPixelsNorthToNorth = 30f;
    public GameObject player;
    float rationAngleToPixel;


    void Start()
    {
        rationAngleToPixel = numberOfPixelsNorthToNorth / 360f;
    }

    
    void Update()
    {
        
        // compass.uvRect = new Rect(player.localEulerAngles.y / 360f, 0f, 1f, 1f);
    }
}
