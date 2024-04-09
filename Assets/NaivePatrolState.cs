using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaivePatrolState : NaiveFSMState
{
    Vector3 InitialPatrollingPosition;
    // Variables No-exclusivas del estado
    // Cono de visi�n
    // Las dej� comentadas como ejemplo de que son las no-exclusivas del estado, y que me las llev� a la m�quina de estados.
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
    // Referencia al estado de alerta (OJO, ESTO CAUSAR� ALTA DEPENDENCIA ENTRE LAS CLASES)
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

        // Ac� ya puedo hacer lo que esta clase hija espec�ficamente tiene que hacer
        AccumulatedTimeBeforeRotating = 0.0f;
        AccumulatedTimeDetectingPlayerBeforeEnteringAlert = 0.0f;
    }

    // Update is called once per frame
    public override void Update()
    {
        Debug.Log("Update del estado patrullaje.");

        // Detectar al infiltrador

        // Lo detectamos?
        // Usar�amos nuestra funci�n del cono de visi�n
        // S�, entonces:
            // Si la funci�n del cono de visi�n nos regresa verdadero
            // Acumulamos tiempo
            // Lo acumulamos en la variable que declaramos para llevar el tiempo acumulado "AccumulatedTimeDetectingPlayerBeforeEnteringAlert"
            // si el tiempo acumulado es mayor a la cantidad de tiempo que establecimos, cambiamos al estado de alerta
            // Para esto, usar�amos la variable que declaramos "TimeDetectingPlayerBeforeEnteringAlert"
                // Aqu� es donde le pedir�amos a la m�quina de estados que nos mande al estado de Alerta.
                // _FSM.ChangeState(_AlertState);

    }

    // Aqu� estamos omitiendo a prop�sito la funci�n Exit()
    // solo para demostrar que podemos hacerlo.
}


