using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour {
    public AudioClip[] music;
    public static AudioSource sound;

    void Start(){
        sound = this.GetComponent<AudioSource>();
        /*for(int i = 0; i < music.Length; i++)
        {
            playMusic(i);
            Debug.Log("听音乐");
        }
        playMusic(0);*/
    }
    public static void PlayMusic(AudioClip ac)
    {
        sound.clip = ac;
        sound.Play();
    }
    public void PlayMusic(int i) { 
        sound.clip = music[i];
        sound.Play();       
    }
}
