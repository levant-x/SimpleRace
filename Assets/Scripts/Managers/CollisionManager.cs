using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager 
{
    public Score score;
    public UIManager uiManager;
    public Player player;


    public void HandleCollision(Collider2D collision)
    {
        var gameObject = collision.gameObject;
        if (gameObject.tag == "Obstacle") DamagePlayer(gameObject);
        else if (gameObject.tag == "Bonus") TakeBonus(gameObject);
    }


    private void TakeBonus(GameObject gameObject)
    {
        var bonus = gameObject.GetComponent<Bonus>();
        score.points += bonus.points;
        if (bonus is Fuel fuel) score.fuel += fuel.chargeAmount;

        uiManager.RedrawUI(score);
        Object.Destroy(gameObject);
    }

    private void DamagePlayer(GameObject gameObject)
    {
        player.lost = true;
        var playerRb = player.GetComponent<Rigidbody2D>();
        playerRb.drag = 3;
        playerRb.AddForce(playerRb.velocity * 10);
        uiManager.StopGame();
    }
}
