using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;



public class SB_Seek : MonoBehaviour
{
    public enum SteeringBehavior 
    { 
        None,  // 0
        Seek,   // 1
        Flee,  // 2
        MAX     // 3
    };

    public SteeringBehavior currentBehavior = SteeringBehavior.Seek;

    // Vector tridimensional para la posición del mouse en el mundo
    Vector3 mouseWorldPos = Vector3.zero;

    public float maxSpeed = 1.0f;

    // Qué tanto tiempo queremos que pase antes de aplicar toda la steering force.
    public float maxSteeringForce = 1.0f;

    // Rigidbody ya trae dentro:
    // Vector tridimensional para la aceleración.
    // Vector tridimensional para representar esa velocidad
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        print("Funcion Start");
        rb = GetComponent<Rigidbody>();
    }

    //void myFunction()
    //{
    //    print("1");
    //    print("2");
    //    print("3");

    //    // myFunction();
    //}

    // Update is called once per frame
    void Update()
    {
        // Lo que esté dentro de la función update, se va a ejecutar cada que se pueda.
        // print("Funcion update");

        // Input.mousePosition // Nos da coordenadas en pixeles.
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);

        mouseWorldPos.z = 0;  // la sobreescribimos con 0 para que no afecte los cálculos en 2D.


        //velocity = 

        // Vector3.Angle

        // print(mousePos);

        // ¿Qué son coordenadas de Pixel?
        // 1920 (en X) * 1080 (en Y)
        // Esto nos da las coordenadas Normalizadas.
        // Normalizado es que va de 0 a 1
        // 0 de pixeles es el 0 de lo normalizado.
        // 1920 de pixeles el 1 de lo normalizado.
        // Qué número de pixel estaría en el 0.5 de lo normalizado? El 960.
        // 1920 * 0.5 = 960
        // 1920 * 0.2 = 384

        // Aplicamos método de Punta Menos Cola
        // Ya tenemos la posición del Mouse, Y ya tenemos la posición del agente.
        // Podemos decir la posición del mouse es la Punta,
        // ¿cuál es nuestra posición del mouse en el Código? la variable "mousePos".
        // y la posición de nuestro agente es la cola.
        // ¿Cuál es la posición de nuestro agente en el Código?
        // es la variable position del componente Transform de este GameObject
        // es decir: "transform.position".
        //                  Punta           Menos       Cola
        // Vector3 Distance = mouseWorldPos      -     transform.position;
        // print(Distance);


        // print(Time.deltaTime);

        // Nuestra velocidad es igual a nuestra velocidad actual + (la aceleración * tiempo transcurrido)
        // velocity = velocity + (acceleration * Time.deltaTime);

        // Nuestra nueva posición es igual a nuestra posición actual + (la velocidad * tiempo transcurrido)
        // transform.position = transform.position + (velocity * Time.deltaTime);

        // Velocidad está definido como: Distancia / Tiempo

        // Distancia + Distancia/Tiempo  (Esto de aquí no procedería)

        // Distancia + (Distancia/Tiempo)*Tiempo

        // Aceleración Distancia/Tiempo^2
    }

    void FixedUpdate()
    {
        // Fixed: Fijo
        // Solo se ejecuta un número fijo de veces por segundo.
        // Generalmente, ese es número es 60 (o 30).
        // print("Funcion fixedUpdate");

        // La declaramos aquí para poder usarla DENTRO del switch, pero que siga viva al salir del switch.
        Vector3 Distance = Vector3.zero;

        // Según el valor de la variable currentBehavior, es cuál Steering Behavior vamos a ejecutar.
        switch (currentBehavior)
        { 
            case SteeringBehavior.None:
                {
                    return;
                    // break;
                }
            case SteeringBehavior.Seek:
                {
                    // En qué dirección vamos a hacer que se mueva nuestro agente? En la dirección en la que está el mouse.
                    // Cuando hablemos de dirección, queremos vectores normalizados (es decir, de magnitude 1).
                    Distance = mouseWorldPos - transform.position;
                    break;
                }
            case SteeringBehavior.Flee:
                {
                    // En qué dirección vamos a hacer que se mueva nuestro agente? En la dirección en la que está el mouse.
                    // Cuando hablemos de dirección, queremos vectores normalizados (es decir, de magnitude 1).
                    Distance = transform.position - mouseWorldPos;
                    break;
                }
            case SteeringBehavior.MAX:
                break;
        }


        // Dirección actual de movimiento de nuestro agente:
        // La vamos a obtener a partir de la velocidad que trae.
        // Vector3 currentDirection = rb.velocity.normalized;  // Nos da la dirección. Es un vector de magnitud 1.
        // float currentMagnitude = rb.velocity.magnitude;

        Vector3 desiredDirection = Distance.normalized;  // queremos la dirección de ese vector, pero de magnitud 1.

        // queremos ir para esa dirección lo más rápido que se pueda.
        Vector3 desiredVelocity = desiredDirection * maxSpeed;

        // La diferencia entre la velocidad que tenemos actualmente y la que queremos tener.
        Vector3 steeringForce = desiredVelocity - rb.velocity;

        // Aquí la limitamos a que sea la mínima entre la fuerza que marca el algoritmo y la máxima
        // que deseamos que pueda tener.
        steeringForce = Vector3.Min(steeringForce, steeringForce.normalized * maxSteeringForce);

        rb.AddForce(steeringForce, ForceMode.Acceleration);
    }

    // Pursuit
    // Idea general es "no vayas ahorita hacia donde está tu objetivo,
    // ve hacia donde va a estar después de cierto tiempo."
    // queremos saber la posición actual de ese target.
    // queremos saber la velocidad actual de ese target.
    // ¿cuál va a ser el tiempo en el que queremos predecir su posición?
    // 


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, mouseWorldPos);

    }
}
