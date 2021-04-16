using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPath : MonoBehaviour
{
 
     //Inicia o Script com os parametros que ele recebe, waypoints e Graph
    public void Inicialize(GameObject[] waypoints,Graph graph) 
    {
        wps = waypoints;
        g = graph;
        currentNode = wps[0];
        agent = GetComponent<NavMeshAgent>();
    }
    
    //Ele inicia um metodo que A* para achar o melhor caminho para  o Heli
    public void GoToHeli()
    {
        //Meotod A*, que recebe o pronto que esta no momento e qual é o destino.
        //g.AStar(currentNode, wps[1]);
        //Manda um destino para o NavMeshAgent, para ele seguir até esse ponto atraves do proprio componente, pegando o Bake do NavMesh
        agent.SetDestination(wps[1].transform.position);
        currentWP = 0;
    }

    //Ele inicia um metodo que A* para achar o melhor caminho para  a Ruina
    public void GoToRuin()
    {
        //Meotod A*, que recebe o pronto que esta no momento e qual é o destino.
       // g.AStar(currentNode, wps[6]);
        //Reseta a current que ele esta
        currentWP = 0;
        //Manda um destino para o NavMeshAgent, para ele seguir até esse ponto atraves do proprio componente, pegando o Bake do NavMesh
       agent.SetDestination(wps[6].transform.position);
    }

 
    public void GoToFabrica()
    {
        // g.AStar(currentNode, wps[8]);
        //Reseta a current que ele esta

        agent.SetDestination(wps[8].transform.position);
        currentWP = 0;
    }

    private void LateUpdate()
    {
      
        if (g.getPathLength() == 0 || currentWP == g.getPathLength())
            return;

        currentNode = g.getPathPoint(currentWP);
        //Quando estiver chegando perto ele muda para proximo ponto
        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position, transform.position) < accuracy)
        {
            currentWP++;
        }
        //Continue enquanto a currente for menor que a lista de caminho
        if (currentWP < g.getPathLength())
        {
            //Ação que faz o tank seguir a direção para o ponto
            goal = g.getPathPoint(currentWP).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
        }
        // o que faz o tank andar no eixo Z 
        transform.Translate(0, 0, Time.deltaTime * speed);
    }


    // Componente NavMeshAgent que serve para pegar o Agent do Tank que interagir com o NavMesh
    NavMeshAgent agent;

    //Variavel de transform que pega a proxima direção
    Transform goal;
    //velocidade de andar e rotação, mais a distancia que ele chega
    float speed = 5.0f;
    float accuracy = 1.0f;
    float rotSpeed = 2.0f;
    //A lista de pontos
    GameObject[] wps;
    //o objeto que ele esta, o waypoint que esta, o Node
    GameObject currentNode;
    //Contador
    int currentWP = 0;
    //A classe Graph que faz toda a logica para andar e buscar o proximo ponto e A*
    Graph g;

}
