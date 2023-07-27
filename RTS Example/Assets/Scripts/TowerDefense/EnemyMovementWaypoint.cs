using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementWaypoint : MonoBehaviour
{
    public GameObject[] waypoints;              //This will be assigned by the WaveManager
    public float[] distanceToWaypoints;    //Precalculate the distance between each Waypoint
    public UnityEngine.AI.NavMeshAgent agent;
    public int waypointIndex = 0;
    public float distanceToExit = 0;

    // Start is called before the first frame update
    void Start()
    {
        PrecalculateDistanceToWaypoints();
    }

    // Update is called once per frame
    void Update()
    {
        //A bit slow to put it here (happens every frame), but helps to avoid a crash (plus can see it in Inspector)
        distanceToExit = DistanceToEnd();
    }
    
    //Precalculate the distanceToWaypoints array
    private void PrecalculateDistanceToWaypoints()
    {
        distanceToWaypoints = new float[waypoints.Length];

        //Ignore the first waypoint
        for (int i = 1; i < waypoints.Length; i++)
        {
            distanceToWaypoints[i] = Vector3.Distance(waypoints[i - 1].transform.position,
                                                      waypoints[i].transform.position);
        }
    }

    //Calculate the distance to the last waypoint (end of the path)
    public float DistanceToEnd()
    {
        //Distance left for current waypoint
        float distance = agent.remainingDistance;

        //Calculate distance for remaining waypoints (start after the current waypoint)
        for (int i = waypointIndex + 1; i < waypoints.Length; i++)
        {
            distance += distanceToWaypoints[i];
        }

        return distance;
    }

    public void StartMoving()
    {
        waypointIndex = 0;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = waypoints[waypointIndex].transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);

        if (other.name == "Exit")
        {
            HealthManager.ReduceHealth(1);              //Reduce health due to hit - this unit only causes one damage
            Destroy(gameObject);                        //delete unit
        }
        else if (other.CompareTag("Waypoint"))
        {
            waypointIndex++;
            agent.destination = waypoints[waypointIndex].transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("TowerBall"))
        {
            Destroy(collision.collider.gameObject);     //remove bullet
            ResourceManager.IncreaseGold(1);            //increase gold
            Destroy(gameObject);                        //delete unit - this unit only has 1 HP (1 shot kill)
        }
    }

    public void LaserHit()
    {
        ResourceManager.IncreaseGold(1);            //increase gold
        Destroy(gameObject);                        //delete unit - this unit only has 1 HP (1 shot kill)
    }
}
