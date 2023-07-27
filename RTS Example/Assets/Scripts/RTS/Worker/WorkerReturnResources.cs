using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerReturnResources : State
{
    Worker worker;
    NavMeshAgent agent;
    GameObject target;

    public override void OnEnter()
    {
        Debug.Log("WorkerReturnResources::OnEnter()");
        worker = sc.gameObject.GetComponent<Worker>();
        agent = worker.GetComponent<NavMeshAgent>();
        FindBase();
    }

    public override void OnUpdate()
    {

    }

    public void FindBase()
    {
        target = sc.FindClosestTarget("Base", Mathf.Infinity);
        agent.destination = target.transform.position;
    }

    public void TransferResources()
    {
        //Resource in range, give resources
        agent.ResetPath();

        TeamController teamController = target.GetComponent<TeamController>();
        teamController.food += worker.foodCollected;
        teamController.wood += worker.woodCollected;

        worker.foodCollected = 0;
        worker.woodCollected = 0;

        Debug.Log("remove WorkerReturnResources()");
        sc.RemoveTop(); //state done, get out
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Base"))
        {
            TransferResources();
        }
    }

    public override void OnCollisionEnter(Collision collision)
    {

    }
}
