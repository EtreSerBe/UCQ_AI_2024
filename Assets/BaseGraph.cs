using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

// En este script va a estar todo lo que tenga que ver con que el grafo funcione a un nivel b�sico.
public class Node
{

    public string ID;
    // Una que no me sab�a yo de Unity C#: Las structs no pueden tener objetos de su propia struct, 
    // pero las classes s�. Probablemente se debe a que las Classes son siempre referencias (punteros) en C#.
    public Node parent;

    public Vector3 position;

    public Node(string in_Id)
    {
        ID = in_Id;
        parent = null;
        position = Vector3.zero;
    }

    public Node(string in_Id, float rangeX, float rangeY)
    {
        ID = in_Id;
        parent = null;
        GetRandomPositionInRange(rangeX, rangeY);
        lineRenderer = null;
    }



    public void GetRandomPositionInRange(float rangeX, float rangeY)
    { 
        position = new Vector3(Random.Range(-rangeX, rangeX), Random.Range(-rangeY, rangeY), 0.0f);
    }

    LineRenderer lineRenderer;
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

public struct Edge
{
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
    public float NodePositionRangeX = 5.0f;
    public float NodePositionRangeY = 5.0f;

    LineRenderer lineRenderer = null;
    private List<Vector3> NodePositions = new List<Vector3>();

    // MyStruct 
    // Esta clase es la que administra nuestros nodos y aristas.
    // List<Node> Nodes = new List<Node>();
    public List<Edge> Edges = new List<Edge>();
    public List<Node> Nodes = new List<Node>();

    // La lista abierta nos permite guardar a cu�les nodos ya hemos llegado pero no hemos terminado de visitar a sus vecinos
    public Dictionary<Node, NodeState> NodeStateDict = new Dictionary<Node, NodeState>();
    // La lista cerrada nos permite guardar a los nodos que ya terminamos de explor
    // es decir, en cu�les ya no hay nada m�s que hacer.


    // 1) Necesitamos que s� respete el orden, espec�ficamente que el �ltimo elemento en a�adirse sea el primero en salir
    // 2) Tenemos que poder checar si el nodo a meter YA est� en la lista abierta.
    // 3) Que agregar y quitar elementos de la estructura de datos sea r�pido.
    public Stack<Node> OpenListStack = new Stack<Node>();
    // public List<Node> ClosedList = new List<Node>();
    // public Stack<Node> ClosedList = new Stack<Node>();

    // Lista abierta para el algoritmo Breadth First Search.
    public Queue<Node> OpenListQueue = new Queue<Node>();

    // Las propiedades que queremos de la estructura de datos para nuestra lista cerrada son:
    // 1) No necesitamos que respete el orden en que se a�adieron los nodos
    // 2) Tiene que agregar nodos lo m�s r�pido posible
    // 3) Tiene que poder checar si contiene o no a un nodo dado r�pidamente.
    public HashSet<Node> ClosedSetList = new HashSet<Node>();

    public void AddPointForRenderer(Node Point)
    {
        NodePositions.Add(Point.position);
    }

    public void SetRendererPositions()
    {
        lineRenderer.positionCount = NodePositions.Count;
        lineRenderer.SetPositions(NodePositions.ToArray());
    }

    private void GrafoDePrueba()
    {
        // Me faltaba ponerle el "public" al constructor!
        // Ponemos todos los nodos de nuestro diagrama.
        Node A = new Node("A", NodePositionRangeX, NodePositionRangeY);
        Node B = new Node("B", NodePositionRangeX, NodePositionRangeY);
        Node C = new Node("C", NodePositionRangeX, NodePositionRangeY);
        Node D = new Node("D", NodePositionRangeX, NodePositionRangeY);
        Node E = new Node("E", NodePositionRangeX, NodePositionRangeY);
        Node F = new Node("F", NodePositionRangeX, NodePositionRangeY);
        Node G = new Node("G", NodePositionRangeX, NodePositionRangeY);
        Node H = new Node("H", NodePositionRangeX, NodePositionRangeY);

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

        // Ahora que ya tenemos nuestro grafo, nos falta aplicar alg�n algoritmo sobre �l.
        // Por ejemplo, B�squeda en profundidad (Depth-First Search).

        // Mi prueba ser� que inicie en H y me diga si puede llegar a D.
        NodeStateDict[H] = NodeState.Open;
        bool pathExists = DepthFirstSearch(H, D);
        if (pathExists)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            print("S� hay un camino de H a D.");
            List<Node> pathToGoal = new List<Node>();
            Node currentNode = D;
            while (currentNode != null)
            {
                AddPointForRenderer(currentNode);
                pathToGoal.Add(currentNode);
                currentNode = currentNode.parent;
            }
            foreach (Node node in pathToGoal)
            {
                print("El nodo: " + node.ID + " fue parte del camino a la meta");
            }
            SetRendererPositions();
        }
        else
            print("No hay camino de H a D.");
    }

