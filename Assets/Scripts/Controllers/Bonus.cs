using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : ItemFrequency
{
    public int points;
    public bool toSpin;
    public float spinRateDeg;

    

    void Update()
    {
        if (!toSpin) return;
        transform.Rotate(0, spinRateDeg * Time.deltaTime, 0);
    }
}
