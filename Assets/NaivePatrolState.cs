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
    // Referencia al estado de alerta (OJO, ESTO CAUSAR� ALTA DEPENDENCIA ENTRE LAS CLASES)
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

        Debug.Log("Entr� al estado de Patrullaje.");
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
        if (Input.GetKey(KeyCode.W))  // Esta va a ser mi "cono de visi�n". Si se aprieta la tecla W, s� lo est� viendo.
        {
            // Si la funci�n del cono de visi�n nos regresa verdadero
            // Acumulamos tiempo
            // Lo acumulamos en la variable que declaramos para llevar el tiempo acumulado "AccumulatedTimeDetectingPlayerBeforeEnteringAlert"
            AccumulatedTimeDetectingPlayerBeforeEnteringAlert += Time.deltaTime;
            // si el tiempo acumulado es mayor a la cantidad de tiempo que establecimos, cambiamos al estado de alerta
            if (TimeDetectingPlayerBeforeEnteringAlert <= AccumulatedTimeDetectingPlayerBeforeEnteringAlert)
            {
                // Si s�, cambiamos al estado de alerta.
                // (PatrolAgentFSM)_FSM -> casteamos la m�quina de estados base, al tipo espec�fico de la FSM que es nuestra due�a.
                // esto nos permite acceder a las variables que tiene esa clase espec�fica, en este caso, al estado de Alerta al que
                // queremos pasar, que lo obtenemos a trav�s de "AlertStateRef".
                PatrolAgentFSM SpecificFSM = (PatrolAgentFSM)_FSM;
                NaiveAlertState AlertStateInstance = SpecificFSM.AlertStateRef;
                _FSM.ChangeState(AlertStateInstance);
                return; // Damos return siempre despu�s del change state, para evitar que cualquier otra cosa 
                // del update se fuera a ejecutar.
            }
        }
        // Usar�amos nuestra funci�n del cono de visi�n
        // S�, entonces:




        // Para esto, usar�amos la variable que declaramos "TimeDetectingPlayerBeforeEnteringAlert"
        // Aqu� es donde le pedir�amos a la m�quina de estados que nos mande al estado de Alerta.
        // _FSM.ChangeState(_AlertState);

    }

    // Aqu� estamos omitiendo a prop�sito la funci�n Exit()
    // solo para demostrar que podemos hacerlo.
}


