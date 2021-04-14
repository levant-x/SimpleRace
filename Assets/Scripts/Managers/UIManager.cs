using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GamePage gamePage;



    public void RedrawUI(Score score)
    {
        gamePage.RedrawUI(score);
    }

    public void StopGame()
    {

    }
}
