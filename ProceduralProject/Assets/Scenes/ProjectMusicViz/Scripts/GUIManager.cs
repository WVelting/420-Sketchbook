using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class GUIManager : MonoBehaviour
{
    public Slider volume;
    public Slider bassLevel;   
    public Slider midLevel;   
    public Slider trebLevel;   
    public AudioMixerGroup EQ;

    private float vol;
    private float bass;
    private float treb;
    private float mid;


    void Start()
    {
        EQ.audioMixer.GetFloat("Volume", out vol);
        EQ.audioMixer.GetFloat("BassLevel", out bass);
        EQ.audioMixer.GetFloat("MidLevel", out mid);
        EQ.audioMixer.GetFloat("TrebLevel", out treb);

        volume.value = vol;
        bassLevel.value = bass;
        midLevel.value = mid;
        trebLevel.value = treb;
    }

    void Update()
    {
        
    }

    public void OnValueChangeVolume(float val)
    {
        EQ.audioMixer.SetFloat("Volume", val);
    }

    public void OnValueChangeBass(float val)
    {
        EQ.audioMixer.SetFloat("BassLevel", val);
    }

    public void OnValueChangeMid(float val)
    {
        EQ.audioMixer.SetFloat("MidLevel", val);
    }

    public void OnValueChangeTreb(float val)
    {
        EQ.audioMixer.SetFloat("TrebLevel", val);
    }
}
