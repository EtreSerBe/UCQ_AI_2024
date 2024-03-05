using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringHijo : SB_Seek
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public new void Update()
    {
        print("Inicio del Update del Hijo.");
        base.Update();
        print("Fin del Update del Hijo.");
    }


    void FixedUpdate() 
    {
        
    }

}
