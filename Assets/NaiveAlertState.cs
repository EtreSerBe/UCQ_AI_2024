using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaiveAlertState : NaiveFSMState
{
    public float TimeDetectingPlayerBeforeEnteringAttack;
    public float AccumulatedTimeDetectingPlayerBeforeEnteringAttack;

    // Si después de un cierto tiempo de la última vez que detectó al jugador ya no lo ha visto.
    // Entonces, vamos a la última posición conocida del sospechoso. (A una cierta velocidad de movimiento)
    // Si al llegar a esa última posición conocida seguimos sin detectar al jugador,
    // entonces el agente se regresará hacia el punto de patrullaje.
    // Si llega al punto de patrullaje y sigue sin detectar al jugador, 
    // entonces pasará al estado de patrullaje.

    //  cierto tiempo de la última vez que detectó al jugador, es decir, en qué momento en el tiempo se detectó.
    private float LastTimePlayerSeen = 0;
    // Cuánto tiempo debe pasar entre la última vez que vio al jugador, para decir: ya toca ir a la última posición conocida.
    public float TimeSinceLastSeenTreshold = 2;
    // Específicamente para este caso, en vez de un AccumulatedTime, vamos a usar :
    // la diferencia entre LastTimePlayerSeen y Time.realtimeSinceStartup.

    // la última posición conocida del sospechoso
    // Si nos ponemos elaborados, hasta podríamos darle un margen de error
    private Vector3 LastKnownPlayerLocation = Vector3.zero;

    // A una cierta velocidad de movimiento
    public float SpeedWhileCheckingLastKnownLocation = 5.0f;

    // el agente se regresará hacia el punto de patrullaje
    // Este ya lo tenemos, a través de la FSM.

    public NaiveAlertState(NaiveFSM FSM)
    {
        Name = "Alert"; // el nombre por el cual conocemos a este estado.
        _FSM = FSM;  // la máquina de estados que es dueña de este estado
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
