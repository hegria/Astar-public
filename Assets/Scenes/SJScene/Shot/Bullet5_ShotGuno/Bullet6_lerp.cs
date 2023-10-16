using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet6_lerp : MonoBehaviour
{
    float theta;
    public float speed;
    public int shot_case;
    public void SetAwake(int Shot_case){
        if(shot_case == 0)
        {
            theta = Random.Range(80f, 100f);
        }
        if(shot_case == 1)
        {
            theta = Random.Range(60f, 120f);
        }
        transform.Rotate(new Vector3(0, 0, theta - 90));
        gameObject.GetComponent<Rigidbody2D>().AddForce(speed * new Vector2(Mathf.Cos(theta*Mathf.Deg2Rad), Mathf.Sin(theta*Mathf.Deg2Rad)), ForceMode2D.Impulse);
        StartCoroutine(nurf());
    }
    void Update()
    {
        if (transform.position.y >= Character.ymax + 0.5f)
        {
            Bullet_Object_Pooling.ReturnObject(5,gameObject);
        }
    }
    IEnumerator nurf()
    {
        Vector3 myvel = GetComponent<Rigidbody2D>().velocity*0.3f;
        for (int i = 0; i < 100; i++)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.Lerp(GetComponent<Rigidbody2D>().velocity, myvel, 0.05f);
            yield return null;
        }
        GetComponent<Rigidbody2D>().velocity = myvel;
    }
}
