using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMovement : MonoBehaviour
{
    float moveSpeed = 20;
    float shiftSpeed = 150;
    float rotationSpeed = 120;
    
    void Start()
    {
        
    }

    void Update()
    {
        //movement
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += moveSpeed * transform.forward * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.position += moveSpeed * -transform.right * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.position += moveSpeed * -transform.forward * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.position += moveSpeed * transform.right * Time.deltaTime;
        }

        //up and down
        if(Input.mouseScrollDelta.y > 0 )
        {
            transform.position += shiftSpeed * transform.up * Time.deltaTime;
        }
        if(Input.mouseScrollDelta.y < 0 )
        {
            transform.position += shiftSpeed * -transform.up * Time.deltaTime;
        }

        //rotation
        if(Input.GetKey(KeyCode.C))
        {
            transform.Rotate(new Vector3(-rotationSpeed * Time.deltaTime, 0, 0));
        }
        if(Input.GetKey(KeyCode.V))
        {
            transform.Rotate(new Vector3(rotationSpeed * Time.deltaTime, 0, 0));
        }
    }
}
