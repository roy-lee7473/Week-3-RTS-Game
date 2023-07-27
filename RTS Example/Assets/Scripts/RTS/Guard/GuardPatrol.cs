using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardPatrol : State
{
    Guard guard;
    public int waypointIndex = 0;
    NavMeshAgent agent;

    //When the state starts for the first time
    public override void OnEnter()
    {
        guard = sc.gameObject.GetComponent<Guard>();
        agent = guard.GetComponent<NavMeshAgent>();

        //Find nearest waypoint - walk to it
        GameObject waypoint = sc.FindClosestTarget("Waypoint", Mathf.Infinity);
        waypointIndex = Array.IndexOf(guard.waypoints, waypoint);

        agent.destination = waypoint.transform.position;
    }

    //Called during Update()
    public override void OnUpdate()
    {

    }

    //When state is turned off
    public override void OnExit()
    {

    }

    //When the object hits a trigger (or is a trigger)
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waypoint"))
        {
            waypointIndex++;
            if (waypointIndex >= guard.waypoints.Length) { waypointIndex = 0; }
            agent.destination = guard.waypoints[waypointIndex].transform.position;
        }
    }

    //When the object touches a RigidBody
    public override void OnCollisionEnter(Collision collision)
    {

    }
}
