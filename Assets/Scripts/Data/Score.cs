using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score 
{
    public int points;
    public int fuel = 10;
    public int maxFuel = 100;
    public float distance;

    float fuelLastChange;
    float pointsLastChange;



    public void DecreaseFuel(float delta)
    {
        fuelLastChange += delta;
        if (fuelLastChange < 1) return;

        var deltaInt = (int)fuelLastChange;
        fuel -= deltaInt;
        fuelLastChange -= deltaInt;
    }

    public void IncreasePoints(float delta)
    {
        pointsLastChange += delta;
        if (pointsLastChange < 1) return;

        var deltaInt = (int)pointsLastChange;
        points += deltaInt;
        pointsLastChange -= deltaInt;
    }
}
