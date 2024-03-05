using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

// En este script va a estar todo lo que tenga que ver con que el grafo funcione a un nivel básico.
public struct Node { 
 
    public string ID;

    public Node(string in_Id) 
    { 
        ID = in_Id;
    }

    // == es el operador de igualdad.
    public static bool operator ==(Node lhs, Node rhs)
    {
        if (rhs.ID == lhs.ID)
            return true; 
        return false;
    }

    // != es el operador de inigualdad.
    public static bool operator !=(Node lhs, Node rhs)
    {
        if (rhs.ID != lhs.ID)
            return true;
        return false;
    }

    // !


}

public struct Edge {
    public Node a;
    public Node b;

    public Edge(Node in_a, Node in_b)
    { 
        a = in_a;
        b = in_b;
    }
}

// Templates o plantillas.


public class BaseGraph : MonoBehaviour
{
    // MyStruct 
    // Esta clase es la que administra nuestros nodos y aristas.
    // List<Node> Nodes = new List<Node>();
    public List<Edge> Edges = new List<Edge>();
    public List<Node> Nodes = new List<Node>();

    private void GrafoDePrueba()
    {
        // Me faltaba ponerle el "public" al constructor!
        // Ponemos todos los nodos de nuestro diagrama.
        Node A = new Node("A");
        Node B = new Node("B");
        Node C = new Node("C");
        Node D = new Node("D");
        Node E = new Node("E");
        Node F = new Node("F");
        Node G = new Node("G");
        Node H = new Node("H");

        // Ahora ponemos los Edges/Aristas, es decir, conexiones entre nodos.
        Edge AB = new Edge(A, B);
        Edge AE = new Edge(A, E);

        Edge BC = new Edge(B, C);
        Edge BD = new Edge(B, D);
       
        Edge EF = new Edge(E, F);
        Edge EG = new Edge(E, G);
        Edge EH = new Edge(E, H);

        // Ahora que ya tenemos nuestro grafo, nos falta aplicar algún algoritmo sobre él.
        // Por ejemplo, Búsqueda en profundidad (Depth-First Search).

        // Mi prueba será que inicie en H y me diga si puede llegar a D.
    }

    public bool DepthFirstSearch(Node Current, Node Target)
    { 
        // Si el nodo donde estoy parado ahorita no es el nodo al que quiero llegar, entonces todavía no acabo.
        if(Current == Target)
            return true;
        // SI no son iguales, tenemos que seguir buscando.
        // Primero vamos al primer vecino de este nodo.

        // Tenemos que tener una forma de saber quiénes son los vecinos del nodo actual.

        // Hay que ver cuál de las aristas está conectada con H.
        // Vamos a hacer un método que nos diga con quién está conectado el nodo X.
        List<Edge> currentNeighbors = FindNeighbors(Current);
        // Visita a cada uno de ellos, hasta que se acaben o hasta que encontremos el objetivo.
        foreach(Edge e in currentNeighbors) 
        {
            Node NonCurrentNode = Current != e.a ? e.a : e.b;
            bool TargetFound = DepthFirstSearch(NonCurrentNode, Target);
            if (TargetFound)
            {
                return true;
            }
        }

        // Cuando ninguno de estos vecinos nos llevó al objetivo, regresamos False.
        return false;
    
    }

    // Método que nos dice quienes son los vecinos de un nodo dado.
    public List<Edge> FindNeighbors(Node in_node)
    { 
        List<Edge> out_list = new List<Edge>(); // empieza vacío.
        // Checar todas las aristas que hay, y meter a este out_vector todas las aristas que referencien al nodo dado.
        foreach (Edge myEdge in Edges) 
        {
            if (myEdge.a == in_node || myEdge.b == in_node)
            {
                out_list.Add(myEdge);
            }
        }

        return out_list;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
