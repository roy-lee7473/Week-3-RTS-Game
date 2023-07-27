using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerThink : State
{
    Worker worker;
    GameObject target;
    TeamController teamController;
    
    GameObject unitHut;
    GameObject FWTroop;
    GameObject SSTroop;

    public override void OnEnter()
    {
        doNotRemove = true;
        worker = sc.gameObject.GetComponent<Worker>();
        target = sc.FindClosestTarget("Base", Mathf.Infinity);
        teamController = target.GetComponent<TeamController>();
        unitHut = teamController.unitHut;
        FWTroop = teamController.FWTroop;
        SSTroop = teamController.SSTroop;
    }

    public override void OnUpdate()
    {
        if(teamController.wood >= worker.buildingCost && !unitHut.activeSelf)
        {
            Debug.Log("adding worker build");
            sc.AddNewState(new WorkerBuild());
        }
        else if(teamController.food >= worker.troopCost && !FWTroop.activeSelf && unitHut.activeSelf)
        {
            sc.AddNewState(new WorkerSpawnTroops());
        }
        else if(teamController.food >= worker.troopCost && FWTroop.activeSelf && unitHut.activeSelf)
        {
            sc.AddNewState(new WorkerSpawnTroops());
        }
        else if(unitHut.activeSelf && !SSTroop.activeSelf)
        {
            Debug.Log("adding FoodGather()");
            sc.AddNewState(new WorkerGatherFood());
        }
        else
        {
            Debug.Log("adding WorkerGather()");
            sc.AddNewState(new WorkerGather());
        }
    }

    public override void OnExit()
    {
        //This state never exits
    }
}
