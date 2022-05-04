using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Orb : MonoBehaviour
{

    SimpleVizOne viz;
    Rigidbody body;


    void Start()
    {
        viz = SimpleVizOne.viz;
        body = GetComponent<Rigidbody>();
        GetComponent<MeshRenderer>().material.SetFloat("_TimeOffset", Random.Range(0, Mathf.PI * 2));

    }

    public void UpdateAudioData(float val)
    {
        transform.localScale = Vector3.one * (transform.localScale.x + val);
    }

    void Update()
    {
        Vector3 vToViz = viz.transform.position - transform.position;
        Vector3 dirToViz = vToViz.normalized;

        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, .01f);

        body.AddForce(dirToViz * 100 * Time.deltaTime);
    }
}
