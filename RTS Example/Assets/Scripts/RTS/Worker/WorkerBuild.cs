using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerBuild : State
{
    Worker worker;
    NavMeshAgent agent;
    GameObject building;
    GameObject homeBase;
    GameObject unitHut;

    public override void OnEnter()
    {
        worker = sc.gameObject.GetComponent<Worker>();
        agent = worker.GetComponent<NavMeshAgent>();
        FindBuilding();
    }

    public override void OnUpdate()
    {

    }

    public void FindBuilding()
    {
        homeBase = sc.FindClosestTarget("Base", Mathf.Infinity);
        building = sc.FindClosestTarget("BuildingTrigger", Mathf.Infinity);
        agent.destination = building.transform.position;
        TeamController teamController = homeBase.GetComponent<TeamController>();
        unitHut = teamController.unitHut;
    }

    public void ResourcesCost()
    {
        TeamController teamController = homeBase.GetComponent<TeamController>();
        teamController.wood -= worker.buildingCost;
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BuildingTrigger"))
        {
            agent.ResetPath();
            ResourcesCost();
            Debug.Log("Building created");
            unitHut.SetActive(true);

            Debug.Log("remove worker build");
            sc.RemoveTop();
        }
    }
}
