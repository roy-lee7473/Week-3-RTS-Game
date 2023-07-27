using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerGatherFood : State
{
    Worker worker;
    NavMeshAgent agent;
    GameObject target;

    public override void OnEnter()
    {
        Debug.Log("FoodGather::OnEnter()");

        worker = sc.gameObject.GetComponent<Worker>();
        agent = worker.GetComponent<NavMeshAgent>();
        //Debug.Log("OnEnter: " + worker);

        //Find nearest resources
        FindNearestResource();
    }

    public override void OnUpdate()
    {
        if (worker == null)
        {
            //Just a check to prevent errors (shouldn't reach this)
            Debug.Log("unreachable code was reached");
            return;
        }

        if (target == null)
        {
            //No resource, find next resource
            FindNearestResource();
        }
        else if (worker.BackpackFull())
        {
            //Cannot carry anymore, transition to return state
            Debug.Log("FoodGather() change to WorkerReturnResources()");
            sc.ChangeState(new WorkerReturnResources());
        }
        else if (Vector3.Distance(worker.transform.position, target.transform.position) <= worker.harvestRange)
        {
            //Resource in range, harvest
            agent.ResetPath();
            if (target.CompareTag("Food"))
            {
                worker.foodCollected += worker.harvestRate * Time.deltaTime;
            }
        }
    }

    public override void OnExit()
    {
        //Nothing to do here, just move along
        Debug.Log("FoodGather::OnExit()");
    }

    public void FindNearestResource()
    {
        target = sc.FindClosestTarget("Food", worker.viewRange);

        if (target == null)
        {
            //No resource found, remove state
            sc.RemoveTop();
        }
        else
        {
            agent.destination = target.transform.position;
        }
    }
}
