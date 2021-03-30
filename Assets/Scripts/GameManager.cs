using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float segmentHeight = 6f;
    public float resetDistanceY = -10;

    public float[] roadLinesCentersX = new float[lines];
    public GameObject[] prefabs = new GameObject[lines];

    public static int lines = 4;
    public static Player player;
    public static GameManager instance;


    void Start()
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
