using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;
    public float segmentHeight = 6f;
    public float resetDistanceY = -10;
    public int lines = 4;
    public float[] roadLinesCentersX;

    SpawnManager spawnManager;
    CollisionManager collisionManager;
    ProgressManager progressManager;


    public void SpawnObstacles(float spawnCenterY)
    {
        spawnManager.SpawnObjects(spawnCenterY);
    }


    void Start()
    {        
        Road.manager = this;
        SpawnManager.manager = this;
        SpawnManager.prefabStorage = GetComponent<PrefabStorage>();
        progressManager = GetComponent<ProgressManager>();

        spawnManager = new SpawnManager();
        collisionManager = new CollisionManager();

        var score = new Score(); // load then
        
        progressManager.score = collisionManager.score = score;
        collisionManager.uiManager = uiManager;

        var player = GameObject.Find("player").GetComponent<Player>();
        player.collisionManager = collisionManager;
        collisionManager.player = player;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
