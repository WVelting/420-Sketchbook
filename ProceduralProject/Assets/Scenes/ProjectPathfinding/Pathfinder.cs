using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pathfinder
{
    public class Node
    {
        public Vector3 position;
        public float G {get; private set;}
        public float H {get; private set;}
        public float F
        {
            get
            {
                return G+H;
            }
        }
        public float moveCost = 1; //change value for different terrain

        public List<Node> neighbors = new List<Node>();
        public Node parent {get; private set;}
        

        public void DoHeuristic(Node end)
        {
            Vector3 d = end.position - this.position;
            H = d.magnitude;
        }
        public void UpdateParentAndG(Node parent, float extraG = 0)
    {
        this.parent = parent;
        if(parent != null)
        {
            G = parent.G + moveCost + extraG;
        }
        else G = extraG;
    }
    }

    

    public static List<Node> Solve(Node start, Node end)
    {
        if(start == null || end == null) return new List<Node>();
        

        List<Node> open = new List<Node>();
        List<Node> closed = new List<Node>();

        start.UpdateParentAndG(null);
        open.Add(start);

        //1. find path from start to end
        while(open.Count > 0)
        {
            //find node in OPEN list with SMALLEST F value
            float bestF = 0;
            Node current = null;
            foreach(Node n in open)
            {
                if(n.F < bestF || current == null)
                {
                    current = n;
                    bestF = n.F;
                }
            }

            if(current == end)
            {
                break;
            }
            bool isDone = false;
            foreach(Node neighbor in current.neighbors)
            {
                if(!closed.Contains(neighbor)) //node not in CLOSED
                {
                    if(!open.Contains(neighbor)) //node not in OPEN
                    {
                        open.Add(neighbor);


                        float dis = (neighbor.position - current.position).magnitude;

                        neighbor.UpdateParentAndG(current, dis);

                        if(neighbor == end)
                        {
                            isDone = true;
                        }

                        neighbor.DoHeuristic(end);

                    } 
                    else 
                    { // node already in OPEN

                        //TODO: if G cost is lower, change neighbor's parent
                        if(neighbor.G > current.G + neighbor.moveCost)
                        {
                            float dis = (neighbor.position - current.position).magnitude;

                            //it's shorter to move to neighbor from current
                            neighbor.UpdateParentAndG(current, dis);
                        }
                    }
                }
                
            }

            closed.Add(current);
            open.Remove(current);
            if(isDone) break;

        }

        //2. travel from end to start, bulding path
        List<Node> path = new List<Node>();
        
        for(Node temp = end; temp != null; temp = temp.parent)
        {
            path.Add(temp);
        }

        //3. reverse path
        path.Reverse();

        return path; 
    }
}

/*

Dykstra -
    Keep a list of OPEN nodes
    Foreach node:
        Record how far node is from start
        Add neighbors to OPEN list
            If neighbor is END, return chain of nodes
        Move to CLOSED list

Greedy Best/first Search -
    Keep a list of OPEN nodes
    Pick one node most likely to be closer to END // Heuristics
        Add neighbors to OPEN list
            If is END, return chain of nodes
        Move to CLOSED list

A* -
    Keep a list of OPEN nodes
    Pick one node w/ lowest cost = (how far from start + how far from end) // Heuristic
    F = total cost
    G = how far from start
    H = how far to end
    F = G + H
        Add neighbors to OPEN list

            Record how far node is from start
            If is END, return chain of nodes
        Move to CLOSED list

Heurestics
    Euclidean(As the bird flies, line to end)
    Manhattan(dx + dy, no diagonal lines)

*/
