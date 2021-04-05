using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager 
{
    public static GameManager manager;
    public static PrefabStorage prefabStorage;
    
    float spawnCenterY;
    int[] clearLines = new int[manager.lines];
    int availableLines = 0;
    bool wasLastZero = false;
        

    public void SpawnObjects(float spawnCenterY)
    {
        this.spawnCenterY = spawnCenterY;
        SetupClearLinesData();
        LoopThroughLines();
    }

    private void SetupClearLinesData()
    {
        availableLines = manager.lines;
        for (int i = 0; i < clearLines.Length; i++)
        {
            if (clearLines[i] == 0) continue;
            availableLines--;
            clearLines[i]--;
        }
    }

    private void LoopThroughLines()
    {
        int total = Random.Range(0, availableLines);
        if (wasLastZero && availableLines > 0) total = availableLines;
        wasLastZero = total == 0;

        int needed = total;
        for (int i = 0; i < manager.lines; i++)
        {
            if (needed == 0) break;
            else if (clearLines[i] > 0 || !SpawnedAtLine(i, needed, total))
                continue;
            needed--;
        }
    }

    private bool SpawnedAtLine(int lineInd, int needed, int total)
    {
        var limit = needed / (float)(manager.lines - lineInd);
        if (limit < 1 && Random.Range(0, 1f) > limit)
            return false;

        var x = manager.roadLinesCentersX[lineInd];
        var prefab = prefabStorage.GetRandomPrefab();
        var newObj = CreateObj(x, prefab);
        OnObjectCreated(newObj, lineInd);
        return true;
    }

    private GameObject CreateObj(float positionX, GameObject obj)
    {
        var roadHeight = manager.segmentHeight - 2;
        var positionY = spawnCenterY + roadHeight *
            (0.5f - Random.Range(0, 1f));

        var objPosition = new Vector2(positionX, positionY);
        return Object.Instantiate(obj, objPosition, Quaternion.identity);
    }

    private void OnObjectCreated(GameObject obj, int lineInd)
    {
        if (!obj.name.Contains("car")) return;
        clearLines[lineInd] = 4;
    }
}
