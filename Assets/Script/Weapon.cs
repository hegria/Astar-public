using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigid;
    public Animator[] childani;
    public SpriteRenderer[] childs;
    bool nowshoot = true;
    public int Weapontype;

    public float shootdelay;

    public AudioSource audioSource;

    void Start()
    {
        StartCoroutine(Playa());
    }
    IEnumerator Playa(){
        if(shootdelay == 0){
            Play();
            yield break;
        }else{
            audioSource.enabled = true;
            while(true){
                yield return new WaitForSeconds(shootdelay);
                audioSource.Stop();
                audioSource.Play();
            }
        }

    }
    IEnumerator Stopa(){
        if(shootdelay == 0){
            Stop();
            yield break;
        }else{
            audioSource.enabled = false;
            StopAllCoroutines();
            Stop();
            yield break;
        }
    }

    public void Play(){
        if(Weapontype==3 ||Weapontype==8||Weapontype==5){
            return;
        }
        audioSource.Play();
        
    }
    public void Stop(){
        if(Weapontype==3 ||Weapontype==8||Weapontype==5){
            return;
        }
        audioSource.Stop();

    }

    public void goodbye(){
        gameObject.GetComponent<Animator>().enabled = false;
        StartCoroutine(Stopa());
        for (int i = 0; i < 2;i++){
            childani[i].Rebind();
            childani[i].enabled = false;
            childs[i].color = new Color(1, 1, 1, 0.7f);
        }
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 5f, ForceMode2D.Impulse);
        Invoke("dead", 1.5f);
    }
    public void dead(){
        Destroy(gameObject);
    }

// Update is called once per frame
    void Update()
    {
        if(nowshoot&&(Character.charact.isCleared||Character.charact.isboom||GameManager.gameManager.ispaused)){
            nowshoot = false;
            StartCoroutine(Stopa());
            for (int i = 0; i < 2; i++)
            {
                childani[i].Rebind();
                childani[i].enabled = false;
            }
        }else if (!nowshoot && !(Character.charact.isCleared || Character.charact.isboom|| GameManager.gameManager.ispaused))
        {
            nowshoot = true;
            StartCoroutine(Playa());
            for (int i = 0; i < 2; i++)
            {
                childani[i].enabled = true;
            }
        }
        if(Weapontype == 5 && Character.charact.power >=2){
            shootdelay = 0.7f;
        }
    }
}
