using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    GameObject MainCamera;
    public Vector3 cameraHeight;
    public float throttlePostition;
    Vector3 acceleration;
    Vector3 velocity;
    public Ship currentShip;
    // Use this for initialization
    void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        throttlePostition = 100;
    }

    // Update is called once per frame
    void Update()
    {
        MainCamera.transform.position = transform.position + cameraHeight;
        acceleration = (transform.forward * currentShip.acceleration * throttlePostition * Time.deltaTime);
        velocity = velocity + (acceleration * Time.deltaTime);
        if(velocity.magnitude > currentShip.speed)
        {
            velocity -= (acceleration * Time.deltaTime);
        }
        if (throttlePostition < 0)
        {
            velocity = Vector3.zero;
        }
        transform.position = transform.position + (velocity * Time.deltaTime);
        if (isLocalPlayer)
        {
            CheckInput();
        }
    }

    void CheckInput()
    {
        throttlePostition = 0;
        if (Input.GetKey("a"))
        {
            transform.localEulerAngles += (-currentShip.turnSpeed * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            transform.localEulerAngles += (currentShip.turnSpeed * Time.deltaTime);
        }
        if (Input.GetKey("w"))
        {
            throttlePostition = 1;
        }
        if (Input.GetKey("s"))
        {
            throttlePostition = -1;
        }
    }
}
