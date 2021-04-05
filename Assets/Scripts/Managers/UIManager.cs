using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image tachometer;
    public Text pointsLabel;


    public void RedrawUI(Score score)
    {
        if (score.fuel > score.maxFuel) score.fuel = score.maxFuel;
        tachometer.fillAmount = score.fuel / (float)score.maxFuel;
        pointsLabel.text = score.points.ToString();
    }

    public void StopGame()
    {
        pointsLabel.text = "Foool";
    }


    void Update()
    {
        
    }
}
