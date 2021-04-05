using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public static GameManager manager;

    static Vector3 startPosition;
       

    void Start()
    {
        if (gameObject.name != "road (2)") return;
        startPosition = transform.position;
    }

    void Update()
    {
        if (manager == null) return;
        var distToCamera = Camera.main.transform.position -
            transform.position;
        
        if (distToCamera.y <= manager.resetDistanceY)
            return;
        ResetThis(distToCamera);        
    }


    private void ResetThis(Vector3 distToCamera)
    {
        transform.position += manager.resetDistanceY *
            Vector3.up + startPosition;
        manager.SpawnObstacles(transform.position.y);
    }
}
