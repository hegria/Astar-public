using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : MonoBehaviour
{
    bool HardVersion;
    public void SetAwake(float vel){
        StartCoroutine(Go_Up(vel));
    }
    private void Update() {
        if(HardVersion){
            if(Vector3.Distance(transform.position,Character.chartrans.position) < 2f){
                StopAllCoroutines();
            }
        }
        if(Vector3.Distance(transform.position,PracBoss.myBoss3.transform.position) > 10f){
            Destroy(gameObject);
        }
    }
    IEnumerator Go_Up(float Vel){
    //yield return new WaitForSeconds(0.5f);
    while(true){
        transform.Translate(Vector3.up*Vel*Time.deltaTime);
        yield return null;
    }
    }
}
