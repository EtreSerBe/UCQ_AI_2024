using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaiveAlertState : NaiveFSMState
{
    public float TimeDetectingPlayerBeforeEnteringAttack;
    public float AccumulatedTimeDetectingPlayerBeforeEnteringAttack;

    // Si despu�s de un cierto tiempo de la �ltima vez que detect� al jugador ya no lo ha visto.
    // Entonces, vamos a la �ltima posici�n conocida del sospechoso. (A una cierta velocidad de movimiento)
    // Si al llegar a esa �ltima posici�n conocida seguimos sin detectar al jugador,
    // entonces el agente se regresar� hacia el punto de patrullaje.
    // Si llega al punto de patrullaje y sigue sin detectar al jugador, 
    // entonces pasar� al estado de patrullaje.

    //  cierto tiempo de la �ltima vez que detect� al jugador, es decir, en qu� momento en el tiempo se detect�.
    private float LastTimePlayerSeen = 0;
    // Cu�nto tiempo debe pasar entre la �ltima vez que vio al jugador, para decir: ya toca ir a la �ltima posici�n conocida.
    public float TimeSinceLastSeenTreshold = 2;
    // Espec�ficamente para este caso, en vez de un AccumulatedTime, vamos a usar :
    // la diferencia entre LastTimePlayerSeen y Time.realtimeSinceStartup.

    // la �ltima posici�n conocida del sospechoso
    // Si nos ponemos elaborados, hasta podr�amos darle un margen de error
    private Vector3 LastKnownPlayerLocation = Vector3.zero;

    // A una cierta velocidad de movimiento
    public float SpeedWhileCheckingLastKnownLocation = 5.0f;

    // el agente se regresar� hacia el punto de patrullaje
    // Este ya lo tenemos, a trav�s de la FSM.

    public NaiveAlertState(NaiveFSM FSM)
    {
        Name = "Alert"; // el nombre por el cual conocemos a este estado.
        _FSM = FSM;  // la m�quina de estados que es due�a de este estado
    }

    public override void Enter()
    {
        base.Enter();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        // Time.realtimeSinceStartup
    }

    public override void Exit()
    {
        base.Exit();
    }
}
