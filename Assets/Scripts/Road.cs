using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    static float[] roadLinesCentersX;
    static float segmentResetPositionY;
    static float segmentHeight;

    static Vector3 startPosition;
    static int lines = GameManager.lines;
    static int initialized = 0;


    public static void SetGameManager(GameManager manager)
    {
        roadLinesCentersX = manager.roadLinesCentersX;
        segmentResetPositionY = manager.segmentResetPositionY;
        segmentHeight = manager.segmentHeight;
    }
    

    void Start()
    {
        initialized++;
        if (gameObject.name != "road (2)") return;
        startPosition = transform.position;
    }

    void Update()
    {
        if (initialized < 3) return;
        transform.position += Vector3.down * Time.deltaTime * GameManager.roadSpeed;
        if (transform.position.y > segmentResetPositionY) return;
        ResetThis();        
    }


    private void ResetThis()
    {
        var positinoToResetTo = startPosition + transform.position -
            new Vector3(0, segmentResetPositionY);
        transform.position = positinoToResetTo;
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        int total = Random.Range(0, 5);
        int needed = total;
        for (int i = 0; i < lines; i++)
        {
            if (needed == 0) break;
            else if (!SpawnedAtLine(i, needed, total))
                continue;
            needed--;
        }
    }

    private bool SpawnedAtLine(int lineInd, int needed, int total)
    {
        var limit = needed / (float)(lines - lineInd);
        if (limit < 1 && Random.Range(0, 1f) > limit)
            return false;

        var x = roadLinesCentersX[lineInd];
        CreateObj(x, GameManager.GetRandomObject());
        return true;
    }

    private void CreateObj(float positionX, GameObject obj)
    {
        var roadHeight = segmentHeight - 2;
        var positionY = transform.position.y + roadHeight * 
            (0.5f - Random.Range(0, 1f));

        var objPosition = new Vector2(positionX, positionY);
        var newObj = Instantiate(obj, objPosition, Quaternion.identity);
        if (newObj.tag != "Car") newObj.transform.SetParent(transform);
    }
}
