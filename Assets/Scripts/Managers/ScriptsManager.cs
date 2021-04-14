using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptsManager : MonoBehaviour
{
    public float segmentHeight = 6f;
    public float resetDistanceY = -10;
    public int lines = 4;
    public float[] roadLinesCentersX;

    SpawnManager spawnManager;
    GameManager gameManager;
    PrefabStorage prefabStorage;
    UIManager uiManager;


    public void SpawnObstacles(float spawnCenterY)
    {
        spawnManager.SpawnObjects(spawnCenterY);
    }


    void Start()
    {
        Road.manager = this;
        SpawnManager.manager = this;

        spawnManager = new SpawnManager();
        uiManager = GetComponent<UIManager>();
        gameManager = GetComponent<GameManager>();
        prefabStorage = GetComponent<PrefabStorage>();

        var player = GameObject.Find("player").GetComponent<Player>();
        var score = new Score(); // load then

        uiManager.RedrawUI(score);

        gameManager.score = score;
        gameManager.uiManager = uiManager;
        spawnManager.prefabStorage = prefabStorage;
    }
}
