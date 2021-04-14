using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (gameObject.tag == "Obstacle") StopGameWithAccident();
        else if (gameObject.tag == "Bonus") TakeBonus(gameObject);
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
        StopGame();
    }

    private void StopGameWithRunOutOfFuel()
    {
        StopPlayer();
        StopGame();
    }



    private void ThrowRotatePlayer()
    {
        var playerRb = StopPlayer();
        var contacts = new ContactPoint2D[1];
        playerRb.GetContacts(contacts);
        playerRb.AddForceAtPosition(playerRb.velocity/* * player.shmackForce*/, contacts[0].normal);
    }

    private Rigidbody2D StopPlayer()
    {
        var playerRb = player.GetComponent<Rigidbody2D>();
        playerRb.AddForce(playerRb.velocity / player.shmackForce);
        playerRb.drag = 2;
        return playerRb;
    }

    private void StopGame()
    {
        player.gameover = true;
        uiManager.StopGame();
    }
}
