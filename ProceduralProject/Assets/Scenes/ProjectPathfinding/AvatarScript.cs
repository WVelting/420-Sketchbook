using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarScript : MonoBehaviour
{

    public Pathfinder.Node moveTarget;
    public PeonController closestMinion = null;
    public Camera cam;
    private List<Pathfinder.Node> pathToTarget = new List<Pathfinder.Node>();
    private bool shouldCheckAgain = true;
    private float checkAgainIn = 0;
    private LineRenderer line;
    public float health = 15;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        cam = Camera.main;
    }

    void Update()
    {
        if(Input.GetMouseButton(1))
        {

            moveTarget = GridController.singleton.Lookup(cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 27.8f)));
            shouldCheckAgain = true;
            checkAgainIn = 1;
        }

        // foreach(PeonController v in SpawnerController.singleton.minions)
        // {
        //     if((v.transform.position - transform.position).sqrMagnitude < (closestMinion.transform.position - transform.position).sqrMagnitude || closestMinion == null)
        //     {
        //         closestMinion = v;
        //     }
        // }

        // if((closestMinion.transform.position - transform.position).sqrMagnitude <=4)
        // {
        //     Destroy(closestMinion.gameObject);
        // }

        checkAgainIn -= Time.deltaTime;
        if(checkAgainIn <= 0) 
        {
            shouldCheckAgain = true;
            checkAgainIn = 1;
        }
        if(shouldCheckAgain)FindPath();
        MoveAlongPath();
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void MoveAlongPath()
    {
        if(pathToTarget == null) return;
        if(pathToTarget.Count < 2) return;

        //TODO: grab first item in path, move to that Node

        Vector3 target = pathToTarget[1].position;
        target.y += 1;
        transform.position = Vector3.Lerp(transform.position, target, .01f);
        
        float d = (target - transform.position).magnitude;
        
        if(d < .25f) shouldCheckAgain = true;

    }

    private void FindPath()
    {
        shouldCheckAgain = false;

        if (GridController.singleton)
        {
            Pathfinder.Node start = GridController.singleton.Lookup(transform.position);
            Pathfinder.Node end = moveTarget;

            if (start == null || end == null || start == end)
            {
                pathToTarget.Clear(); //empty the list

                return;
            }

            pathToTarget = Pathfinder.Solve(start, end);

            //Rendering the path on a LineRenderer

            Vector3[] positions = new Vector3[pathToTarget.Count];
            for(int i = 0; i < pathToTarget.Count; i ++)
            {
                positions[i] = pathToTarget[i].position + new Vector3(0,1,0);
            }
            line.positionCount = positions.Length;
            line.SetPositions(positions);

        }
    }
}
