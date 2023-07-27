using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCannonBall : MonoBehaviour
{
    public Vector3 direction = Vector3.zero;
    Vector3 velocity = Vector3.zero;
    float speed = 10f;
    float gravity = -12f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        velocity.y += gravity * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
        velocity = direction.normalized * speed;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Ground")
        {
            //Spawn explosion

            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
