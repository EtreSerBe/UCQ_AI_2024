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
    // 
    public float RotationAngle;
    public float TimeBeforeRotating;
    public float AccumulatedTimeBeforeRotating;
    public float TimeDetectingPlayerBeforeEnteringAlert;
    public float AccumulatedTimeDetectingPlayerBeforeEnteringAlert;

    public void Init(float in_RotationAngle, float in_TimeBeforeRotating, float in_TimeDetectingPlayerBeforeEnteringAlert)
    { 
        RotationAngle = in_RotationAngle;
        TimeBeforeRotating = in_TimeBeforeRotating;
        TimeDetectingPlayerBeforeEnteringAlert = in_TimeDetectingPlayerBeforeEnteringAlert;
    }

    // Ojo: no hagan esto, porque conlleva a situaciones molestas y propensas a errores humanos.
    // Referencia al estado de alerta (OJO, ESTO CAUSARÁ ALTA DEPENDENCIA ENTRE LAS CLASES)
    // NaiveAlertState _AlertState;

    public NaivePatrolState(NaiveFSM FSM)
    {
        Name = "Patrol";
        _FSM = FSM;
    }


    public override void Enter()
    { 
        // 
        base.Enter();

        Debug.Log("Entré al estado de Patrullaje.");
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
        if (Input.GetKey(KeyCode.W))  // Esta va a ser mi "cono de visión". Si se aprieta la tecla W, sí lo está viendo.
        {
            // Si la función del cono de visión nos regresa verdadero
            // Acumulamos tiempo
            // Lo acumulamos en la variable que declaramos para llevar el tiempo acumulado "AccumulatedTimeDetectingPlayerBeforeEnteringAlert"
            AccumulatedTimeDetectingPlayerBeforeEnteringAlert += Time.deltaTime;
            // si el tiempo acumulado es mayor a la cantidad de tiempo que establecimos, cambiamos al estado de alerta
            if (TimeDetectingPlayerBeforeEnteringAlert <= AccumulatedTimeDetectingPlayerBeforeEnteringAlert)
            {
                // Si sí, cambiamos al estado de alerta.
                // (PatrolAgentFSM)_FSM -> casteamos la máquina de estados base, al tipo específico de la FSM que es nuestra dueña.
                // esto nos permite acceder a las variables que tiene esa clase específica, en este caso, al estado de Alerta al que
                // queremos pasar, que lo obtenemos a través de "AlertStateRef".
                PatrolAgentFSM SpecificFSM = (PatrolAgentFSM)_FSM;
                NaiveAlertState AlertStateInstance = SpecificFSM.AlertStateRef;
                _FSM.ChangeState(AlertStateInstance);
                return; // Damos return siempre después del change state, para evitar que cualquier otra cosa 
                // del update se fuera a ejecutar.
            }
        }
        // Usaríamos nuestra función del cono de visión
        // Sí, entonces:




        // Para esto, usaríamos la variable que declaramos "TimeDetectingPlayerBeforeEnteringAlert"
        // Aquí es donde le pediríamos a la máquina de estados que nos mande al estado de Alerta.
        // _FSM.ChangeState(_AlertState);

    }

    // Aquí estamos omitiendo a propósito la función Exit()
    // solo para demostrar que podemos hacerlo.
}


