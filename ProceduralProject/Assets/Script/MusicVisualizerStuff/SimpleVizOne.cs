using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(LineRenderer))]
public class SimpleVizOne : MonoBehaviour
{
    static public SimpleVizOne viz {get; private set;}
    public float ringRadius = 10;
    public float ringHeight = 5;
    public float orbHeight = 10;
    public float avgAmp = 0;
    public int numBands = 128;
    public List<ChunkMeshController> chunks = new List<ChunkMeshController>();
    public Orb prefabOrb;

    private AudioSource player;
    private LineRenderer line;

    private List<Orb> orbs = new List<Orb>();
    void Start()
    {

        if(!viz) viz = this;
        else 
        {
            Destroy(gameObject);
            return;
        }

        player = GetComponent<AudioSource>();
        line = GetComponent<LineRenderer>();

        //spawn 1 orb for each frequency band:
        Quaternion q = Quaternion.identity;
        for(int i = 0; i < numBands; i++)
        {
            Vector3 p = new Vector3(0, i * orbHeight / numBands, 0);
            orbs.Add(Instantiate(prefabOrb, p, q, transform));
        }
    }

    void OnDestroy()
    {
        if(viz == this) viz = null;
    }

    void Update()
    {
        UpdateWaveform();
        UpdateFreqBands();
    }

    private void UpdateFreqBands()
    {
        float[] bands = new float[numBands];
        player.GetSpectrumData(bands, 0, FFTWindow.BlackmanHarris);

        for (int i = 0; i < orbs.Count; i++)
        {
            //float p = (i + 1) / (float) numBands;
            //orbs[i].localScale = Vector3.one * bands[i] * 200 * p;
            orbs[i].UpdateAudioData(bands[i] * 100);
        
        for (int c = 0; c<chunks.Count; c++)
        {
            chunks[c].UpdateAudioData(bands[c] * 10);
        }
        }
    }

    private void UpdateWaveform()
    {
        int samples = 1024;
        float[] data = new float[samples];
        player.GetOutputData(data, 0);

        Vector3[] points = new Vector3[samples];


        for (int i = 0; i < data.Length; i++)
        {
            float sample = data[i];

            float rads = Mathf.PI * 2 * i / samples;

            float x = Mathf.Cos(rads) * ringRadius;
            float y = sample * ringHeight;
            float z = Mathf.Sin(rads) * ringRadius;

            points[i] = new Vector3(x, y, z);
        }


        line.positionCount = points.Length;
        line.SetPositions(points);
    }
}
