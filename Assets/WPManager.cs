using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
//A classe que faz as direção
public struct Link{
    //a direção ser BI OU UNI, mais a direção que seria os dois gameobjects
    public enum direction { UNI, BI }
    public GameObject node1; 
    public GameObject node2; 
    public direction dir;}

public class WPManager : MonoBehaviour
{
    //array de Waypoints
    public GameObject[] waypoints; 
    //A direção que ele pode fazer, com o Link, direção Bi e Uni
    public Link[] links; 
    //A classe G, para gerenciar os wapoints e os links
    public Graph graph = new Graph();

    // Start is called before the first frame update
    void Start()
    {
        //Gerenciador e colocando os waypoints no Graph e os links, causa a lista de waypoints for maior que 0
        if (waypoints.Length > 0)
        {
            foreach(GameObject wp in waypoints)
            {
                graph.AddNode(wp);
            }
            foreach (Link l in links)
            {
                //Adiciona uma direção, caso for BI, ele adiciona mais uma que seria a ida e a volta
                graph.AddEdge(l.node1, l.node2);
                if (l.dir == Link.direction.BI)
                    graph.AddEdge(l.node2, l.node1);
            }
            //Inicia o script de Follow, dando os parametros que ele recebe (eu que fiz KKKK, treinando essa norma)
            follow.Inicialize(graph:graph,waypoints:waypoints);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Desenhar
        graph.debugDraw();
    }

    [SerializeField] private FollowPath follow;
}
