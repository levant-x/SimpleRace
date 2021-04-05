using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public Score score;
    public GameObject player;
    public UIManager uiManager;

    float lastCheckpoint;


    void Update()
    {
        if (player.transform.position.y - lastCheckpoint < 10) return;
        lastCheckpoint = player.transform.position.y;
        score.points += 50;
        uiManager.RedrawUI(score);
    }
}
