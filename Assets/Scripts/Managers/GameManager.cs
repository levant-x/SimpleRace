using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Score score;
    public UIManager uiManager;
    public Player player;

    public float effectsDeltaDist = 100;
    public float fuelConsumptionRate = 2f;
    public float distancePointsAward = 2f;

    float nextEffectDist;

       

    public void HandleCollision(GameObject gameObject)
    {
        if (gameObject.CompareTag("Obstacle")) StopGameWithAccident();
        else if (gameObject.CompareTag("Bonus")) TakeBonus(gameObject);
    }

    public void OnGameOverSubmit()
    {
        DataManager.UpdateRecords(new Record()
        {
            Name = uiManager.PlayerName.Trim(),
            Points = score.points,
            Distance = (float)Math.Round(player.transform.position.y / 100)
        });
        SceneManager.LoadScene("Menu");
    }

    private void Start()
    {
        nextEffectDist = effectsDeltaDist;
    }

    void Update()
    {
        if (player.transform.position.y < nextEffectDist ||
            player.gameover) return;

        nextEffectDist += effectsDeltaDist;
        score.DecreaseFuel(fuelConsumptionRate);
        score.IncreasePoints(distancePointsAward);

        if (score.fuel <= 0) StopGameWithRunOutOfFuel();
        uiManager.RedrawUI(score);
    }


    private void TakeBonus(GameObject gameObject)
    {
        var bonus = gameObject.GetComponent<Bonus>();
        score.points += bonus.points;
        if (bonus is Fuel fuel) score.fuel += fuel.chargeAmount;

        uiManager.RedrawUI(score);
        Destroy(gameObject);
    }


    private void StopGameWithAccident()
    {
        ThrowRotatePlayer();
        StopGame(true);
    }

    private void StopGameWithRunOutOfFuel()
    {
        StopPlayerInertially();
        StopGame();
    }


    private void ThrowRotatePlayer()
    {
        var playerRb = StopPlayerInertially();
        var contacts = new ContactPoint2D[1];
        playerRb.GetContacts(contacts);
        playerRb.AddForceAtPosition(playerRb.velocity/* * player.shmackForce*/, contacts[0].normal);
    }

    private Rigidbody2D StopPlayerInertially()
    {
        var playerRb = player.GetComponent<Rigidbody2D>();
        playerRb.AddForce(playerRb.velocity / player.shmackForce);
        playerRb.drag = 2;
        return playerRb;
    }

    private void StopGame(bool toCopStop = false)
    {
        player.gameover = true;
        if (!toCopStop) return;
        var playerRb = player.gameObject.GetComponent<Rigidbody2D>();
        uiManager.StopGame(playerRb);
    }
}
