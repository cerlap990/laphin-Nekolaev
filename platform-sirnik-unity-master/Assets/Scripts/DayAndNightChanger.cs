using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNightChanger : MonoBehaviour
{
    [SerializeField] Light light;
    [SerializeField] float day_time;
    [SerializeField] float night_time;
    float TimeBeforeDayOrNight;
    bool isNight;

    void Start()
    {
        TimeBeforeDayOrNight = day_time;
        isNight = false;
    }

    void Update()
    {
        if (TimeBeforeDayOrNight > 0)
        {
            TimeBeforeDayOrNight -= Time.deltaTime;
        }

        if (TimeBeforeDayOrNight <= 0 && !isNight)
        {
            TimeBeforeDayOrNight = night_time;
            isNight = true;
            light.intensity = 0.1f;
        }

        else if (TimeBeforeDayOrNight <= 0 && !isNight)
        {
            TimeBeforeDayOrNight = day_time;
            isNight = false;
            light.intensity = 1f;
        }
    }
}
