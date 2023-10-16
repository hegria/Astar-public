using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spread_script : MonoBehaviour
{
    public GameObject sub_shot;
    public static spread_script spread_shot;
    [HideInInspector]
    public Vector3 hispos;
    int spread_num;
    bool itsnot;
    bool Finish;
    public void SetAwake(int Spread_Cnt){
        Finish = false;
        if(Spread_Cnt == 0){
            itsnot = true;
        }
        else{
            itsnot = false;
            spread_num = Spread_Cnt;
        }
    }
    void Update()
    {
        if (transform.position.y > 3f&&!itsnot&&!Finish)
        {
            pung();
        }
        if (transform.position.y >= Character.ymax + 0.5f)
        {
            Bullet_Object_Pooling.ReturnObject(3,gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")&&!itsnot&&!Finish)
        {
            pung();
        }
    }
    void pung()
    {
        for (int i = 0; i < spread_num; i++)
        {
            float divide = (float)spread_num;
            GameObject my_obj = Bullet_Object_Pooling.GetObject(10);
            my_obj.transform.position = transform.position;
            my_obj.GetComponent<circle_moving>().parpos = transform.position;
            my_obj.GetComponent<circle_moving>().setAwake(Mathf.PI * 2 / divide * i*Mathf.Rad2Deg);
        }
        Finish = true;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Invoke("MyDestroy",0.1f);
    }
    void MyDestroy(){
        Bullet_Object_Pooling.ReturnObject(3,gameObject);
    }
}
