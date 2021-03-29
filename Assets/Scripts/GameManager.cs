using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float segmentHeight = 6f;
    public float segmentResetPositionY;

    public static float roadSpeed = 2f;
    public static int lines = 4;

    public float[] roadLinesCentersX = new float[lines];
    public GameObject[] prefabs = new GameObject[lines];

    static GameManager instance;


    void Update()
    {
        instance = this;
        Road.SetGameManager(this);
    }

    public static GameObject GetRandomObject()
    {
        var index = Random.Range(0, instance.prefabs.Length);
        return instance.prefabs[index];
    }
}
