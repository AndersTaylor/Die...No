using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] songs;

    public AudioSource songPlayer;
    public AudioClip playingSong;
    private int index;

    void Start()
    {
        songPlayer = GetComponent<AudioSource>();
        index = Random.Range(0, songs.Length);
        playingSong = songs[index];
        SetAndPlay();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            songPlayer.time = playingSong.length - 1;

        if (!songPlayer.isPlaying)
        {
            //Debug.Log(index);
            SetAndPlay();
        }
    }

    public void SetAndPlay()
    {
        if (index < songs.Length - 1)
        {
            index++;
            playingSong = songs[index];
        }
        else
        {
            index = 0;
            playingSong = songs[index];
        }
        songPlayer.clip = playingSong;
        songPlayer.Play();
    }
}
