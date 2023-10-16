using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIsound : MonoBehaviour
{
    // Start is called before the first frame update

    public static UIsound uIsound;

    public AudioClip[] sounds;

    public AudioSource bgm;
    public AudioSource UIClick;
    public int bgmclipnum;

    public void Clicked(){
        UIClick.clip = sounds[1];
        UIClick.Play();
    }
    public void upgraded(){
        UIClick.clip = sounds[2];
        UIClick.Play();
    }

    void Start()
    {
        if(uIsound == null){
            uIsound = this;
        }else{
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
        bgm.clip = sounds[0];
        bgmclipnum = 0;
        play();
    }
    public void GameOver(){
        bgm.volume = 0.5f;
        bgm.loop = false;
        bgm.clip = sounds[4];
        bgmclipnum = 4;
        bgm.Play();
    }
    public void StageClear(){
        bgm.volume = 0.5f;
        bgm.loop = false;
        bgm.clip = sounds[3];
        bgmclipnum = 3;

        bgm.Play();
    }
    public void stop(){
        bgm.Stop();
    }
    public void play(){
        bgm.Play();
    }
    public void inLobby(){
        bgm.loop = true;
        bgm.clip = sounds[5];
        bgm.volume = 0.7f;
        bgmclipnum = 5;
        bgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
