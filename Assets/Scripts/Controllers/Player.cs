using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CollisionManager collisionManager;

    public float steeringSpeed = 0.3f;
    public float gasBrakeSpeed = 1f;
    public float speedX = 3f;
    public float speedY = 0.2f;
    public bool lost = false;

    Rigidbody2D rBody;
    Vector3 acceleration;
    Camera cam;
    const float pi_4 = Mathf.PI / 4;
    float offsetToCamera = 3f;
    float rotationRad;
    float dx;
    float dy;


    void Start()
    {
        cam = Camera.main;
        rBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (lost) return;

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

    void OnTriggerEnter2D(Collider2D collider)
    {
        collisionManager.HandleCollision(collider);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        collisionManager.HandleCollision(collision.collider);
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
        rBody.velocity = (new Vector3(dx, dy) * speedY + acceleration);

        var camPosition = cam.transform.position;
        camPosition.y = transform.position.y + offsetToCamera;
        cam.transform.position = camPosition;
    }
}
