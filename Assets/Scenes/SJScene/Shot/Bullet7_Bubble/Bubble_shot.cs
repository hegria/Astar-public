using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble_shot : MonoBehaviour
{
    public float Bubble_size_speed, Start_Speed;
    Vector3 itsvel;
    float theta;
    bool Check_Bubble;
    Animator bublleanimator;
    SpriteRenderer bubsprite;
    public Sprite[] atfirst;
    private void Awake() {
        bublleanimator = gameObject.GetComponent<Animator>();
        bubsprite = gameObject.GetComponent<SpriteRenderer>();
    }
    public void SetAwake(){
        bublleanimator.enabled = false;
        bubsprite.sprite = atfirst[Random.Range(0, 2)];
        transform.localScale = Vector3.one*0.125f;
        Check_Bubble = false;
        theta = Random.Range(75f, 105f)*Mathf.Deg2Rad;
        GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0) * Start_Speed;
        StartCoroutine(size_up());
        StartCoroutine(Speed_down());
    }
    private void Update() {
        if (transform.position.y >= Character.ymax + 0.5f)
        {
            Bullet_Object_Pooling.ReturnObject(7,gameObject);
        }
    }
    IEnumerator size_up()
    {
        float myscalex = transform.localScale.x;
        while(transform.localScale.x < 0.375f)
        {
            transform.localScale += Vector3.one *0.375f*Time.deltaTime;
            yield return null;
        }
        while (transform.localScale.x < 0.575f)
        {
            transform.localScale += Vector3.one*0.75f*Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator Speed_down()
    {
        itsvel = GetComponent<Rigidbody2D>().velocity*0.3f;
        for (int i = 0; i < 100; i++)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.Lerp(GetComponent<Rigidbody2D>().velocity, itsvel, 0.05f);
            yield return null;
        }
        GetComponent<Rigidbody2D>().velocity = itsvel;
    }
    public void Bubble_Destroy()
    {
        Bullet_Object_Pooling.ReturnObject(7,gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!Check_Bubble)
            {
                collision.GetComponent<Enemy>().energy -= GetComponent<Bullet>().damage* (1.0f + 0.1f * PlayerInfo.playerInfo.workshop[3]) / 5f;
                Check_Bubble = true;
                bublleanimator.enabled = true;
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
        }
    }

}
