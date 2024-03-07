using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

// En este script va a estar todo lo que tenga que ver con que el grafo funcione a un nivel básico.
public class Node { 
 
    public string ID;
    // Una que no me sabía yo de Unity C#: Las structs no pueden tener objetos de su propia struct, 
    // pero las classes sí. Probablemente se debe a que las Classes son siempre referencias (punteros) en C#.
    public Node parent;

    public Node(string in_Id) 
    { 
        ID = in_Id;
        parent = null;
    }

    //== es el operador de igualdad.
    //public static bool operator ==(Node lhs, Node rhs)
    //{
    //    if (rhs.ID == lhs.ID)
    //        return true;
    //    return false;
    //}

    //// != es el operador de inigualdad.
    //public static bool operator !=(Node lhs, Node rhs)
    //{
    //    if (rhs.ID != lhs.ID)
    //        return true;
    //    return false;
    //}

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


public enum NodeState
{ 
    Unknown = 0,
    Open, 
    Closed,
    MAX
}

public class BaseGraph : MonoBehaviour
{
    // MyStruct 
    // Esta clase es la que administra nuestros nodos y aristas.
    // List<Node> Nodes = new List<Node>();
    public List<Edge> Edges = new List<Edge>();
    public List<Node> Nodes = new List<Node>();

    // La lista abierta nos permite guardar a cuáles nodos ya hemos llegado pero no hemos terminado de visitar a sus vecinos
    public Dictionary<Node, NodeState> NodeStateDict = new Dictionary<Node, NodeState>();
    // La lista cerrada nos permite guardar a los nodos que ya terminamos de explor
    // es decir, en cuáles ya no hay nada más que hacer.

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

        Nodes.Add(A);
        Nodes.Add(B);
        Nodes.Add(C);
        Nodes.Add(D);
        Nodes.Add(E);
        Nodes.Add(F);
        Nodes.Add(G);
        Nodes.Add(H);

        // Por defecto, nuestro Diccionario (que tiene tanto la lista de abiertos como de cerrados)
        // Va a tener a todos nuestros nodos.
        NodeStateDict.Add(A, NodeState.Unknown);
        NodeStateDict.Add(B, NodeState.Unknown);
        NodeStateDict.Add(C, NodeState.Unknown);
        NodeStateDict.Add(D, NodeState.Unknown);
        NodeStateDict.Add(E, NodeState.Unknown);
        NodeStateDict.Add(F, NodeState.Unknown);
        NodeStateDict.Add(G, NodeState.Unknown);
        NodeStateDict.Add(H, NodeState.Unknown);


        // Ahora ponemos los Edges/Aristas, es decir, conexiones entre nodos.
        Edge AB = new Edge(A, B);
        Edge AE = new Edge(A, E);

        Edge BC = new Edge(B, C);
        Edge BD = new Edge(B, D);
       
        Edge EF = new Edge(E, F);
        Edge EG = new Edge(E, G);
        Edge EH = new Edge(E, H);

        // Metemos nuestras aristas en la lista de Aristas.
        Edges.Add(EH);
        Edges.Add(EG);
        Edges.Add(EF);
        Edges.Add(BD);
        Edges.Add(BC);
        Edges.Add(AE);
        Edges.Add(AB);

        // Ahora que ya tenemos nuestro grafo, nos falta aplicar algún algoritmo sobre él.
        // Por ejemplo, Búsqueda en profundidad (Depth-First Search).

        // Mi prueba será que inicie en H y me diga si puede llegar a D.
        NodeStateDict[H] = NodeState.Open;
        bool pathExists = DepthFirstSearch(H, D);
        if (pathExists)
        {   
            print("Sí hay un camino de H a D.");
            List<Node> pathToGoal = new List<Node>();
            Node currentNode = D;
            while (currentNode != null)
            { 
                pathToGoal.Add(currentNode); 
                currentNode = currentNode.parent;
            }
            foreach (Node node in pathToGoal)
            {
                print("El nodo: " + node.ID + " fue parte del camino a la meta");
            }
        }
        else
            print("No hay camino de H a D.");
    }

    //public bool ItDepthFirstSearch(Node Origin, Node Target)
    //{
    //    NodeStateDict[Origin] = NodeState.Open;

    //    Stack<Node>
    //    // Desde ese nodo
    //    while()


    //    return false;
    //}


    public bool DepthFirstSearch(Node Current, Node Target)
    {
        // Cuando tú te paras en un nodo, lo primero que tienes que hacer es si ya está en la lista cerrada.
        // Si ya está en la cerrada, no hay nada más que hacer.
        if(NodeStateDict[Current] == NodeState.Closed)
            return false;

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
            // Primero checamos si ya está en la lista abierta, y si lo está, no mandamos a llamar el algoritmo.
            if (NodeStateDict[NonCurrentNode] == NodeState.Open)
                continue;
            else
            { 
                // Si no, lo ponemos como que ya está en la lista abierta.
                NodeStateDict[NonCurrentNode] = NodeState.Open;
                // Cuando tú (Nodo Current) metes a otro nodo a la lista abierta, te pones como su parent node.
                NonCurrentNode.parent = Current;
            }

            // Marcamos el nodo como que está en la lista abierta.
            bool TargetFound = DepthFirstSearch(NonCurrentNode, Target);
            if (TargetFound)
            {
                print("El nodo: " + Current.ID + " fue parte del camino a la meta.");
                return true;
            }
        }

        NodeStateDict[Current] = NodeState.Closed;

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
        GrafoDePrueba();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
