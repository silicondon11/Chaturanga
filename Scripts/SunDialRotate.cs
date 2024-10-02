using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class SunDialRotate : MonoBehaviour
{
    public GameObject sun;
    public GameObject cam;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        UnityEngine.Vector2 cam2D = new UnityEngine.Vector2(cam.transform.position.x, cam.transform.position.z);
        UnityEngine.Vector2 sun2D = new UnityEngine.Vector2(sun.transform.position.x, sun.transform.position.z);
        float rot = cam.transform.eulerAngles.y;

        UnityEngine.Vector2 direction = sun2D - cam2D;
        float angle = ((Mathf.Atan2(direction.y, direction.x) * (180 / Mathf.PI)) - 90) + rot;

        rectTransform.rotation = UnityEngine.Quaternion.Euler(0f, 0f, angle);
    }
}
