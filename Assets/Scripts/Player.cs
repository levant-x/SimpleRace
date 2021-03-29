using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager manager;
    public float steeringSpeed = 0.3f;
    public float speedY = 0.2f;

    Vector3 acceleration;
    float pi_4 = Mathf.PI / 4;
    float roadNormalSpeed;
    float rotationRad;
    float dx;
    float dy;


    private void Start()
    {
        roadNormalSpeed = GameManager.roadSpeed;
    }

    void Update()
    {
        var inputSteer = -Input.GetAxis("Horizontal");
        var inputY = Input.GetAxis("Vertical");

        rotationRad = transform.rotation.eulerAngles.z /
            360 * Mathf.PI * 2f;
        Debug.Log(rotationRad);
        var stop = ToManualMoving();

        if (!stop || stop && inputY != 0)
            transform.Rotate(0, 0, inputSteer * steeringSpeed);
        DetectMovement(inputY);
        Move(stop);
    }


    bool ToManualMoving()
    {
        return rotationRad < 7 * pi_4 && rotationRad > pi_4;
    }

    void DetectMovement(float inputY)
    {
        dx = -Mathf.Sin(rotationRad);
        dy = Mathf.Cos(rotationRad);
        acceleration = new Vector3(dx, dy) * speedY * inputY;
    }

    void Move(bool stop)
    {
        transform.position += (new Vector3(GameManager.roadSpeed * dx, 0)
             + acceleration) * Time.deltaTime;
        if (stop) GameManager.roadSpeed = 0;
        else GameManager.roadSpeed = roadNormalSpeed * dy;
    }
}
