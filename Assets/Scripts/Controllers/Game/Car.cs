using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : ItemFrequency
{
    public float speed = 2f;
    public float deccelerationRate = 10f;

    bool toStop = false;


    
    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime * speed;
        if (!toStop) return;

        speed -= deccelerationRate / 100;
        if (speed < 0) speed = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        toStop = true;
    }
}