    public bool ItDepthFirstSearch(Node Origin, Node Target)
    {
        OpenListStack.Clear();
        ClosedSetList.Clear();

        // empezamos en el nodo Origen.
        // Ponemos al nodo origen en la lista abierta.
        OpenListStack.Push(Origin);

        Node currentNode = Origin;

        // Otra posibilidad es while(currentNode != null),
        while (OpenListStack.Count != 0)  // Mientras haya otros nodos por visitar, sigue ejecutando el algoritmo.
        {
            // ya que sabemos cual es el nodo actual, podemos empezar a meter a sus vecinos a la lista abierta.

            // Necesitamos quitar elementos de la lista abierta en alg�n punto de este ciclo while.
            // El truco est� en saber d�nde.
            // Puede que en Breadth first search no sea igual la ubicaci�n!

            // Tenemos que tener una forma de saber qui�nes son los vecinos del nodo actual.
            // Hay que ver cu�l de las aristas est� conectada con nuestro nodo CurrentNode.
            // Lo hicimos a trav�s del m�todo FindNeighbors.
            List<Edge> currentNeighbors = FindNeighbors(currentNode);
            // Si esta bandera queda true al terminar el foreach de las aristas, mete a currentNode a la lista cerrada.
            bool sendToClosedList = true;
            // Visita a cada uno de ellos, hasta que se acaben o hasta que encontremos el objetivo.
            foreach (Edge e in currentNeighbors)
            {
                // Checamos cu�l de los dos nodos que esta arista conecta no es el CurrentNode.
                Node NonCurrentNode = currentNode != e.a ? e.a : e.b;
                // Primero checamos si ya est� en la lista abierta, y si lo est�, no mandamos a llamar el algoritmo.
                // tambi�n tenemos que checar que no est� en la lista cerrada!
                if (OpenListStack.Contains(NonCurrentNode) || ClosedSetList.Contains(NonCurrentNode))
                    continue;
                if (NonCurrentNode == Target)
                {
                    // Entonces ya tenemos una ruta de origin a target.
                    // nada m�s le ponemos que target.parent es igual a currentNode y listo, podemos salir de la funci�n.
                    NonCurrentNode.parent = currentNode;
                    return true;
                }
                else
                {
                    // Si no, lo agregamos a lista abierta.
                    OpenListStack.Push(NonCurrentNode);
                    // Cuando t� (Nodo Current) metes a otro nodo a la lista abierta, pones a currentNode como su parent node.
                    NonCurrentNode.parent = currentNode;
                    // En esta versi�n iterativa, cuando currentNode mete a alguien m�s a la lista abierta, 
                    // ese nuevo nodo se convierte en currentNode, y vuelves a empezar el ciclo.
                    sendToClosedList = false;
                    currentNode = NonCurrentNode;
                    break; // Esto hace que el ciclo vaya a la siguiente iteraci�n, sin llegar al c�digo debajo de este continue.
                }
            }

            // Cuando el currentNode no mete a nadie a la lista abierta, quiere decir que ya visit� a todos sus vecinos
            // y por lo tanto, �l se sale de la lista abierta, y se mete a la lista cerrada.
            if (sendToClosedList)
            {
                Node poppedNode = OpenListStack.Pop();
                ClosedSetList.Add(poppedNode);
                currentNode = OpenListStack.Peek(); //Peek es "dame el elemento de hasta arriba pero sin sacarlo de la pila".
            }
            // el else ya no es necesario, porque ya nos encargamos justo antes del "break;" del foreach.

        }

        // no hay camino de origin a target.
        return false;
    }


