using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// скрипт для фиксатора проийденного расстояния (белая точка на маятнике)
public class IndicatorScript : MonoBehaviour
{
    public Vector2 indicatorPos, highestPoint, startingPoint;
    public float distanceTraveled;
    public Text distanceTraveledTxt;
   
    void Start()
    {
        startingPoint = GetComponent<Rigidbody2D>().position;;
    }

    void Update()
    {
        indicatorPos = GetComponent<Rigidbody2D>().position;
        if (highestPoint.x <= indicatorPos.x)
        {
            highestPoint = indicatorPos;
        }
        distanceTraveled = highestPoint.x - startingPoint.x;
        distanceTraveledTxt.text = distanceTraveled.ToString();
        GameObject.FindGameObjectWithTag("Projectile").GetComponent<ProjectileScript>().S = distanceTraveled;
    }
}
