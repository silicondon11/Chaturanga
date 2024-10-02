using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeatherEvents : MonoBehaviour
{
    public TMP_Text weatherBox;

    [System.Serializable]
    public struct WeatherItem
    {
        public string weather;
        public int hours;
    }

    public List<WeatherItem> weatherItems = new List<WeatherItem>();

    private int hourCount = 0;
    private int weatherItemIndex = 0;

    void Start()
    {
        WeatherItem weather = weatherItems[0];
        weatherBox.text = weather.weather;
    }

    void Update()
    {
        WeatherItem weather = weatherItems[weatherItemIndex];
        weatherBox.text = weather.weather;
        int curHours = weather.hours;

        if (hourCount < curHours)
        {
            hourCount++;
        }
        else
        {
            hourCount = 0;
            weatherItemIndex++;

            if (weatherItemIndex >= weatherItems.Count)
            {
                weatherItemIndex = 0; //for now loop weather array if end reached.
            }
        }
    }
}
