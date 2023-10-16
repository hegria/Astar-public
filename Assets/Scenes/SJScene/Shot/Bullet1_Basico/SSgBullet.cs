using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSgBullet : MonoBehaviour
{
    float theta;
    float standardVel;
    public float Vel;
    public void setAwake(int num){
        standardVel = Vel;
        StartCoroutine(GoStraight(num));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= Character.ymax + 0.5f)
        {
            Bullet_Object_Pooling.ReturnObject(12,gameObject);
        }
    }
    //회전값은 받읍시당
    IEnumerator GoStraight(int BulletCase){
        counting(BulletCase);
        transform.rotation = Quaternion.Euler(new Vector3(0,0,theta));
        while(true){
            transform.Translate(Vector3.up*Time.deltaTime*Vel);
            yield return null;
        }
    }
    void counting(int number)
    {
        switch (number)
        {
            case 0:
                theta = Mathf.PI/2f;
                break;
            case 3:
                theta = 5 / 12f*Mathf.PI;
                break;
            case 4:
                theta = 7 / 12f * Mathf.PI ;
                break;
            case 1:
                theta = 85 / 180f * Mathf.PI;
                break;
            case 2:
                theta = 95 / 180f * Mathf.PI;
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Enemy")){
            Vel = standardVel;
        }
    }
}
