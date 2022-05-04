using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(AudioSource))]
public class SongPlayer : MonoBehaviour
{

    public AudioClip[] playlist;

    private AudioSource player;
    private int currentTrack = -1;

    void Start()
    {
        player = GetComponent<AudioSource>();
        PlayTrackRandom();
        
    }

    public void PlayTrack(int t)
    {
        if(t < 0 || t >= playlist.Length) return;
        player.PlayOneShot(playlist[t]);
        currentTrack = t;
    }

    public void PlayTrackRandom()
    {
        PlayTrack(Random.Range(0, playlist.Length));
    }

    public void PlayTrackNext()
    {
        int track = currentTrack + 1;
        if(track >= playlist.Length) track = 0;

        PlayTrack(track);
    }

    void Update()
    {
        if(!player.isPlaying) PlayTrackNext();
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            player.Stop();
            PlayTrackNext();
        }
    }

}
    //TODO: make custom Editor
    [CustomEditor(typeof(SongPlayer))]
    public class MusicPlayerEditor : Editor
    {

    }
