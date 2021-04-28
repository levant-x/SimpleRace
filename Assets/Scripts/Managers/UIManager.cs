using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GamePage gamePage;
    public GameObject copStopPageObj;

    Rigidbody2D playerRigidbody;


    public void RedrawUI(Score score)
    {
        gamePage.RedrawUI(score);
    }

    public void StopGame(Rigidbody2D rb)
    {
        playerRigidbody = rb;
    }
    

    private void Update()
    {
        if (playerRigidbody == null || playerRigidbody.velocity.magnitude > 0)
            return;
        else if (copStopPageObj.activeInHierarchy) return;
        copStopPageObj.SetActive(true);
    }
}
