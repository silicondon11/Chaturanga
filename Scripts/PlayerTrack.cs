using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrack : MonoBehaviour
{
    private UnityEngine.Vector3 offsetPos;
    public float rot = .01f;
    private float speed = 100f;

    void Start()
    {
        offsetPos = new UnityEngine.Vector3(0f, 250f, -20f);
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(UnityEngine.Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(UnityEngine.Vector3.right * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(UnityEngine.Vector3.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(UnityEngine.Vector3.back * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(transform.position, UnityEngine.Vector3.up, 20 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(transform.position, UnityEngine.Vector3.down, 20 * Time.deltaTime);
        }
    }
}
