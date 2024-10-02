using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Wind : MonoBehaviour
{
    private RectTransform rectTransform;
    public TMP_Text windspeedBox;
    public TMP_Text weather;

    private float targetAngle = 0f;
    private float currentAngle = 0f;
    private float targetSpeed = 0f;
    private float currentSpeed = 0f;
    private float angleIncrement = 2f; // Adjust based don how big of a change you want each frame
    private float speedIncrement = 0.25f; // Adjust based on how big of a change you want each frame
    private float lerpTime = 0.05f;

    public float nonPlayerAngle; //variable accessed from BirdController.cs


    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        float[] wind = WindControl();
        rectTransform.rotation = UnityEngine.Quaternion.Euler(0f, 0f, wind[0]);
        int windspeedNum = (int)wind[1];
        string windspeedText = windspeedNum.ToString();
        windspeedBox.text = windspeedText;
    }

    private float[] WindControl()
    {
        string weatherString = weather.text;

        switch (weatherString)
        {
            case "Windy":
                targetAngle += UnityEngine.Random.Range(-angleIncrement, angleIncrement);
                targetSpeed += UnityEngine.Random.Range(-speedIncrement, speedIncrement);
                targetAngle = Mathf.Clamp(targetAngle, 0f, 360f);
                targetSpeed = Mathf.Clamp(targetSpeed, 30f, 60f);
                break;

            case "Rainy":
                targetAngle += UnityEngine.Random.Range(-angleIncrement, angleIncrement);
                targetSpeed += UnityEngine.Random.Range(-speedIncrement, speedIncrement);
                targetAngle = Mathf.Clamp(targetAngle, 0f, 360f);
                targetSpeed = Mathf.Clamp(targetSpeed, 10f, 30f);
                break;

            case "Stormy":
                targetAngle += UnityEngine.Random.Range(-angleIncrement, angleIncrement);
                targetSpeed += UnityEngine.Random.Range(-speedIncrement, speedIncrement);
                targetAngle = Mathf.Clamp(targetAngle, 0f, 360f);
                targetSpeed = Mathf.Clamp(targetSpeed, 40f, 80f);
                break;

            case "Snowy":
                targetAngle += UnityEngine.Random.Range(-angleIncrement, angleIncrement);
                targetSpeed += UnityEngine.Random.Range(-speedIncrement, speedIncrement);
                targetAngle = Mathf.Clamp(targetAngle, -30f, 30f);
                targetSpeed = Mathf.Clamp(targetSpeed, 5f, 15f);
                break;

            case "Blizzard":
                targetAngle += UnityEngine.Random.Range(-angleIncrement, angleIncrement);
                targetSpeed += UnityEngine.Random.Range(-speedIncrement, speedIncrement);
                targetAngle = Mathf.Clamp(targetAngle, -10f, 10f);
                targetSpeed = Mathf.Clamp(targetSpeed, 30f, 50f);
                break;

            case "Sunny":
                targetAngle += UnityEngine.Random.Range(-angleIncrement, angleIncrement);
                targetSpeed += UnityEngine.Random.Range(-speedIncrement, speedIncrement);
                targetAngle = Mathf.Clamp(targetAngle, 0f, 360f);
                targetSpeed = Mathf.Clamp(targetSpeed, 0f, 10f);
                break;

            case "Overcast":
                targetAngle += UnityEngine.Random.Range(-angleIncrement, angleIncrement);
                targetSpeed += UnityEngine.Random.Range(-speedIncrement, speedIncrement);
                targetAngle = Mathf.Clamp(targetAngle, -30f, 30f);
                targetSpeed = Mathf.Clamp(targetSpeed, 10f, 25f);
                break;

            default:
                targetAngle = 0f;
                targetSpeed = 0f;
                break;
        }

        // Interpolating smoothly to the target values
        currentAngle = Mathf.Lerp(currentAngle, targetAngle, lerpTime);
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, lerpTime);

        nonPlayerAngle = currentAngle;

        float[] wind = { currentAngle, currentSpeed };
        return wind;
    }

}
