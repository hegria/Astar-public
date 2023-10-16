using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss5_bullet : MonoBehaviour
{
    public int ShotType;
    private void Update() {
        theta+=Time.deltaTime;
    }
    public void setAwake(float vel,int type){
        ShotType = type;
        transform.Rotate(0,0,180);
        switch(ShotType){
            case 0 :
            StartCoroutine(go_straight(vel,false));
            break;
            case 1:
            StartCoroutine(go_straight(vel,true));
            break;
            case 2:
            StartCoroutine(Go_Trigonal(5,0.7f));
            break;
        }
    }
    IEnumerator go_straight(float Vel,bool pung){
        while(true){
            transform.Translate(Vector3.up*Time.deltaTime*Vel);
            if(Vector3.Distance(Boss5.boss5.transform.position,transform.position)>5f&&pung){
                go_Pung(6);
            }
            yield return null;
        }
    }
    float theta;
    public int clock;
    IEnumerator Go_Trigonal(float Radius, float Angle){
        while(true){
            transform.Translate(new Vector3(Mathf.Cos(theta*Angle),clock*Mathf.Sin(theta*Angle),0)*theta*Radius*Time.deltaTime);
            yield return null;
        }
    }
    void go_Pung(int SpreadCnt){
        float deltatheta = 360/SpreadCnt;
        float Myran = Random.Range(0,deltatheta);
        for(int i = 0;i<SpreadCnt;i++){
            GameObject mygame = Instantiate(gameObject,transform.position,Quaternion.Euler(0,0,deltatheta*i+Myran));
            mygame.transform.localScale *=.5f;
            mygame.GetComponent<Boss5_bullet>().setAwake(10,0);
            mygame.GetComponent<Destroy_Boss_bullet>().sub = true;
            mygame.GetComponent<Destroy_Boss_bullet>().pos = transform.position;
        }
        Destroy(gameObject);
    }
}