    public bool DepthFirstSearch(Node Current, Node Target)
    {
        // Cuando t� te paras en un nodo, lo primero que tienes que hacer es si ya est� en la lista cerrada.
        // Si ya est� en la cerrada, no hay nada m�s que hacer.
        if (NodeStateDict[Current] == NodeState.Closed)
            return false;

        // Si el nodo donde estoy parado ahorita no es el nodo al que quiero llegar, entonces todav�a no acabo.
        if (Current == Target)
            return true;
        // SI no son iguales, tenemos que seguir buscando.
        // Primero vamos al primer vecino de este nodo.

        // Tenemos que tener una forma de saber qui�nes son los vecinos del nodo actual.
        // Hay que ver cu�l de las aristas est� conectada con H.
        // Vamos a hacer un m�todo que nos diga con qui�n est� conectado el nodo X.
        List<Edge> currentNeighbors = FindNeighbors(Current);
        // Visita a cada uno de ellos, hasta que se acaben o hasta que encontremos el objetivo.
        foreach (Edge e in currentNeighbors)
        {
            Node NonCurrentNode = Current != e.a ? e.a : e.b;
            // Primero checamos si ya est� en la lista abierta, y si lo est�, no mandamos a llamar el algoritmo.
            if (NodeStateDict[NonCurrentNode] == NodeState.Open)
                continue;
            else
            {
                // Si no, lo ponemos como que ya est� en la lista abierta.
                NodeStateDict[NonCurrentNode] = NodeState.Open;
                // Cuando t� (Nodo Current) metes a otro nodo a la lista abierta, te pones como su parent node.
                NonCurrentNode.parent = Current;
            }

            // Marcamos el nodo como que est� en la lista abierta.
            bool TargetFound = DepthFirstSearch(NonCurrentNode, Target);
            if (TargetFound)
            {
                print("El nodo: " + Current.ID + " fue parte del camino a la meta.");
                return true;
            }
        }

        NodeStateDict[Current] = NodeState.Closed;

        // Cuando ninguno de estos vecinos nos llev� al objetivo, regresamos False.
        return false;

    }

    // M�todo que nos dice quienes son los vecinos de un nodo dado.
    public List<Edge> FindNeighbors(Node in_node)
    {
        List<Edge> out_list = new List<Edge>(); // empieza vac�o.
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

    public bool BreadthFirstSearch(Node Origin, Node Target)
    {
        OpenListQueue.Clear();
        ClosedSetList.Clear();

        // empezamos en el nodo Origen.
        // Ponemos al nodo origen en la lista abierta.
        OpenListQueue.Enqueue(Origin);

        Node currentNode = Origin;

        // Otra posibilidad es while(currentNode != null),
        while (OpenListQueue.Count != 0)  // Mientras haya otros nodos por visitar, sigue ejecutando el algoritmo.
        {
            currentNode = OpenListQueue.Dequeue();
            if (ClosedSetList.Contains(currentNode))
            {
                continue;
            }

            ClosedSetList.Add(currentNode);
            // ya que sabemos cual es el nodo actual, podemos empezar a meter a sus vecinos a la lista abierta.

            // Obtenemos qui�nes son los vecinos del nodo actual.
            // Lo hacemos a trav�s de las aristas conectadas con nuestro nodo CurrentNode.
            // Con el m�todo FindNeighbors.
            List<Edge> currentNeighbors = FindNeighbors(currentNode);
            // Visita a cada uno de ellos, hasta que se acaben o hasta que encontremos el objetivo.
            foreach (Edge e in currentNeighbors)
            {
                // Checamos cu�l de los dos nodos que esta arista conecta no es el CurrentNode.
                Node NonCurrentNode = currentNode != e.a ? e.a : e.b;
                // Primero checamos si ya est� en la lista abierta, y si lo est�, no mandamos a llamar el algoritmo.
                // tambi�n tenemos que checar que no est� en la lista cerrada!
                if ( ClosedSetList.Contains(NonCurrentNode))
                    continue;
                if (NonCurrentNode == Target)
                {
                    // Entonces ya tenemos una ruta de origin a target.
                    // nada m�s le ponemos que target.parent es igual a currentNode y listo, podemos salir de la funci�n.
                    NonCurrentNode.parent = currentNode;
                    return true;
                }
                else
                {
                    // Si no, lo agregamos a lista abierta.
                    OpenListQueue.Enqueue(NonCurrentNode);
                    // Cuando t� (Nodo Current) metes a otro nodo a la lista abierta, pones a currentNode como su parent node.
                    NonCurrentNode.parent = currentNode;
                }
            }
        }

        // no hay camino de origin a target.
        return false;
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
