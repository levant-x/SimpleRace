using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    static GameManager manager;
    static Vector3 startPosition;
    static int lines = GameManager.lines;
    static int initialized = 0;


    public static void SetGameManager(GameManager manager)
    {
        Road.manager = manager;
    }


    void Start()
    {
        if (gameObject.name != "road (2)") return;
        startPosition = transform.position;
    }

    void Update()
    {
        if (manager == null) return;
        var distToCameraY = Camera.main.transform.position.y -
            transform.position.y;
        
        if (distToCameraY <= manager.resetDistanceY)
            return;
        ResetThis();        
    }


    private void ResetThis()
    {
        transform.position = startPosition + transform.position +
            Vector3.up * manager.resetDistanceY; ;
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

        var x = manager.roadLinesCentersX[lineInd];
        CreateObj(x, GameManager.GetRandomObject());
        return true;
    }

    private void CreateObj(float positionX, GameObject obj)
    {
        var roadHeight = manager.segmentHeight - 2;
        var positionY = transform.position.y + roadHeight * 
            (0.5f - Random.Range(0, 1f));

        var objPosition = new Vector2(positionX, positionY);
        var newObj = Instantiate(obj, objPosition, Quaternion.identity);
        if (newObj.tag != "Car") newObj.transform.SetParent(transform);
    }
}
