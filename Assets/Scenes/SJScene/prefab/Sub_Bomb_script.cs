using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sub_Bomb_script : MonoBehaviour
{
    public float boomtime;
    Animator animator;

    public AudioSource bombsfx;
    private void Start() {
        bombsfx.Play();
        animator = gameObject.GetComponent<Animator>();
        Invoke("setAni", boomtime);
    }
    void setAni(){
        animator.SetTrigger("Bomb_trigger");
    }
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {

    }
    public void my_destory()
    {
        Destroy(gameObject);
    }
}
