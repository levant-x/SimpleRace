using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public ScriptsManager manager;

    public float steeringSpeed = 0.3f;
    public float gasBrakeSpeed = 1f;
    public float moveSpeed = 0.2f;
    public float offsetToCamera = 3f;
    public float shmackForce = 100;
    public bool gameover = false;

    Rigidbody2D rBody;
    Vector3 acceleration;
    Camera cam;
    const float pi_4 = Mathf.PI / 4;
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
        if (gameover) return;

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        DispatchCollision(collision);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameover) return;
        DispatchCollision(collision.collider);
    }



    private void DispatchCollision(Collider2D collider)
    {
        if (gameover) return;
        var otherObject = collider.gameObject;
        gameManager.HandleCollision(otherObject);
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
        rBody.velocity = new Vector3(dx, dy) * moveSpeed + acceleration;

        var camPosition = cam.transform.position;
        camPosition.y = transform.position.y + offsetToCamera;
        cam.transform.position = camPosition;
    }
}
