using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    StateController controller;
    public GameObject[] waypoints = new GameObject[2];
    public float viewRange = 30;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<StateController>();
        controller.ChangeState(new GuardThink());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
