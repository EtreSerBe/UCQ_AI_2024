using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Como NaiveFSM ya hereda de monoBehaviour, entonces PatrolAgentFSM también ya tiene las propiedades de MonoBehaviour.
public class PatrolAgentFSM : NaiveFSM
{
    // Otra ventaja de tenerlas en la FSM es que estas variables las podemos acceder y modificar desde el editor.
    public float VisionAngle;
    public float VisionDistance;
    private bool DetectedPlayer;

    //private string _myPvtString = "pvtString";

    //public string MyPvtString
    //{ 
    //    get { return _myPvtString; }
    //    set { if (value != null) { _myPvtString = value; } }
    //}


    // Pero... ¿y qué de las variables que son específicas de ciertos estados?
    // Depende de cómo lo quieran hacer ustedes.
    // Alternativa 1) sí poner las variables esas aquí en la FSM :C 
        // Lo malo es que nos podrían estar haciendo bulto en la FSM ya que no se usan en los demás estados.
    // Alternativa 2) Usar un archivo de lectura que contenga todos los valores para esas variables.
    // Alternativa 2.5) Leer los valores desde un Excel o base de datos.
    // Cada una tiene sus ventajas y desventajas.



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(MyPvtString);
        MyPvtString = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
