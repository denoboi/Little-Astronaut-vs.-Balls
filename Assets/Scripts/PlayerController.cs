using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody playerRb;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        float verticalMove = Input.GetAxis("Vertical");
        playerRb.AddForce(Vector3.forward * verticalMove * speed);
    }
}
