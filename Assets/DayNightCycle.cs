using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Time")]
    public int hour;
    public int minute;
    public float secondPerMinute = 1;
    public float dayMinutes;
    public float TimeMaxValue => (60 * 24);

    [Header("Day/Night Times (Hour)")]
    public int nightStarts = 18;
    public int nightEnds = 6;
    public float transitionTime = 1;
    public float DayState = 1;

    [Header("Scene References")]
    public float sunRotation;
    public Transform sun;

    private void OnValidate()
    {
        if(secondPerMinute <= 0)
        {
            secondPerMinute = 0.001f;
        }
    }
    
    void Update()
    {
        dayMinutes += Time.deltaTime / secondPerMinute;
        if (dayMinutes > TimeMaxValue) dayMinutes -= TimeMaxValue;

        hour = Mathf.FloorToInt(dayMinutes) / 60;

        minute = Mathf.FloorToInt(dayMinutes) - (hour * 60);

        CalculateDayState();
        sun.rotation = new Quaternion(sun.rotation.x, dayMinutes / TimeMaxValue, sun.rotation.z, sun.rotation.w);
        //Debug.Log((360 * dayMinutes / TimeMaxValue) - 180);
    }

    void CalculateDayState()
    {
        //Debug.Log((GetMinutesFromHour(nightStarts)) + " " + (GetMinutesFromHour(nightStarts + transitionTime)) + " " + dayMinutes);
        DayState = Mathf.InverseLerp(GetMinutesFromHour(nightStarts), GetMinutesFromHour(nightStarts + transitionTime), dayMinutes);
    }

    float GetMinutesFromHour(float hour)
    {
        return hour * 60;
    }
}
