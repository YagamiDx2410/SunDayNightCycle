using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SunOnInput : MonoBehaviour
{
    public Transform objectToRotate; // Reference to the object you want to rotate- the sun
    public Transform keyPosition; // Reference to the object you want to view

    public int radius = 19;
    // Take a base location
    public float latitude = 0f;
    public float longitude = 0f;
    // Take a current time
    public float month = DateTime.Now.Month;
    public float day = DateTime.Now.Day;
    public float hour = DateTime.Now.Hour;
    public float minute = DateTime.Now.Minute;
    

    private void Start()
    {
        PlaceObject();
    }
    private void Update()
    {
    

        //PlaceObject();
        
    }

    private int CalculateDayOfYear(float month, float day)
    {
        int[] daysInMonth = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        int dayOfYear = 0;
        for (int i = 0; i < month - 1; i++)
        {
            dayOfYear += daysInMonth[i];
        }

        dayOfYear += (int)day;

        return dayOfYear;
    }
    private (float,float,float) CalculateValue(float latitude, float longitude, float month, float day, float hour, float minute)
    {
        // Convert x and y coordinates to radians
        float xRad = Mathf.Deg2Rad * latitude;
        float yRad = Mathf.Deg2Rad * longitude;

        // Calculate the day of the year
        int dayOfYear = CalculateDayOfYear(month, day);

        // Calculate the decimal time
        float decimalTime = hour + (minute / 60f);

        // Calculate the equation of time
        float b = (dayOfYear - 81) * 360f / 365f;
        float equationOfTime = 9.87f * Mathf.Sin(2f * b * Mathf.Deg2Rad) - 7.53f * Mathf.Cos(b * Mathf.Deg2Rad) - 1.5f * Mathf.Sin(b * Mathf.Deg2Rad);

        // Calculate the solar noon time
        float solarNoon = 12f - equationOfTime / 60f;

        // Calculate the hour angle
        float hourAngle = (decimalTime - solarNoon) * 15f;
        
        return (xRad,yRad,hourAngle);
    }
    private float CalculateSunAzimuth(float latitude, float longitude, float month, float day, float hour, float minute)
    {
        (float xRad, float yRad, float hourAngle) = CalculateValue(latitude, longitude, month, day, hour, minute);


        // Calculate the object's azimuth
        float azimuth = Mathf.Atan2(Mathf.Sin(hourAngle * Mathf.Deg2Rad), Mathf.Cos(hourAngle * Mathf.Deg2Rad) * Mathf.Sin(yRad) - Mathf.Tan(0.41f * Mathf.Deg2Rad) * Mathf.Cos(yRad));

        // Convert azimuth to degrees and normalize to the range [0, 360)
        azimuth = Mathf.Rad2Deg * azimuth;
        if (azimuth < 0f)
            azimuth += 360f;

        return azimuth;
    }

    private float CalculateSunElevation(float latitude, float longitude, float month, float day, float hour, float minute)
    {
        (float xRad, float yRad, float hourAngle) = CalculateValue(latitude, longitude, month, day, hour, minute);

        // Calculate the object's elevation
        float elevation = Mathf.Asin(Mathf.Sin(xRad) * Mathf.Sin(yRad) + Mathf.Cos(xRad) * Mathf.Cos(yRad) * Mathf.Cos(hourAngle * Mathf.Deg2Rad));

        elevation = Mathf.Rad2Deg * elevation;

        return elevation;
    }
    private void PlaceObject()
    {
        float azimuth = CalculateSunAzimuth(latitude, longitude, month, day, hour, minute);
        float elevation = CalculateSunElevation(latitude, longitude, month, day, hour, minute);
        // Convert azimuth and elevation to radians
        float azimuthRad = Mathf.Deg2Rad * azimuth;
        float elevationRad = Mathf.Deg2Rad * elevation;

        // Calculate the position using spherical coordinates
        float x = radius * Mathf.Sin(azimuthRad) * Mathf.Cos(elevationRad);
        float y = radius * Mathf.Sin(elevationRad);
        float z = radius * Mathf.Cos(azimuthRad) * Mathf.Cos(elevationRad);

        // Set the position of the object
        objectToRotate.position = new Vector3(x, y, z);
        // Set new radius the object will rotate
        radius = (int)Vector3.Distance(objectToRotate.position, keyPosition.position); ;
    }
    
}

