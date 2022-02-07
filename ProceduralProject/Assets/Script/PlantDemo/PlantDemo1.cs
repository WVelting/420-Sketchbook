using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class PlantDemo1 : MonoBehaviour
{
    [Range(0, 20)]
    public int max = 5;

    [Range(5, 60)]
    public float spreadDegrees = 10;

    void Start()
    {
        Build();
    }

    private void OnValidate()
    {
        Build();
    }

    void Build()
    {
        //1. making storage for instances:
        List<CombineInstance> instances = new List<CombineInstance>();

        //2. spawn the instances:

        Grow(instances, Vector3.zero, Quaternion.identity, new Vector3(.25f, 1, .25f), max);



        //3. combining the instances together:
        Mesh mesh = new Mesh();

        mesh.CombineMeshes(instances.ToArray());

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter) meshFilter.mesh = mesh;
    }

    void Grow(List<CombineInstance> instances, Vector3 pos, Quaternion rot, Vector3 scale, int max, int num = 0)
    {
        if (num < 0) num = 0;
        if (num >= max) return;

        //make a cube mesh, add to list
        CombineInstance inst = new CombineInstance();
        inst.mesh = MeshTools.MakeCube();
        inst.transform = Matrix4x4.TRS(pos, rot, scale);

        instances.Add(inst);


        //do recursion

        float percentAtEnd = ++num / (float)max;

        Vector3 endPoint = inst.transform.MultiplyPoint(new Vector3(0, 1, 0));

        if ((pos - endPoint).sqrMagnitude < .1f) return;

        Quaternion newRotation = Quaternion.Lerp(
            rot * Quaternion.Euler(spreadDegrees, Random.Range(-90f, 90f), 0),
            Quaternion.RotateTowards(rot, Quaternion.identity, 45),
            .5f);

        Grow(instances,
        endPoint,
        newRotation,
        scale *= .95f,
        max, num);

        if (num > 1)
        {
            if (num % 2 == 1)
            {

                float degrees = Random.Range(-1, 2) * 90;

                Quaternion newRot = Quaternion.LookRotation(endPoint - pos) * Quaternion.Euler(0, 0, degrees);


                Grow(instances,
                endPoint,
                newRot,
                scale *= .95f,
                max, num);

            }
        }



    }

}
