using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class UIManager : MonoBehaviour
{
    Rigidbody2D playerRigidbody;


    [SerializeField] GamePage gamePage;
    [SerializeField] GameObject copStopPageObj;
    [SerializeField] InputField playerNameInput;


    public string PlayerName { get; private set; }


    public void RedrawUI(Score score)
    {
        gamePage.RedrawUI(score);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void StopGame(Rigidbody2D rb)
    {
        playerRigidbody = rb;
    }

    public void OnPlayerNameEnter()
    {
        PlayerName = playerNameInput.text;
    }
    

    private void Update()
    {
        if (playerRigidbody == null || playerRigidbody.velocity.magnitude > .1f)
            return;
        else if (copStopPageObj.activeInHierarchy) return;
        copStopPageObj.SetActive(true);
    }
}
