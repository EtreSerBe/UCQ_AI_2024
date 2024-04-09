using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaivePatrolState : NaiveFSMState
{
    Vector3 InitialPatrollingPosition;
    // Variables No-exclusivas del estado
    // Cono de visión
    // Las dejé comentadas como ejemplo de que son las no-exclusivas del estado, y que me las llevé a la máquina de estados.
    //public float VisionAngle;
    //public float VisionDistance;
    //public bool DetectedPlayer;


    // Variables Exclusivas de este estado.
    // public float MovementSpeed;
    public float RotationAngle;
    public float TimeBeforeRotating;
    public float AccumulatedTimeBeforeRotating;
    public float TimeDetectingPlayerBeforeEnteringAlert;
    public float AccumulatedTimeDetectingPlayerBeforeEnteringAlert;

    // Ojo: no hagan esto, porque conlleva a situaciones molestas y propensas a errores humanos.
    // Referencia al estado de alerta (OJO, ESTO CAUSARÁ ALTA DEPENDENCIA ENTRE LAS CLASES)
    // NaiveAlertState _AlertState;

    NaivePatrolState(string name, NaiveFSM FSM)
    {
        Name = name;
        _FSM = FSM;
    }


    public override void Enter()
    { 
        // 
        base.Enter();

        // Acá ya puedo hacer lo que esta clase hija específicamente tiene que hacer
        AccumulatedTimeBeforeRotating = 0.0f;
        AccumulatedTimeDetectingPlayerBeforeEnteringAlert = 0.0f;
    }

    // Update is called once per frame
    public override void Update()
    {
        Debug.Log("Update del estado patrullaje.");

        // Detectar al infiltrador

        // Lo detectamos?
        // Usaríamos nuestra función del cono de visión
        // Sí, entonces:
            // Si la función del cono de visión nos regresa verdadero
            // Acumulamos tiempo
            // Lo acumulamos en la variable que declaramos para llevar el tiempo acumulado "AccumulatedTimeDetectingPlayerBeforeEnteringAlert"
            // si el tiempo acumulado es mayor a la cantidad de tiempo que establecimos, cambiamos al estado de alerta
            // Para esto, usaríamos la variable que declaramos "TimeDetectingPlayerBeforeEnteringAlert"
                // Aquí es donde le pediríamos a la máquina de estados que nos mande al estado de Alerta.
                // _FSM.ChangeState(_AlertState);

    }

    // Aquí estamos omitiendo a propósito la función Exit()
    // solo para demostrar que podemos hacerlo.
}


