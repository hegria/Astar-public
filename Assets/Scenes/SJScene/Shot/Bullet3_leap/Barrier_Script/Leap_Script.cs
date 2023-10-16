using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leap_Script : MonoBehaviour
{
    float theta;
    public float AngleVelociy, Circle_Radius;
    public void SetAwake(float myStart)
    {
        theta = 0;
        StartCoroutine(Spin_Leap(myStart));
    }

    // Update is called once per frame
    void Update()
    {
        theta += Time.deltaTime;
    }
    IEnumerator Spin_Leap(float Start_Transform){
        while(true){
            transform.position = Character.chartrans.position + new Vector3(Mathf.Cos(Start_Transform+theta*Mathf.Deg2Rad*AngleVelociy),Mathf.Sin(Start_Transform+theta*Mathf.Deg2Rad*AngleVelociy),0)*Circle_Radius;
            yield return null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("E_bullet"))
        {
            Destroy(collision.gameObject);
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(CoolTime(5f));
        }
    }
    IEnumerator CoolTime(float cool){
        yield return new WaitForSeconds(cool);
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
