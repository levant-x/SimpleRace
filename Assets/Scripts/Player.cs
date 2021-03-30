using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float steeringSpeed = 0.3f;
    public float speedX = 3f;
    public float speedY = 0.2f;

    float offsetToCamera = 3f;
    Vector3 acceleration;
    Camera cam;
    float pi_4 = Mathf.PI / 4;
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

        rotationRad = transform.rotation.eulerAngles.z /
            360 * Mathf.PI * 2f;

        Debug.Log(rotationRad);
        //var stop = ToManualMoving();
                
        transform.Rotate(0, 0, inputSteer * steeringSpeed);
        DetectMovement(inputY);
        Move();
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

    void Move(bool stop = false)
    {
        transform.localPosition += new Vector3(speedY * dx,
            speedY * dy) * Time.deltaTime;
        var camPosition = cam.transform.position;

        camPosition.y = transform.position.y + offsetToCamera;
        cam.transform.position = camPosition;
    }
}
