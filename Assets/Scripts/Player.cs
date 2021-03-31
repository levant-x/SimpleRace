using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float steeringSpeed = 0.3f;
    public float gasBrakeSpeed = 1f;
    public float speedX = 3f;
    public float speedY = 0.2f;

    Vector3 acceleration;
    Camera cam;
    float offsetToCamera = 3f;
    const float pi_4 = Mathf.PI / 4;
    float rotationRad;
    float dx;
    float dy;


    void Start()
    {
        cam = Camera.main;
        GameManager.player = this;
    }

    void Update()
    {
        var inputSteer = -Input.GetAxis("Horizontal");
        var inputY = Input.GetAxis("Vertical");

        transform.Rotate(0, 0, inputSteer * steeringSpeed);
        CalcRotation();                

        if (!HeadingForward())
            transform.Rotate(0, 0, -inputSteer * steeringSpeed);

        CalcRotation();
        CalcMovement(inputY);
        Move();
    }


    void CalcRotation()
    {
        var rotation = transform.rotation.eulerAngles.z;
        rotationRad = rotation / 360 * Mathf.PI * 2f;
    }

    bool HeadingForward()
    {
        return rotationRad > 7 * pi_4 ^ rotationRad < pi_4;
    }

    void CalcMovement(float inputY)
    {
        dx = -Mathf.Sin(rotationRad);
        dy = Mathf.Cos(rotationRad);
        acceleration = new Vector3(dx, dy) * gasBrakeSpeed * inputY;
        offsetToCamera -= acceleration.y * Time.deltaTime;
    }

    void Move(bool stop = false)
    {
        transform.localPosition += (new Vector3(speedY * dx,
            speedY * dy) + acceleration) * Time.deltaTime;

        var camPosition = cam.transform.position;
        camPosition.y = transform.position.y + offsetToCamera;
        cam.transform.position = camPosition;
    }
}
