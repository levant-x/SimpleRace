using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float moveSpeed = .5f;

    
    // Update is called once per frame
    void Update()
    {
        var speed = GameManager.roadSpeed - moveSpeed;
        transform.position += Vector3.down * Time.deltaTime * speed;
    }
}
