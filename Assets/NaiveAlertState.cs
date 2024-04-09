using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaiveAlertState : NaiveFSMState
{
    NaiveAlertState(string name, NaiveFSM FSM)
    {
        Name = name; // el nombre por el cual conocemos a este estado.
        _FSM = FSM;  // la m�quina de estados que es due�a de este estado
    }

    public override void Enter()
    {
        // base.Enter();
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override void Exit()
    {
        // base.Exit();
    }
}
