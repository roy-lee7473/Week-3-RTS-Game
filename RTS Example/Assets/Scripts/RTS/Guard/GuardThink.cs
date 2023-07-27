using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardThink : State
{
    Guard guard;

    public override void OnEnter()
    {
        doNotRemove = true;
        guard = sc.gameObject.GetComponent<Guard>();
    }

    public override void OnUpdate()
    {
        //What does the guard do?

        //Just patrol
        sc.AddNewState(new GuardPatrol());
    }
    
    public override void OnExit()
    {
        //This state never exits
    }
}
