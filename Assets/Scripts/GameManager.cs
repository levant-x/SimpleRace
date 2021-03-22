using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float roadSegResetPositionY;
    public float roadSegHeight = 6f;
    public float roadSpeed = 2f;

    public float[] roadLinesCentersX = new float[lines];
    public GameObject[] prefabs = new GameObject[lines];
    
    public static int lines = 4;


    void Update()
    {
        
    }

    public GameObject GetRandomObject()
    {
        var index = Random.Range(0, prefabs.Length);
        return prefabs[index];
    }
}
