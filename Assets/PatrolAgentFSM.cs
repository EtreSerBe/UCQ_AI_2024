using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Como NaiveFSM ya hereda de monoBehaviour, entonces PatrolAgentFSM tambi�n ya tiene las propiedades de MonoBehaviour.
public class PatrolAgentFSM : NaiveFSM
{
    private NaivePatrolState _PatrolState;
    private NaiveAlertState _AlertState;

    public NaivePatrolState PatrolStateRef
    { 
        get { return _PatrolState; }
    }

    public NaiveAlertState AlertStateRef
    {
        get { return _AlertState; }
    }

    // public float MovementSpeed;
    // La posici�n de nuestro agente la podemos acceder a trav�s del transform.

    public Vector3 InitialPatrolPosition = Vector3.zero;
    public Transform InitialPatrolTransform;

    // Otra ventaja de tenerlas en la FSM es que estas variables las podemos acceder y modificar desde el editor.
    public float VisionAngle = 45.0f;
    public float VisionDistance = 20.0f;
    // La variable detected plauyer s� queremos que sea accesible desde otros pedazos de c�digo, pero no desde el editor.
    private bool _DetectedPlayer = false;

    public bool DetectedPlayer
    {
        get { return _DetectedPlayer; }
        set { _DetectedPlayer = value; }
    }


    //private string _myPvtString = "pvtString";

    //public string MyPvtString
    //{ 
    //    get { return _myPvtString; }
    //    set { if (value != null) { _myPvtString = value; } }
    //}


    // Pero... �y qu� de las variables que son espec�ficas de ciertos estados?
    // Depende de c�mo lo quieran hacer ustedes.
    // Alternativa 1) s� poner las variables esas aqu� en la FSM :C 
    // Lo malo es que nos podr�an estar haciendo bulto en la FSM ya que no se usan en los dem�s estados.
    // Alternativa 2) Usar un archivo de lectura que contenga todos los valores para esas variables.
    // Por ejemplo, usar un archivo de tipo JSON
    // https://docs.unity3d.com/ScriptReference/JsonUtility.FromJson.html
    // Alternativa 2.5) Leer los valores desde un Excel o base de datos.
    // Cada una tiene sus ventajas y desventajas.

    // Grupo de variables para el estado de Patrullaje (NaivePatrolState)
    // Estas variables solo se van a setear en el estado al iniciar la m�quina de estados (por el momento).
    public float RotationAngle = 45.0f;
    public float TimeBeforeRotating = 3.0f;
    public float TimeDetectingPlayerBeforeEnteringAlert = 2.0f;
    

    // Esta es una extensi�n del Start de la clase FSM base, por lo que hace todo lo que ella har�a, m�s 
    // lo que se necesite espec�ficamente en esta clase.
    public override void Start()
    {
        // Le decimos expl�citamente que mande a llamar el start de su clase padre.
        base.Start();



        _PatrolState = (NaivePatrolState)_CurrentState;
        _AlertState = new NaiveAlertState(this);
        // _AttackState = new NaiveAttackState(this);

        _PatrolState.Init(RotationAngle, TimeBeforeRotating, TimeDetectingPlayerBeforeEnteringAlert);
    }

    protected override NaiveFSMState GetInitialState()
    {
        // Regresa null para que cause error porque la funci�n de esta clase padre nunca debe de usarse, siempre 
        // se le debe de hacer un override.
        return new NaivePatrolState(this);
    }


}
