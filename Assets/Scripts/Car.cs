using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float moveSpeed = .5f;

    
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime * moveSpeed;
    }
}
