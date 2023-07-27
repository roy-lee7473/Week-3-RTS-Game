using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerSpawnTroops : State
{
    Worker worker;
    NavMeshAgent agent;
    GameObject homeBase;
    GameObject trigger;
    public GameObject FWTroop;
    public GameObject SSTroop;
    TeamController teamController;

    public override void OnEnter()
    {
        trigger = sc.FindClosestTarget("BuildingTrigger", Mathf.Infinity);
        worker = sc.gameObject.GetComponent<Worker>();
        agent = worker.GetComponent<NavMeshAgent>();
        homeBase = sc.FindClosestTarget("Base", Mathf.Infinity);
        teamController = homeBase.GetComponent<TeamController>();
        FWTroop = teamController.FWTroop;
        SSTroop = teamController.SSTroop;
    }

    public override void OnUpdate()
    {
        if(teamController.food >= worker.troopCost)
        {
            agent.destination = trigger.transform.position;
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BuildingTrigger"))
        {
            if(!FWTroop.activeSelf)
            {
                FWTroop.SetActive(true);
                teamController.food -= worker.troopCost;
                sc.RemoveTop();
            }
            else
            {
                SSTroop.SetActive(true);
                teamController.food -= worker.troopCost;
                sc.RemoveTop();
            }
        }
    }
}
