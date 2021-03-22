using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public GameManager manager;
    public static int lines = GameManager.lines;

    static Vector3 startPosition;
    static bool move = false;

    GameObject[] objects = new GameObject[lines];


    void Start()
    {
        if (gameObject.name != "road (2)") return;
        startPosition = transform.position;
        move = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!move) return;
        transform.position += Vector3.down * Time.deltaTime * 
            manager.roadSpeed;
        if (transform.position.y > manager.roadSegResetPositionY)
            return;
        ResetThis();        
    }

    private void ResetThis()
    {
        var positinoToResetTo = startPosition + transform.position -
            new Vector3(0, manager.roadSegResetPositionY);
        transform.position = positinoToResetTo;
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        var centers = manager.roadLinesCentersX;
        for (int i = 0; i < lines; i++) SpawnObjAtLine(i);
    }

    private void SpawnObjAtLine(int lineNum)
    {
        Destroy(objects[lineNum]);
        if (Random.Range(0, 3) == 0) return;
        var x = manager.roadLinesCentersX[lineNum];
        objects[lineNum] = CreateObj(x, manager.GetRandomObject());
    }

    private GameObject CreateObj(float positionX, GameObject obj)
    {
        var roadHeight = manager.roadSegHeight - 1;
        var positionY = transform.position.y + roadHeight * 
            (0.5f - Random.Range(0, 1f));
        var objPosition = new Vector2(positionX, positionY);
        var newObj = Instantiate(
            obj, 
            objPosition, 
            Quaternion.identity,
            transform);
        return newObj;
    }
}
