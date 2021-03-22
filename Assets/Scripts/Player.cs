using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager manager;
    public float speedSteer = 0.3f;
    public float speedY = 0.2f;


    float speed;

    private void Start()
    {
        speed = manager.roadSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        var inputSteer = Input.GetAxis("Horizontal");
        var inputY = Input.GetAxis("Vertical");
        

        transform.Rotate(0, 0, -(inputSteer * speedSteer));
        var positionDelta = new Vector3(
            -speed * 
            Mathf.Sin(transform.rotation.z),
            0);

        manager.roadSpeed = speed * inputY;

        Debug.Log(transform.rotation.eulerAngles.z);
        Debug.Log(Mathf.Sin(transform.rotation.z));

        //transform.position += positionDelta;
    }
}
